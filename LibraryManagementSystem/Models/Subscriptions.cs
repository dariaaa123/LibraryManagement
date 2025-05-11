namespace LibraryManagementSystem.Models;

public class Subscription
{
    public Guid BookId { get; set; }
    public string Email { get; set; } = string.Empty;
}