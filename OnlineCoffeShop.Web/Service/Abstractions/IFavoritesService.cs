namespace OnlineCoffeShop.Web.Service.Abstractions;

public interface IFavoritesService
{
    HashSet<Guid> GetIds();
    
    bool IsFav(Guid id);
    
    void Toggle(Guid id);
    
    int Count();
}