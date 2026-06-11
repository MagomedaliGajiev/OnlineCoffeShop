using System.ComponentModel.DataAnnotations;

namespace OnlineCoffeShop.Web.Areas.Admin.Models;

// Смена прав доступа (опционально)
public class ChangeRoleViewModel
{
    public Guid Id { get; set; }

    [Required(ErrorMessage = "Выберите роль")]
    [Display(Name = "Роль")]
    public string Role { get; set; } = string.Empty;
}