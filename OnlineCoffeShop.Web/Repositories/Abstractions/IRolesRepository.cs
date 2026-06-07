using OnlineCoffeShop.Web.Models.Roles;

namespace OnlineCoffeShop.Web.Repositories.Abstractions;

public interface IRolesRepository
{
    IReadOnlyList<Role> GetAll();

    Role? TryGetByName(string name);

    void Add(Role role);

    void Delete(string name);
}