namespace OnlineCoffeShop.Web.Models.Orders;

/// <summary>
/// Статус заказа.
/// </summary>
public enum OrderStatus
{
    /// <summary>Создан.</summary>
    CREATED,

    /// <summary>Обработан.</summary>
    PROCESSED,

    /// <summary>В пути.</summary>
    SHIPPING,

    /// <summary>Доставлен.</summary>
    DELIVERED,

    /// <summary>Отменён.</summary>
    CANCELLED,
}