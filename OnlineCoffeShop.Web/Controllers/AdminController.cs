using Microsoft.AspNetCore.Mvc;

namespace OnlineCoffeShop.Web.Controllers;

public class AdminController : Controller
{
    // Точка входа кнопки: сразу открываем первый раздел — "Заказы"
    public IActionResult Index() => RedirectToAction(nameof(Orders));

    public IActionResult Orders() => View();

    public IActionResult Users() => View();

    public IActionResult Roles() => View();

    public IActionResult Products() => View();
}