using System.ComponentModel.DataAnnotations;

namespace OnlineCoffeShop.Web.Areas.Admin.Models;

// Смена пароля (два поля)
public class ChangePasswordViewModel
{
    public Guid Id { get; set; }

    [Required(ErrorMessage = "Введите новый пароль")]
    [DataType(DataType.Password)]
    [StringLength(50, MinimumLength = 6, ErrorMessage = "Пароль должен быть от 6 до 50 символов")]
    [Display(Name = "Новый пароль", Prompt = "минимум 6 символов")]
    public string NewPassword { get; set; } = string.Empty;

    [Required(ErrorMessage = "Повторите пароль")]
    [DataType(DataType.Password)]
    [Compare(nameof(NewPassword), ErrorMessage = "Пароли не совпадают")]
    [Display(Name = "Повторите пароль", Prompt = "ещё раз тот же пароль")]
    public string ConfirmPassword { get; set; } = string.Empty;
}