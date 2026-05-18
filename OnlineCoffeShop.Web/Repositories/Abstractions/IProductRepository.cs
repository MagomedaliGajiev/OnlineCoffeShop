using OnlineCoffeShop.Web.Models;

namespace OnlineCoffeShop.Web.Repositories.Abstractions;

public interface IProductRepository
{
    IReadOnlyList<Product> GetAll { get; }

    IReadOnlyList<CategorySummary> Categories { get; }

    Product? Find(Guid id);

    Product? FindBySlug(string slug);

    IEnumerable<Product> Query(string? category, string? roast, string? origin, string? sort);

    void Add(Product product);

    void Update(Product product);

    void Delete(Guid id);
}