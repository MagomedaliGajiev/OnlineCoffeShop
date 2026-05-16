using System.ComponentModel.DataAnnotations;

namespace OnlineCoffeShop.Web.Models;

public class Order
{
    public required Guid Id { get; init; }

    [StringLength(32)]
    [RegularExpression(@"^[A-Z]{2}-\d{4}-\d+$")]
    public required string Number { get; init; }

    public string UserId { get; init; } = string.Empty;

    public DateTime PlacedAt { get; init; }

    public decimal Total { get; init; }

    public OrderStatus Status { get; init; } = OrderStatus.Pending;

    public DeliveryMethod Delivery { get; init; } = DeliveryMethod.Courier;

    public List<OrderItem> Items { get; init; } = [];

    public int Count => Items.Sum(i => i.Qty);
}

public class OrderItem
{
    public required string Name { get; init; }

    public int Qty { get; init; }

    public decimal Price { get; init; }

    public ArtStyle Art { get; init; } = ArtStyle.Dark;
}

public enum OrderStatus
{
    Pending,
    Delivered,
}

public enum DeliveryMethod
{
    Courier,
    Pickup,
    Cdek,
}