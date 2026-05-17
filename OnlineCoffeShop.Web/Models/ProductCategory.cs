namespace OnlineCoffeShop.Web.Models;

public enum ProductCategory
{
    Coffee,
    Gear,
    Gift,
}

public static class ProductCategoryExtensions
{
    public static string Slug(this ProductCategory c) => c switch
    {
        ProductCategory.Coffee => "coffee",
        ProductCategory.Gear => "gear",
        ProductCategory.Gift => "gift",
        _ => "coffee",
    };

    public static string DisplayName(this ProductCategory c) => c switch
    {
        ProductCategory.Coffee => "Зерновой кофе",
        ProductCategory.Gear => "Оборудование",
        ProductCategory.Gift => "Подарки",
        _ => string.Empty,
    };

    public static ProductCategory? TryParseSlug(string? slug) => slug?.ToLowerInvariant() switch
    {
        "coffee" => ProductCategory.Coffee,
        "gear" => ProductCategory.Gear,
        "gift" => ProductCategory.Gift,
        _ => null,
    };
}