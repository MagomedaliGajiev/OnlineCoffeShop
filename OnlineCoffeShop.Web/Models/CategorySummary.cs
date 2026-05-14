namespace OnlineCoffeShop.Web.Models;

public class CategorySummary
{
    public ProductCategory Id { get; init; }
    public string Name { get; init; } = "";
    public int Count { get; init; }
}