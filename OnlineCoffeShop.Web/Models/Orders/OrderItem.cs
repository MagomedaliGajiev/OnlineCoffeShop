namespace OnlineCoffeShop.Web.Models.Orders;

public class OrderItem
{
    public required string Name { get; init; }

    public int Qty { get; init; }

    public decimal Price { get; init; }

    public ArtStyle Art { get; init; } = ArtStyle.Dark;
}