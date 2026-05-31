using System.ComponentModel.DataAnnotations;
using OnlineCoffeShop.Web.Validation;

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

    /// <summary>Текущий поисковый запрос.</summary>
    public string? Query { get; init; }
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
    [StringLength(25, MinimumLength = 2,
        ErrorMessage = "Имя должно быть от 2 до 25 символов")]
    [Display(Name = "Имя", Prompt = "Как к вам обращаться?")]
    public string FirstName { get; set; } = string.Empty;

    [Required(ErrorMessage = "Укажите фамилию")]
    [StringLength(25, MinimumLength = 2,
        ErrorMessage = "Фамилия должна быть от 2 до 25 символов")]
    [Display(Name = "Фамилия", Prompt = "Ваша фамилия")]
    public string LastName { get; set; } = string.Empty;

    [Required(ErrorMessage = "Укажите телефон")]
    [DataType(DataType.PhoneNumber)]
    [StringLength(16, MinimumLength = 5,
        ErrorMessage = "Телефон должен быть от 5 до 16 символов")]
    [Display(Name = "Телефон", Prompt = "+7 ___ ___ __ __")]
    [Phone(ErrorMessage = "Некорректный телефон")]
    public string Phone { get; set; } = string.Empty;

    [Required(ErrorMessage = "Укажите email")]
    [EmailAddress(ErrorMessage = "Некорректный email")]
    [Display(Name = "Email", Prompt = "your@email.com")]
    public string Email { get; set; } = string.Empty;

    [Required(ErrorMessage = "Укажите город")]
    [Display(Name = "Город", Prompt = "Москва")]
    public string City { get; set; } = string.Empty;

    [Required(ErrorMessage = "Укажите адрес")]
    [StringLength(100, MinimumLength = 5,
        ErrorMessage = "Адрес должен быть от 5 до 100 символов")]
    [Display(Name = "Улица, дом", Prompt = "ул. Покровка, д. 1")]
    public string Address { get; set; } = string.Empty;

    public string Apt { get; set; } = string.Empty;

    public string Entrance { get; set; } = string.Empty;

    public string Floor { get; set; } = string.Empty;

    [Required(ErrorMessage = "Выберите дату доставки")]
    [DataType(DataType.Date)]
    [DateWithinThreeMonths] // ← наш кастомный валидатор (шаг 4)
    [Display(Name = "Дата доставки")] // у даты — только Name, без Prompt (по заданию)
    public DateTime? Date { get; set; }

    [StringLength(512, ErrorMessage = "Комментарий — не более 512 символов")]
    [Display(Name = "Комментарий курьеру", Prompt = "Например: позвонить за 15 минут")]
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
    [Required(ErrorMessage = "Введите email")]
    [EmailAddress(ErrorMessage = "Некорректный email")]
    [StringLength(30, MinimumLength = 5,
        ErrorMessage = "Email должен быть от 5 до 30 символов")]
    [Display(Name = "Email", Prompt = "your@email.com")]
    public string Email { get; set; } = string.Empty;

    [Required(ErrorMessage = "Введите пароль")]
    [DataType(DataType.Password)]
    [StringLength(30, MinimumLength = 6,
        ErrorMessage = "Пароль должен быть от 6 до 50 символов")]
    [Display(Name = "Пароль", Prompt = "••••••••")]
    public string Password { get; set; } = string.Empty;

    public bool Remember { get; set; } = true;

    public string? ReturnUrl { get; set; }
}

public class RegisterViewModel
{
    [Required(ErrorMessage = "Укажите имя")]
    [Display(Name = "Имя", Prompt = "Как к вам обращаться?")]
    public string Name { get; set; } = string.Empty;

    [Required(ErrorMessage = "Введите email")]
    [EmailAddress(ErrorMessage = "Некорректный email")]
    [StringLength(30, MinimumLength = 5,
        ErrorMessage = "Email должен быть от 5 до 30 символов")]
    [Display(Name = "Email", Prompt = "your@email.com")]
    public string Email { get; set; } = string.Empty;

    [Required(ErrorMessage = "Введите пароль")]
    [DataType(DataType.Password)]
    [StringLength(50, MinimumLength = 6,
        ErrorMessage = "Пароль должен быть от 6 до 50 символов")]
    [Display(Name = "Пароль", Prompt = "минимум 6 символов")]
    public string Password { get; set; } = string.Empty;

    [Required(ErrorMessage = "Повторите пароль")]
    [DataType(DataType.Password)]
    [Compare(nameof(Password), ErrorMessage = "Пароли не совпадают")]
    [Display(Name = "Повторите пароль", Prompt = "ещё раз тот же пароль")]
    public string ConfirmPassword { get; set; } = string.Empty;

    public bool Agree { get; set; }

    public string? ReturnUrl { get; set; }
}

public class ProductFormViewModel
{
    public Guid Id { get; set; } // пусто = создание, заполнено = редактирование

    [Required(ErrorMessage = "Укажите название")]
    public string Name { get; set; } = string.Empty;

    [Required(ErrorMessage = "Укажите slug")]
    [RegularExpression(
        "^[a-z0-9-]+$",
        ErrorMessage = "Только строчные латинские буквы, цифры и дефис")]
    public string Slug { get; set; } = string.Empty;

    [Required]
    public ProductCategory Category { get; set; } = ProductCategory.Coffee;

    public string Type { get; set; } = string.Empty;

    public string? Roast { get; set; }

    public string? Origin { get; set; }

    public int? WeightGrams { get; set; }

    [Range(0, 1_000_000, ErrorMessage = "Цена не может быть отрицательной")]
    public decimal Price { get; set; }

    public decimal? OldPrice { get; set; }

    public ProductTag? Tag { get; set; }

    public ArtStyle Art { get; set; } = ArtStyle.Dark;

    public string? Notes { get; set; } // вкусовые ноты — через запятую

    public string? Specs { get; set; } // характеристики — построчно "Ключ=Значение"

    public string Blurb { get; set; } = string.Empty;
}

public class CompareViewModel
{
    public IReadOnlyList<Product> Products { get; init; } = Array.Empty<Product>();

    public IReadOnlyList<CompareRow> Rows { get; init; } = Array.Empty<CompareRow>();

    /// <summary>Показывать только строки, где значения различаются.</summary>
    public bool DiffOnly { get; init; }
}

/// <summary>Одна строка таблицы сравнения: название характеристики и значения по каждому товару.</summary>
public class CompareRow
{
    public string Label { get; init; } = string.Empty;

    public IReadOnlyList<string> Values { get; init; } = Array.Empty<string>();

    /// <summary>true — у всех товаров значение одинаковое (строка не «отличие»).</summary>
    public bool AllSame { get; init; }
}