using LibraryManagementSystem.Data;
using LibraryManagementSystem.Models;

namespace LibraryManagementSystem.Services;

public class BookService
{
    private readonly BookRepository _repo;
    private List<Book> _books;

    public BookService(BookRepository repo)
    {
        _repo = repo;
        _books = _repo.LoadBooks();
    }

    public List<Book> GetAll() => _books;

    public Book? GetById(Guid id) => _books.FirstOrDefault(b => b.Id == id);

    public List<Book> Search(string? title, string? author) =>
        _books.Where(b =>
            (string.IsNullOrEmpty(title) || b.Title.Contains(title, StringComparison.OrdinalIgnoreCase)) &&
            (string.IsNullOrEmpty(author) || b.Author.Contains(author, StringComparison.OrdinalIgnoreCase))
        ).ToList();

    public void Add(Book book)
    {
        _books.Add(book);
        _repo.SaveBooks(_books);
    }

    public bool Update(Book updated)
    {
        var book = GetById(updated.Id);
        if (book == null) return false;

        book.Title = updated.Title;
        book.Author = updated.Author;
        book.Quantity = updated.Quantity;
        _repo.SaveBooks(_books);
        return true;
    }

    public bool Delete(Guid id)
    {
        var book = GetById(id);
        if (book == null) return false;

        _books.Remove(book);
        _repo.SaveBooks(_books);
        return true;
    }

    public bool LendBook(Guid id)
    {
        var book = GetById(id);
        if (book == null || book.Quantity <= 0) return false;

        book.Quantity--;
        _repo.SaveBooks(_books);
        return true;
    }

    public bool ReturnBook(Guid id)
    {
        var book = GetById(id);
        if (book == null) return false;

        book.Quantity++;
        _repo.SaveBooks(_books);
        return true;
    }
}