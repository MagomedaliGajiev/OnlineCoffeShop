using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace OnlineCoffeShop.Web.Models.Orders;

public class Order
{
    public required Guid Id { get; init; }

    [Required]
    [StringLength(32)]
    [RegularExpression(@"^[A-Z]{2}-\d{4}-\d+$")]
    public required string Number { get; init; }

    [ValidateNever]
    public string UserId { get; init; } = string.Empty;

    [Required]
    public DateTime PlacedAt { get; init; }

    [Required]
    public decimal Total { get; init; }

    [Required]
    public OrderStatus Status { get; set; } = OrderStatus.CREATED;

    [Required]
    public DeliveryMethod Delivery { get; init; } = DeliveryMethod.Courier;

    [ValidateNever]
    public List<OrderItem> Items { get; init; } = [];

    [Required]
    public string CustomerName { get; init; } = string.Empty;

    [Required]
    public string Phone { get; init; } = string.Empty;

    [Required]
    public string Email { get; init; } = string.Empty;

    [Required]
    public string City { get; init; } = string.Empty;

    [Required]
    public string Address { get; init; } = string.Empty;

    public string? Apt { get; init; }

    public string? Entrance { get; init; }

    public string? Floor { get; init; }

    public string? Comment { get; init; }

    /// <summary>Желаемая дата доставки (из формы оформления).</summary>
    public DateTime? DeliveryDate { get; init; }

    public PaymentMethod Payment { get; init; } = PaymentMethod.CARD_ONLINE;

    public int Count => Items.Sum(i => i.Qty);
}