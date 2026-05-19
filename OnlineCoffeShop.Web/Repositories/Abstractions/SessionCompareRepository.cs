using System.Text.Json;

namespace OnlineCoffeShop.Web.Repositories.Abstractions;

public class SessionCompareRepository : ICompareRepository
{
    private const string Key = "compare";
    public const int MaxItems = 4;

    private readonly IHttpContextAccessor _http;

    public SessionCompareRepository(IHttpContextAccessor http) => _http = http;

    private ISession Session => _http.HttpContext!.Session;

    public List<Guid> GetIds()
    {
        var json = Session.GetString(Key);
        return string.IsNullOrEmpty(json)
            ? new List<Guid>()
            : JsonSerializer.Deserialize<List<Guid>>(json) ?? new();
    }

    public bool IsInCompare(Guid id) => GetIds().Contains(id);

    public bool Toggle(Guid id)
    {
        var ids = GetIds();
        if (ids.Remove(id)) // уже был — убираем, всегда успешно
        {
            Save(ids);
            return true;
        }

        if (ids.Count >= MaxItems) // лимит — не добавляем
            return false;

        ids.Add(id);
        Save(ids);
        return true;
    }

    public void Clear() => Session.Remove(Key);

    public int Count() => GetIds().Count;

    private void Save(IEnumerable<Guid> ids)
        => Session.SetString(Key, JsonSerializer.Serialize(ids));
}