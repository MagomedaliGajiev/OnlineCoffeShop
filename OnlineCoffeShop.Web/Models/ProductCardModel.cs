namespace OnlineCoffeShop.Web.Models;

public class ProductCardModel
{
    public Product Product { get; init; } = null!;
    public bool IsFav { get; init; }
    public string? ReturnUrl { get; init; }
}