using LibraryManagementSystem.Data;
using LibraryManagementSystem.Models;

namespace LibraryManagementSystem.Services;

public class SubscriptionService
{
    private readonly SubscriptionRepository _repo;
    private List<Subscription> _subs;

    public SubscriptionService(SubscriptionRepository repo)
    {
        _repo = repo;
        _subs = _repo.Load();
    }

    public void Subscribe(Guid bookId, string email)
    {
        if (!_subs.Any(s => s.BookId == bookId && s.Email == email))
        {
            _subs.Add(new Subscription { BookId = bookId, Email = email });
            _repo.Save(_subs);
        }
    }

    public List<string> NotifyAndClear(Guid bookId)
    {
        var emails = _subs
            .Where(s => s.BookId == bookId)
            .Select(s => s.Email)
            .ToList();

        _subs.RemoveAll(s => s.BookId == bookId);
        _repo.Save(_subs);
        return emails;
    }
}