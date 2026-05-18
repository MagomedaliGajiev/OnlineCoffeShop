using Microsoft.AspNetCore.Mvc;
using OnlineCoffeShop.Web.Models;

namespace OnlineCoffeShop.Web.Controllers;

public class AccountController : Controller
{
    [HttpGet]
    public IActionResult Login(string? returnUrl = null)
    {
        ViewData["ReturnUrl"] = returnUrl;
        return View(new LoginViewModel { ReturnUrl = returnUrl });
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Login(LoginViewModel model)
    {
        // model.Email и model.Password уже заполнены тем,
        // что пользователь ввёл в форму.
        // TODO: логику проверки/входа добавим позже.
        return View(model);
    }
}