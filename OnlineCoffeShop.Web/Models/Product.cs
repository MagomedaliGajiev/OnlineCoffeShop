using System.ComponentModel.DataAnnotations;

namespace OnlineCoffeShop.Web.Models;

public class Product
{
    public required Guid Id { get; init; }

    [StringLength(64)]
    [RegularExpression("^[a-z0-9-]+$")]
    public required string Slug { get; init; }

    public required string Name { get; init; }

    public required ProductCategory Category { get; init; }

    public string Type { get; init; } = "";

    public string? Roast { get; init; }

    public string? Origin { get; init; }

    public int? WeightGrams { get; init; }

    public decimal Price { get; init; }

    public decimal? OldPrice { get; init; }

    public decimal AverageRating { get; init; }

    public int ReviewCount { get; init; }

    public ProductTag? Tag { get; init; }

    public ArtStyle Art { get; init; } = ArtStyle.Dark;

    public string[]? Notes { get; init; }

    public string Blurb { get; init; } = "";

    public Dictionary<string, string> Specs { get; init; } = new();

    public int DiscountPercent => OldPrice.HasValue && OldPrice > 0
        ? (int)Math.Round((1m - Price / OldPrice.Value) * 100m)
        : 0;
}
