using OnlineCoffeShop.Web.Models;

namespace OnlineCoffeShop.Web.Repositories;

public static class ProductsRepository
{
    private static readonly List<Product> _products =
    [
        new Product(
            Guid.NewGuid(),
            "Эспрессо",
            150m,
            "Классический крепкий кофе, приготовленный под давлением."),
        new Product(

            Guid.NewGuid(),
            "Капучино",
            220m,
            "Эспрессо с молоком и густой молочной пенкой."),
        new Product
        (
            Guid.NewGuid(),
            "Латте",
            250m,
            "Нежный кофейный напиток с большим количеством молока.")
    ];

    public static IReadOnlyList<Product> GetAll() => _products;
}