namespace OnlineCoffeShop.Web.Models.Orders;

public static class OrderStatusExtensions
{
    public static string DisplayName(this OrderStatus s) => s switch
    {
        OrderStatus.CREATED => "Создан",
        OrderStatus.PROCESSED => "Обработан",
        OrderStatus.SHIPPING => "В пути",
        OrderStatus.DELIVERED => "Доставлен",
        OrderStatus.CANCELLED => "Отменён",
        _ => s.ToString(),
    };
}