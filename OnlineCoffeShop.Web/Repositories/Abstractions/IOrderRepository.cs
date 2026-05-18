using OnlineCoffeShop.Web.Models;

namespace OnlineCoffeShop.Web.Repositories.Abstractions;

public interface IOrderRepository
{
    IReadOnlyList<Order> GetForUser(string userId);

    Order? GetById(Guid id);

    Order Place(string userId, IEnumerable<CartLine> lines, decimal total, CheckoutViewModel form);
}