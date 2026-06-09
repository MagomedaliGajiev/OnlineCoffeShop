using OnlineCoffeShop.Web.Models.Users;

namespace OnlineCoffeShop.Web.Repositories.Abstractions;

public interface IUsersRepository
{
    IReadOnlyList<User> GetAll();

    // Понадобится и для входа (найти и сверить пароль),
    // и для регистрации (проверить, что email ещё не занят).
    public User? TryGetByEmail(string email);

    public void Add(User user);
}