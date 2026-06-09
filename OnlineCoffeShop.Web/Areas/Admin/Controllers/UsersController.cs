using Microsoft.AspNetCore.Mvc;

namespace OnlineCoffeShop.Web.Areas.Admin.Controllers;

[Area("Admin")]
public class UsersController : Controller
{
    public IActionResult Index() => View();
}