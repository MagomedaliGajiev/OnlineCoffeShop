using System.ComponentModel.DataAnnotations;

namespace OnlineCoffeShop.Web.Areas.Admin.Models;

// Добавление нового пользователя из админки
public class UserCreateViewModel
{
    [Required(ErrorMessage = "Укажите имя")]
    [Display(Name = "Имя", Prompt = "Магомедали")]
    public string FirstName { get; set; } = string.Empty;

    [Required(ErrorMessage = "Укажите фамилию")]
    [Display(Name = "Фамилия", Prompt = "Гаджиев")]
    public string LastName { get; set; } = string.Empty;

    [Required(ErrorMessage = "Введите email")]
    [EmailAddress(ErrorMessage = "Некорректный email")]
    [Display(Name = "Email (логин)", Prompt = "your@email.com")]
    public string Email { get; set; } = string.Empty;

    [Required(ErrorMessage = "Укажите телефон")]
    [Phone(ErrorMessage = "Некорректный телефон")]
    [Display(Name = "Телефон", Prompt = "+7 ___ ___ __ __")]
    public string Phone { get; set; } = string.Empty;

    [Required(ErrorMessage = "Введите пароль")]
    [DataType(DataType.Password)]
    [StringLength(50, MinimumLength = 6, ErrorMessage = "Пароль должен быть от 6 до 50 символов")]
    [Display(Name = "Пароль", Prompt = "минимум 6 символов")]
    public string Password { get; set; } = string.Empty;
}