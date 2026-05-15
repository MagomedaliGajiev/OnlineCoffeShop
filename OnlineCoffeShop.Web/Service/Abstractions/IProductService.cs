using OnlineCoffeShop.Web.Models;

namespace OnlineCoffeShop.Web.Service.Abstractions;

public interface IProductService
{
    IReadOnlyList<Product> GetAll { get; }
    
    IReadOnlyList<CategorySummary> Categories { get; }
    
    Product? Find(Guid id);
    
    Product? FindBySlug(string slug);
    
    IEnumerable<Product> Query(string? category, string? roast, string? origin, string? sort);
}