namespace BibliotecaConsola;

/// <summary>
/// Catálogo de libros. Usa exclusivamente un arreglo (Book[]) de tamaño fijo.
/// Los libros registrados ocupan siempre las posiciones 0 .. Count - 1
/// (nunca hay huecos entre ellos).
/// </summary>
public class BookCatalog
{
    public const int MaxCapacity = 20;

    private readonly Book[] _books = new Book[MaxCapacity];
    private int _count = 0;      // Cantidad de libros registrados = siguiente posición libre
    private int _nextId = 1;     // Generador autoincremental de Id (garantiza Ids únicos)

    public int Count => _count;
    public bool IsFull => _count == MaxCapacity;

    /// <summary>
    /// Agrega un libro en la siguiente posición disponible.
    /// Big O: O(1) - se escribe directamente en el índice _count, sin recorrer el arreglo.
    /// (El Id se genera automáticamente, por lo que no hace falta recorrer para validar duplicados.)
    /// </summary>
    public bool Add(string title, string author, int year, out Book? created)
    {
        created = null;

        // Arreglo lleno: no se puede insertar (intento del libro #21)
        if (IsFull)
            return false;

        created = new Book(_nextId++, title, author, year);
        _books[_count] = created;
        _count++;
        return true;
    }

    /// <summary>
    /// Devuelve una copia de los libros registrados para listarlos.
    /// Big O: O(n) - recorre los n libros registrados.
    /// </summary>
    public Book[] GetAll()
    {
        Book[] result = new Book[_count];
        for (int i = 0; i < _count; i++)
            result[i] = _books[i];
        return result;
    }

    /// <summary>
    /// Busca la posición (índice) de un libro por Id mediante búsqueda lineal.
    /// Big O: O(n) en el peor caso (el libro está al final o no existe);
    ///        O(1) en el mejor caso (está en la posición 0).
    /// </summary>
    private int FindIndexById(int id)
    {
        for (int i = 0; i < _count; i++)
        {
            if (_books[i].Id == id)
                return i;
        }
        return -1; // No encontrado
    }

    /// <summary>
    /// Busca un libro por Id.
    /// Big O: O(n) - búsqueda lineal.
    /// </summary>
    public Book? FindById(int id)
    {
        int index = FindIndexById(id);
        return index == -1 ? null : _books[index];
    }

    /// <summary>
    /// Actualiza los datos de un libro (los valores null o vacíos conservan el dato actual).
    /// Big O: O(n) por la búsqueda + O(1) por la modificación = O(n).
    /// </summary>
    public bool Update(int id, string? newTitle, string? newAuthor, int? newYear)
    {
        int index = FindIndexById(id);           // O(n)
        if (index == -1)
            return false;

        Book book = _books[index];               // Acceso directo por índice: O(1)
        if (!string.IsNullOrWhiteSpace(newTitle)) book.Title = newTitle;
        if (!string.IsNullOrWhiteSpace(newAuthor)) book.Author = newAuthor;
        if (newYear.HasValue) book.Year = newYear.Value;
        return true;
    }

    /// <summary>
    /// Elimina un libro por Id y desplaza a la izquierda los elementos posteriores
    /// para no dejar posiciones vacías.
    /// Big O: O(n) búsqueda + O(n) desplazamiento = O(n).
    /// </summary>
    public bool Remove(int id)
    {
        int index = FindIndexById(id);           // O(n)
        if (index == -1)
            return false;

        // Desplazamiento a la izquierda: O(n) en el peor caso (eliminar el primero)
        for (int i = index; i < _count - 1; i++)
            _books[i] = _books[i + 1];

        _books[_count - 1] = null!;              // Libera la última posición
        _count--;
        return true;
    }
}
