using Microsoft.AspNetCore.Mvc;
using LibraryManagementSystem.Models;
using LibraryManagementSystem.Services;

namespace LibraryManagementSystem.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BooksController : ControllerBase
{
    private readonly BookService _service;
    private readonly SubscriptionService _subs;

    public BooksController(BookService service, SubscriptionService subs)
    {
        _service = service;
        _subs = subs;
    }

    [HttpGet]
    public IActionResult GetAll() => Ok(_service.GetAll());

    [HttpGet("{id}")]
    public IActionResult GetById(Guid id)
    {
        var book = _service.GetById(id);
        return book == null ? NotFound() : Ok(book);
    }

    [HttpGet("search")]
    public IActionResult Search(string? title, string? author)
        => Ok(_service.Search(title, author));

    [HttpPost]
    public IActionResult Add(Book book)
    {
        _service.Add(book);
        return CreatedAtAction(nameof(GetById), new { id = book.Id }, book);
    }

    [HttpPut("{id}")]
    public IActionResult Update(Guid id, Book updated)
    {
        updated.Id = id;
        return _service.Update(updated) ? NoContent() : NotFound();
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(Guid id)
        => _service.Delete(id) ? NoContent() : NotFound();

    [HttpPost("{id}/lend")]
    public IActionResult Lend(Guid id)
        => _service.LendBook(id) ? Ok() : BadRequest("Not available");

    [HttpPost("{id}/return")]
    public IActionResult Return(Guid id)
    {
        var success = _service.ReturnBook(id);
        if (!success) return BadRequest("Invalid return");

        var emails = _subs.NotifyAndClear(id);
        if (emails.Any())
        {
            Console.WriteLine($"📬 Notifying subscribers of book {id}:");
            emails.ForEach(e => Console.WriteLine($"→ Email: {e}"));
        }

        return Ok();
    }

    
    [HttpPost("{id}/subscribe")]
    public IActionResult Subscribe(Guid id, [FromQuery] string email)
    {
        var book = _service.GetById(id);
        if (book == null) return NotFound();

        if (book.Quantity > 0)
            return BadRequest("Book is already available. No need to subscribe.");

        _subs.Subscribe(id, email);
        return Ok("Subscription added.");
    }

    
    
    
}