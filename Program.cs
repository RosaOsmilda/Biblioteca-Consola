using System.Text;
using BibliotecaConsola;

Console.OutputEncoding = Encoding.UTF8;

// Identificación del estudiante (se pide al iniciar)
Console.WriteLine("=== IDENTIFICACIÓN DEL ESTUDIANTE ===");
string StudentName = ReadRequiredText("Nombre completo: ");
string StudentId = ReadRequiredText("Matrícula: ");
Console.WriteLine();

BookCatalog catalog = new BookCatalog();
SeedBooks(catalog);

bool running = true;
while (running)
{
    ShowMenu();
    string option = Console.ReadLine()?.Trim() ?? "";
    Console.WriteLine();

    switch (option)
    {
        case "1": AddBook(); break;
        case "2": ListBooks(); break;
        case "3": SearchBook(); break;
        case "4": UpdateBook(); break;
        case "5": DeleteBook(); break;
        case "0": running = false; Console.WriteLine("Hasta luego."); break;
        default: Console.WriteLine("Opción no válida."); break;
    }

    if (running)
    {
        Console.WriteLine("\nPresione Enter para continuar...");
        Console.ReadLine();
        Console.WriteLine();
    }
}

// ---------------------------------------------------------------------------
// Menú y operaciones
// ---------------------------------------------------------------------------

void ShowMenu()
{
    Console.WriteLine("====== SISTEMA DE BIBLIOTECA ======");
    Console.WriteLine($"Estudiante: {StudentName} | Matrícula: {StudentId}\n");
    Console.WriteLine($"Libros registrados: {catalog.Count}/{BookCatalog.MaxCapacity}\n");
    Console.WriteLine("1. Agregar libro");
    Console.WriteLine("2. Listar libros");
    Console.WriteLine("3. Buscar libro por ID");
    Console.WriteLine("4. Actualizar libro");
    Console.WriteLine("5. Eliminar libro");
    Console.WriteLine("0. Salir\n");
    Console.Write("Seleccione una opción: ");
}

void AddBook()
{
    Console.WriteLine("--- Agregar libro ---");

    if (catalog.IsFull)
    {
        Console.WriteLine($"No se puede agregar: el catálogo alcanzó su capacidad máxima ({BookCatalog.MaxCapacity} libros).");
        return;
    }

    string title = ReadRequiredText("Título: ");
    string author = ReadRequiredText("Autor: ");
    int year = ReadInt("Año de publicación: ", 0, DateTime.Now.Year);

    if (catalog.Add(title, author, year, out Book? created))
        Console.WriteLine($"Libro agregado correctamente con ID {created!.Id}.");
    else
        Console.WriteLine("No se pudo agregar el libro.");
}

void ListBooks()
{
    Console.WriteLine("--- Catálogo de libros ---");
    Book[] books = catalog.GetAll();

    if (books.Length == 0)
    {
        Console.WriteLine("El catálogo está vacío.");
        return;
    }

    PrintHeader();
    foreach (Book b in books)
        Console.WriteLine(b);
}

void SearchBook()
{
    Console.WriteLine("--- Buscar libro por ID ---");
    int id = ReadInt("ID del libro: ", 1, int.MaxValue);

    Book? book = catalog.FindById(id);
    if (book == null)
    {
        Console.WriteLine($"No existe un libro con ID {id}.");
        return;
    }

    PrintHeader();
    Console.WriteLine(book);
}

void UpdateBook()
{
    Console.WriteLine("--- Actualizar libro ---");
    int id = ReadInt("ID del libro a actualizar: ", 1, int.MaxValue);

    Book? book = catalog.FindById(id);
    if (book == null)
    {
        Console.WriteLine($"No existe un libro con ID {id}.");
        return;
    }

    Console.WriteLine("Datos actuales:");
    PrintHeader();
    Console.WriteLine(book);
    Console.WriteLine("\n(Deje vacío y presione Enter para conservar el valor actual)\n");

    Console.Write("Nuevo título: ");
    string? title = Console.ReadLine();

    Console.Write("Nuevo autor: ");
    string? author = Console.ReadLine();

    int? year = null;
    while (true)
    {
        Console.Write("Nuevo año: ");
        string? input = Console.ReadLine();
        if (string.IsNullOrWhiteSpace(input)) break;
        if (int.TryParse(input, out int y) && y >= 0 && y <= DateTime.Now.Year)
        {
            year = y;
            break;
        }
        Console.WriteLine("Año no válido.");
    }

    catalog.Update(id, title, author, year);
    Console.WriteLine("\nLibro actualizado correctamente.");
}

void DeleteBook()
{
    Console.WriteLine("--- Eliminar libro ---");
    int id = ReadInt("ID del libro a eliminar: ", 1, int.MaxValue);

    if (catalog.Remove(id))
        Console.WriteLine("Libro eliminado. Los elementos posteriores fueron desplazados a la izquierda.");
    else
        Console.WriteLine($"No existe un libro con ID {id}.");
}

// ---------------------------------------------------------------------------
// Utilidades de entrada / salida
// ---------------------------------------------------------------------------

void PrintHeader()
{
    Console.WriteLine($"{"ID",-4} | {"Título",-38} | {"Autor",-24} | Año");
    Console.WriteLine(new string('-', 80));
}

string ReadRequiredText(string prompt)
{
    while (true)
    {
        Console.Write(prompt);
        string? input = Console.ReadLine();
        if (!string.IsNullOrWhiteSpace(input))
            return input.Trim();
        Console.WriteLine("El campo no puede estar vacío.");
    }
}

int ReadInt(string prompt, int min, int max)
{
    while (true)
    {
        Console.Write(prompt);
        if (int.TryParse(Console.ReadLine(), out int value) && value >= min && value <= max)
            return value;
        Console.WriteLine($"Ingrese un número entero entre {min} y {max}.");
    }
}

// ---------------------------------------------------------------------------
// Datos iniciales (12 libros, mínimo requerido: 10)
// ---------------------------------------------------------------------------

void SeedBooks(BookCatalog c)
{
    c.Add("Cien años de soledad", "Gabriel García Márquez", 1967, out _);
    c.Add("Don Quijote de la Mancha", "Miguel de Cervantes", 1605, out _);
    c.Add("El principito", "Antoine de Saint-Exupéry", 1943, out _);
    c.Add("1984", "George Orwell", 1949, out _);
    c.Add("La casa de los espíritus", "Isabel Allende", 1982, out _);
    c.Add("Rayuela", "Julio Cortázar", 1963, out _);
    c.Add("Crónica de una muerte anunciada", "Gabriel García Márquez", 1981, out _);
    c.Add("El túnel", "Ernesto Sabato", 1948, out _);
    c.Add("Pedro Páramo", "Juan Rulfo", 1955, out _);
    c.Add("Ficciones", "Jorge Luis Borges", 1944, out _);
    c.Add("Clean Code", "Robert C. Martin", 2008, out _);
    c.Add("Introduction to Algorithms", "Thomas H. Cormen", 1990, out _);
}