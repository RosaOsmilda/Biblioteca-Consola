namespace BibliotecaConsola;

/// <summary>
/// Representa un libro del catálogo.
/// </summary>
public class Book
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Author { get; set; }
    public int Year { get; set; }

    public Book(int id, string title, string author, int year)
    {
        Id = id;
        Title = title;
        Author = author;
        Year = year;
    }

    public override string ToString()
    {
        return $"{Id,-4} | {Title,-38} | {Author,-24} | {Year}";
    }
}
