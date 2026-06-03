namespace OnlineCoffeShop.Web.Models.Orders;

/// <summary>
/// Способ доставки.
/// </summary>
public enum DeliveryMethod
{
    /// <summary>Курьером.</summary>
    Courier,

    /// <summary>Самовывоз.</summary>
    Pickup,

    /// <summary>СДЭК.</summary>
    Cdek,
}