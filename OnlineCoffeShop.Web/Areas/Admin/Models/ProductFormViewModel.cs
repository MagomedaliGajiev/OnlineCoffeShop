using System.ComponentModel.DataAnnotations;
using OnlineCoffeShop.Web.Models;

namespace OnlineCoffeShop.Web.Areas.Admin.Models;

public class ProductFormViewModel
{
    public Guid Id { get; set; } // пусто = создание, заполнено = редактирование

    [Required(ErrorMessage = "Укажите название")]
    [StringLength(200, MinimumLength = 2,
        ErrorMessage = "Название должно быть от 2 до 200 символов")]
    [Display(Name = "Название", Prompt = "Эфиопия Иргачеффе")]
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

    [Required(ErrorMessage = "Укажите цену")]
    [Range(0, 1_000_000,
        ErrorMessage = "Цена должна быть от 0 до 1 000 000 ₽")]
    [Display(Name = "Цена, ₽", Prompt = "Например: 1290")]
    public decimal? Price { get; set; }

    public decimal? OldPrice { get; set; }

    public ProductTag? Tag { get; set; }

    public ArtStyle Art { get; set; } = ArtStyle.Dark;

    public string? Notes { get; set; } // вкусовые ноты — через запятую

    public string? Specs { get; set; } // характеристики — построчно "Ключ=Значение"

    [StringLength(4096, ErrorMessage = "Описание — не более 4096 символов")]
    [Display(Name = "Описание", Prompt = "Краткое описание товара")]
    public string Blurb { get; set; } = string.Empty;
}