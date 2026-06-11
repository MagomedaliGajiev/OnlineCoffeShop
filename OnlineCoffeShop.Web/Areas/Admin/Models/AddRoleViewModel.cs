using System.ComponentModel.DataAnnotations;

namespace OnlineCoffeShop.Web.Areas.Admin.Models;

public class AddRoleViewModel
{
    [Required(ErrorMessage = "Укажите наименование роли")]
    [StringLength(50, MinimumLength = 2,
        ErrorMessage = "Наименование должно быть от 2 до 50 символов")]
    [Display(Name = "Наименование роли", Prompt = "Например: Manager")]
    public string Name { get; set; } = string.Empty;
}