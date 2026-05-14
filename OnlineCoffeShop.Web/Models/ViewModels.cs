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

public class ProductDetailsViewModel
{
    public Product Product { get; init; } = null!;
    public bool IsFav { get; init; }
    public int Qty { get; init; } = 1;
    public string Grind { get; init; } = "whole";
    public string Weight { get; init; } = "250";
}