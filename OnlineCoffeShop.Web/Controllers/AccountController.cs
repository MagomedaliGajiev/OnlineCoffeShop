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
        // 1) Кастомное правило: логин и пароль НЕ должны совпадать.
        //    Это сравнение двух разных полей — атрибутом на одном
        //    свойстве так не выразить, поэтому добавляем ошибку вручную.
        if (model.Email == model.Password)
        {
            // string.Empty => ошибка уровня ВСЕЙ модели (model-level),
            // её покажет asp-validation-summary="ModelOnly".
            ModelState.AddModelError(
                string.Empty,
                "Логин и пароль не должны совпадать");
        }

        // 2) Общая проверка: прошли ли ВСЕ атрибуты + наша ручная ошибка.
        //    AddModelError выше уже сделал ModelState невалидным,
        //    поэтому проверяем IsValid ПОСЛЕ него.
        if (!ModelState.IsValid)
        {
            // Возвращаем ту же модель — иначе поля очистятся
            // и пользователь не увидит, что он вводил, и где ошибки.
            return View(model);
        }

        // Сюда дошли — данные валидны.
        // TODO: реальная логика входа (проверка пользователя в БД и т.п.)
        return View(model);
    }

    [HttpGet]
    public IActionResult Register(string returnUrl = null!)
    {
        // Открывает пустую форму регистрации.
        return View(new RegisterViewModel { ReturnUrl = returnUrl });
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Register(RegisterViewModel model)
    {
        // То же правило для регистрации.
        if (model.Email == model.Password)
        {
            ModelState.AddModelError(string.Empty,
                "Логин и пароль не должны совпадать");
        }

        if (!ModelState.IsValid)
        {
            return View(model);
        }

        // TODO: сохранение нового пользователя.
        return View(model);
    }
}