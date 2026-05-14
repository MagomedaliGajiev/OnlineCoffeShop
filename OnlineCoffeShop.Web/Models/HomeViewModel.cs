namespace OnlineCoffeShop.Web.Models;

public class HomeViewModel
{
    public IReadOnlyList<CategorySummary> Categories { get; init; } = Array.Empty<CategorySummary>();
    public IReadOnlyList<Product> Products { get; init; } = Array.Empty<Product>();
    public HashSet<Guid> Favs { get; init; } = new();
    public string Filter { get; init; } = "all";
    public string? Roast { get; init; }
    public string? Origin { get; init; }
    public string Sort { get; init; } = "popular";
    public Product Hero { get; init; } = null!;
}