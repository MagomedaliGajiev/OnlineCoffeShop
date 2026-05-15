namespace OnlineCoffeShop.Web.Models;

/// <summary>
/// Идентификаторы товаров каталога.
/// Guid нельзя объявить как const, поэтому используются static readonly поля.
/// </summary>
public static class ProductIds
{
    public static readonly Guid Ethiopia  = new("11111111-1111-1111-1111-111111111111");
    public static readonly Guid Colombia  = new("22222222-2222-2222-2222-222222222222");
    public static readonly Guid Guatemala = new("33333333-3333-3333-3333-333333333333");
    public static readonly Guid Brazil    = new("44444444-4444-4444-4444-444444444444");
    public static readonly Guid HarioMini = new("55555555-5555-5555-5555-555555555555");
    public static readonly Guid Aeropress = new("66666666-6666-6666-6666-666666666666");
    public static readonly Guid HarioV60  = new("77777777-7777-7777-7777-777777777777");
    public static readonly Guid GiftSet   = new("88888888-8888-8888-8888-888888888888");
}
