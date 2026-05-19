namespace OnlineCoffeShop.Web.Repositories.Abstractions;

public interface ICompareRepository
{
    List<Guid> GetIds();  // List, а не HashSet — важен порядок добавления

    bool IsInCompare(Guid id);

    bool Toggle(Guid id);  // false — товар не добавлен, превышен лимит

    void Clear();

    int Count();
}