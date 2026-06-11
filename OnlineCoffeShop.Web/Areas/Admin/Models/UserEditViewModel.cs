using System.ComponentModel.DataAnnotations;

namespace OnlineCoffeShop.Web.Areas.Admin.Models;

// Редактирование данных (без пароля — он меняется отдельно)
public class UserEditViewModel
{
    public Guid Id { get; set; }

    [Required(ErrorMessage = "Укажите имя")]
    [Display(Name = "Имя")]
    public string FirstName { get; set; } = string.Empty;

    [Required(ErrorMessage = "Укажите фамилию")]
    [Display(Name = "Фамилия")]
    public string LastName { get; set; } = string.Empty;

    [Required(ErrorMessage = "Введите email")]
    [EmailAddress(ErrorMessage = "Некорректный email")]
    [Display(Name = "Email (логин)")]
    public string Email { get; set; } = string.Empty;

    [Required(ErrorMessage = "Укажите телефон")]
    [Phone(ErrorMessage = "Некорректный телефон")]
    [Display(Name = "Телефон")]
    public string Phone { get; set; } = string.Empty;
}