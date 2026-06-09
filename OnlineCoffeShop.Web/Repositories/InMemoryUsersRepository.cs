using OnlineCoffeShop.Web.Models.Users;
using OnlineCoffeShop.Web.Repositories.Abstractions;

namespace OnlineCoffeShop.Web.Repositories;

public class InMemoryUsersRepository : IUsersRepository
{
    private readonly object _lock = new ();
    private readonly List<User> _users;

    public InMemoryUsersRepository()
    {
        // Сразу заводим тестового пользователя, чтобы можно было войти
        _users = new List<User>
        {
            new() { Name = "Администратор", Email = "admin@beanhouse.ru", Password = "admin123", },
        };
    }

    public IReadOnlyList<User> GetAll()
    {
        lock (_lock)
        {
            return _users.ToList();
        }
    }

    public User? TryGetByEmail(string email)
    {
        lock (_lock)
        {
            return _users.FirstOrDefault(u =>
                string.Equals(u.Email, email, StringComparison.OrdinalIgnoreCase));
        }
    }

    public void Add(User user)
    {
        lock (_lock)
        {
            _users.Add(user);
        }
    }
}