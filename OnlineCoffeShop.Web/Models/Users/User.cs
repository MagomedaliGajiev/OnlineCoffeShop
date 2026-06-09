namespace OnlineCoffeShop.Web.Models.Users;

public class User
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public string Name { get; set; } = string.Empty;

    public string Email { get; set; } = string.Empty;

    // Пока учебный проект — храним пароль как есть, в открытом виде.
    // В реальном приложении здесь был бы ХЭШ пароля, а не сам пароль.
    public string Password { get; set; } = string.Empty;
}