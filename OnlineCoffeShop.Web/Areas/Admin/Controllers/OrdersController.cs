using Microsoft.AspNetCore.Mvc;
using OnlineCoffeShop.Web.Models.Orders;
using OnlineCoffeShop.Web.Repositories.Abstractions;

namespace OnlineCoffeShop.Web.Areas.Admin.Controllers;

[Area("Admin")]
public class OrdersController : Controller
{
    private readonly IOrderRepository _orders;

    public OrdersController(IOrderRepository orders)
    {
        _orders = orders;
    }

    public IActionResult Index() => View(_orders.GetAll());

    public IActionResult Details(Guid id)
    {
        var order = _orders.GetById(id);
        return order is null ? NotFound() : View(order);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult UpdateStatus(Guid id, OrderStatus status)
    {
        _orders.UpdateStatus(id, status);
        return RedirectToAction(nameof(Details), new { id }); // PRG-паттерн
    }
}