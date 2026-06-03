namespace OnlineCoffeShop.Web.Models.Orders;

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