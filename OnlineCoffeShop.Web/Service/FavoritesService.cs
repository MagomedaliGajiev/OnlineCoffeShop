using System.Text.Json;
using OnlineCoffeShop.Web.Repositories;
using OnlineCoffeShop.Web.Service.Abstractions;

namespace OnlineCoffeShop.Web.Service;

public class FavoritesService : IFavoritesService
{
    private const string Key = "favs";
    private readonly IHttpContextAccessor _http;
    
    public FavoritesService(IHttpContextAccessor http) => _http = http;
    
    private ISession Session => _http.HttpContext!.Session;
    
    public HashSet<Guid> GetIds()
    {
        var json = Session.GetString(Key);
        if (string.IsNullOrEmpty(json))
        {
            var seed = new HashSet<Guid> { ProductsRepository.EthiopiaId, ProductsRepository.AeropressId };
            Save(seed);
            return seed;
        }

        return JsonSerializer.Deserialize<HashSet<Guid>>(json) ?? new();
    }

    public bool IsFav(Guid id) => GetIds().Contains(id);

    public void Toggle(Guid id)
    {
        var ids = GetIds();
        if (!ids.Add(id)) ids.Remove(id);
        Save(ids);
    }
    
    public int Count() => GetIds().Count;
    
    private  void Save(IEnumerable<Guid> ids)
        => Session.SetString(Key, JsonSerializer.Serialize(ids));
}