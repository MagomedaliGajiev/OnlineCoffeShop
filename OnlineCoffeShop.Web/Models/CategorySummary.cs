namespace OnlineCoffeShop.Web.Models;

public class CategorySummary
{
    public ProductCategory Id { get; init; }

    public string Name { get; init; } = string.Empty;

    public int Count { get; init; }
}