namespace OnlineCoffeShop.Web.Repositories.Abstractions;

public interface IFavoritesRepository
{
    HashSet<Guid> GetIds();

    bool IsFav(Guid id);

    void Toggle(Guid id);

    int Count();
}