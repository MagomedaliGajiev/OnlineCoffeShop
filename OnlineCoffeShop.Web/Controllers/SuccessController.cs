using Microsoft.AspNetCore.Mvc;
using OnlineCoffeShop.Web.Models;
using OnlineCoffeShop.Web.Repositories.Abstractions;

namespace OnlineCoffeShop.Web.Controllers;

public class SuccessController : Controller
{
    private readonly IOrderRepository _orders;

    public SuccessController(IOrderRepository orders)
    {
        _orders = orders;
    }

    public async Task<IActionResult> Index(Guid? id = null)
    {
        var order = id.HasValue ? _orders.GetById(id.Value) : null;
        if (order is null)
        {
            order = new Order
            {
                Id = Guid.NewGuid(),
                Number = "BH-2026-1248",
                UserId = "_demo",
                PlacedAt = new DateTime(2026, 5, 11),
                Total = 5860,
                Status = OrderStatus.Pending,
                Delivery = DeliveryMethod.Courier,
                Items = new List<OrderItem>(),
            };
        }

        var vm = new SuccessViewModel
        {
            Order = order,
            UserName = "Магомедали",
            UserEmail = string.Empty,
        };

        return View(vm);
    }
}