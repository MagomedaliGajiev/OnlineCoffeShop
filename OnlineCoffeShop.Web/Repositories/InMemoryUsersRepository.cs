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
            new()
            {
                FirstName = "Магомедали",
                LastName = "Гаджиев",
                Email = "admin@beanhouse.ru",
                Phone = "+79894434821",
                Password = "admin123",
                Role = "Admin",
            },
        };
    }

    public IReadOnlyList<User> GetAll()
    {
        lock (_lock)
        {
            return _users.ToList();
        }
    }

    public User? GetById(Guid id)
    {
        lock (_lock)
        {
            return _users.FirstOrDefault(u => u.Id == id);
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

    public void Update(User user)
    {
        lock (_lock)
        {
            var existing = _users.FirstOrDefault(u => u.Id == user.Id);
            if (existing is null) return;

            existing.FirstName = user.FirstName;
            existing.LastName = user.LastName;
            existing.Email = user.Email;
            existing.Phone = user.Phone;

            // пароль и роль здесь НЕ трогаем — для них отдельные методы
        }
    }

    public void Delete(Guid id)
    {
        lock (_lock)
        {
            _users.RemoveAll(u => u.Id == id);
        }
    }

    public void ChangePassword(Guid id, string newPassword)
    {
        lock (_lock)
        {
            var user = _users.FirstOrDefault(u => u.Id == id);
            if (user is not null)
                user.Password = newPassword;
        }
    }

    public void ChangeRole(Guid id, string role)
    {
        lock (_lock)
        {
            var user = _users.FirstOrDefault(u => u.Id == id);
            if (user is not null)
                user.Role = role;
        }
    }
}