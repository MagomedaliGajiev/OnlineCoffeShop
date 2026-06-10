using OnlineCoffeShop.Web.Models.Users;

namespace OnlineCoffeShop.Web.Repositories.Abstractions;

public interface IUsersRepository
{
    IReadOnlyList<User> GetAll();

    User? GetById(Guid id);

    // Понадобится и для входа (найти и сверить пароль),
    // и для регистрации (проверить, что email ещё не занят).
    public User? TryGetByEmail(string email);

    public void Add(User user);

    void Update(User user);

    void Delete(Guid id);

    void ChangePassword(Guid id, string newPassword);

    void ChangeRole(Guid id, string role);
}