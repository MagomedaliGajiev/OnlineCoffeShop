using OnlineCoffeShop.Web.Models.Roles;
using OnlineCoffeShop.Web.Repositories.Abstractions;

namespace OnlineCoffeShop.Web.Repositories;

public class InMemoryRolesRepository : IRolesRepository
{
    private readonly object _lock = new();
    private readonly List<Role> _roles;

    public InMemoryRolesRepository()
    {
        _roles = new List<Role>
        {
            new() { Name = "Admin" },
        };
    }

    public IReadOnlyList<Role> GetAll()
    {
        lock (_lock)
        {
            return _roles.ToList();
        }
    }

    // Регистронезависимый поиск — чтобы "admin" и "Admin" считались дублем
    public Role? TryGetByName(string name)
    {
        lock (_lock)
        {
            return _roles.FirstOrDefault(r =>
                string.Equals(r.Name, name, StringComparison.OrdinalIgnoreCase));
        }
    }

    public void Add(Role role)
    {
        lock (_lock)
        {
            _roles.Add(role);
        }
    }

    public void Delete(string name)
    {
        lock (_lock)
        {
            _roles.RemoveAll(r =>
                string.Equals(r.Name, name, StringComparison.OrdinalIgnoreCase));
        }
    }
}