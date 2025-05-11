using System.Text.Json;
using LibraryManagementSystem.Models;

namespace LibraryManagementSystem.Data;

public class BookRepository
{
    private readonly string _filePath = "Data/books.json";

    public List<Book> LoadBooks()
    {
        if (!File.Exists(_filePath))
            return new List<Book>();

        var json = File.ReadAllText(_filePath);
        return JsonSerializer.Deserialize<List<Book>>(json) ?? new List<Book>();
    }

    public void SaveBooks(List<Book> books)
    {
        var json = JsonSerializer.Serialize(books, new JsonSerializerOptions { WriteIndented = true });
        File.WriteAllText(_filePath, json);
    }
}