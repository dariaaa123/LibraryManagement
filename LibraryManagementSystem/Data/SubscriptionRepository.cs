using System.Text.Json;
using LibraryManagementSystem.Models;

namespace LibraryManagementSystem.Data;

public class SubscriptionRepository
{
    private readonly string _filePath = "Data/subscriptions.json";

    public List<Subscription> Load()
    {
        if (!File.Exists(_filePath))
            return new List<Subscription>();

        var json = File.ReadAllText(_filePath);
        return JsonSerializer.Deserialize<List<Subscription>>(json) ?? new List<Subscription>();
    }

    public void Save(List<Subscription> subs)
    {
        var json = JsonSerializer.Serialize(subs, new JsonSerializerOptions { WriteIndented = true });
        File.WriteAllText(_filePath, json);
    }
}