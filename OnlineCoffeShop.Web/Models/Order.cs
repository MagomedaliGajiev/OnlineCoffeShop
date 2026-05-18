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

    public string CustomerName { get; init; } = string.Empty;

    public string Phone { get; init; } = string.Empty;

    public string Email { get; init; } = string.Empty;

    public string City { get; init; } = string.Empty;

    public string Address { get; init; } = string.Empty;

    public string? Apt { get; init; }

    public string? Entrance { get; init; }

    public string? Floor { get; init; }

    public string? Comment { get; init; }

    public PaymentMethod Payment { get; init; } = PaymentMethod.CARD_ONLINE;

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

public enum PaymentMethod
{
    /// <summary>Картой онлайн (Visa, MC, МИР).</summary>
    CARD_ONLINE = 0,

    /// <summary>СБП — Система быстрых платежей (перевод по QR).</summary>
    SBP = 1,

    /// <summary>Apple Pay (Touch / Face ID).</summary>
    APPLE_PAY = 2,

    /// <summary>Google Pay (привязанные карты).</summary>
    GOOGLE_PAY = 3,
}