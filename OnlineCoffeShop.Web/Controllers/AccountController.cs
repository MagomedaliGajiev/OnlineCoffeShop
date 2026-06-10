using Microsoft.AspNetCore.Mvc;
using OnlineCoffeShop.Web.Models;
using OnlineCoffeShop.Web.Models.Users;
using OnlineCoffeShop.Web.Repositories.Abstractions;

namespace OnlineCoffeShop.Web.Controllers;

public class AccountController : Controller
{
    private readonly IUsersRepository _users;

    public AccountController(IUsersRepository users)
    {
        _users = users;
    }

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
        // НОВОЕ: ищем пользователя и сверяем пароль
        var user = _users.TryGetByEmail(model.Email);
        if (user is null || user.Password != model.Password)
        {
            // Намеренно не уточняем, что именно неверно (email или пароль) —
            // это стандартная практика безопасности.
            ModelState.AddModelError(
                string.Empty,
                "Неверный email или пароль");

            return View(model);
        }

        // Успех — пользователь найден и пароль совпал.
        // Пока просто уходим на главную (или на returnUrl, если он есть).
        if (!string.IsNullOrEmpty(model.ReturnUrl) && Url.IsLocalUrl(model.ReturnUrl))
        {
            return Redirect(model.ReturnUrl);
        }

        return RedirectToAction("Index", "Home");
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

        // НОВОЕ: проверяем, что такой email ещё не зарегистрирован
        if (_users.TryGetByEmail(model.Email) is not null)
        {
            ModelState.AddModelError(string.Empty,
                "Пользователь с таким email уже зарегистрирован");
            return View(model);
        }

        // НОВОЕ: сохраняем нового пользователя в памяти
        _users.Add(new User()
        {
            FirstName = model.Name,
            Email = model.Email,
            Password = model.Password,
        });

        // После регистрации — на страницу входа
        return RedirectToAction(nameof(Login));
    }
}