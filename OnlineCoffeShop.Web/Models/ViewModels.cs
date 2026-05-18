using System.ComponentModel.DataAnnotations;

namespace OnlineCoffeShop.Web.Models;

public class HomeViewModel
{
    public IReadOnlyList<CategorySummary> Categories { get; init; } = Array.Empty<CategorySummary>();

    public IReadOnlyList<Product> Products { get; init; } = Array.Empty<Product>();

    public HashSet<Guid> Favs { get; init; } = new();

    public string Filter { get; init; } = "all";

    public string? Roast { get; init; }

    public string? Origin { get; init; }

    public string Sort { get; init; } = "popular";

    public Product Hero { get; init; } = null!;
}

public class ProductDetailsViewModel
{
    public Product Product { get; init; } = null!;

    public bool IsFav { get; init; }

    public int Qty { get; init; } = 1;

    public string Grind { get; init; } = "whole";

    public string Weight { get; init; } = "250";
}

public class FavoritesViewModel
{
    public IReadOnlyList<Product> Products { get; init; } = Array.Empty<Product>();
}

public class CartViewModel
{
    public IReadOnlyList<CartLine> Lines { get; init; } = Array.Empty<CartLine>();

    public decimal Subtotal { get; init; }

    public decimal Discount { get; init; }

    public decimal Shipping { get; init; }

    public decimal Total { get; init; }

    public bool PromoApplied { get; init; }

    public string? Promo { get; init; }

    public bool IsAuthenticated { get; init; }
}

public class CheckoutViewModel
{
    public IReadOnlyList<CartLine> Lines { get; init; } = Array.Empty<CartLine>();

    public decimal Subtotal { get; init; }

    public decimal Discount { get; init; }

    public decimal Shipping { get; init; }

    public decimal Total { get; init; }

    [Required(ErrorMessage = "Укажите имя")]
    public string FirstName { get; set; } = string.Empty;

    [Required(ErrorMessage = "Укажите фамилию")]
    public string LastName { get; set; } = string.Empty;

    [Required]
    [Phone(ErrorMessage = "Некорректный телефон")]
    public string Phone { get; set; } = string.Empty;

    [Required]
    [EmailAddress(ErrorMessage = "Некорректный email")]
    public string Email { get; set; } = string.Empty;

    [Required(ErrorMessage = "Укажите город")]
    public string City { get; set; } = string.Empty;

    [Required(ErrorMessage = "Укажите адрес")]
    public string Address { get; set; } = string.Empty;

    public string Apt { get; set; } = string.Empty;

    public string Entrance { get; set; } = string.Empty;

    public string Floor { get; set; } = string.Empty;

    public string? Comment { get; set; }

    public string Delivery { get; set; } = "courier";

    public PaymentMethod Payment { get; set; } = PaymentMethod.CARD_ONLINE;
}

public class SuccessViewModel
{
    public Order Order { get; init; } = null!;

    public string UserName { get; init; } = string.Empty;

    public string UserEmail { get; init; } = string.Empty;
}

public class LoginViewModel
{
    [Required]
    [EmailAddress]
    public string Email { get; set; } = string.Empty;

    [Required]
    [DataType(DataType.Password)]
    public string Password { get; set; } = string.Empty;

    public bool Remember { get; set; } = true;

    public string? ReturnUrl { get; set; }
}