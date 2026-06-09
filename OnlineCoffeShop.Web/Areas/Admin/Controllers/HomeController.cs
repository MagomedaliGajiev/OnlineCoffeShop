using Microsoft.AspNetCore.Mvc;

namespace OnlineCoffeShop.Web.Areas.Admin.Controllers;

[Area("Admin")]
public class HomeController : Controller
{
    // Кнопка "Панель администратора" ведёт сюда -> сразу открываем "Заказы"
    public IActionResult Index() => RedirectToAction("Index", "Orders");
}