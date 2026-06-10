using Microsoft.AspNetCore.Mvc;
using OnlineCoffeShop.Web.Models;
using OnlineCoffeShop.Web.Models.Users;
using OnlineCoffeShop.Web.Repositories.Abstractions;

namespace OnlineCoffeShop.Web.Areas.Admin.Controllers;

[Area("Admin")]
public class UsersController : Controller
{
    private readonly IUsersRepository _users;
    private readonly IRolesRepository _roles;

    public UsersController(IUsersRepository users, IRolesRepository roles)
    {
        _users = users;
        _roles = roles;
    }

    public IActionResult Index() => View(_users.GetAll());

    public IActionResult Details(Guid id)
    {
        var user = _users.GetById(id);
        return user is null ? NotFound() : View(user);
    }

    // ---- Добавление нового пользователя ----
    [HttpGet]
    public IActionResult Create() => View(new UserCreateViewModel());

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(UserCreateViewModel model)
    {
        if (!ModelState.IsValid)
            return View(model);

        if (_users.TryGetByEmail(model.Email) is not null)
        {
            ModelState.AddModelError(string.Empty, "Пользователь с таким email уже существует");
            return View(model);
        }

        _users.Add(new User
        {
            FirstName = model.FirstName,
            LastName = model.LastName,
            Email = model.Email,
            Phone = model.Phone,
            Password = model.Password,
            Role = "User",
        });
        return RedirectToAction(nameof(Index));
    }

    // ---- Редактирование данных ----
    [HttpGet]
    public IActionResult Update(Guid id)
    {
        var user = _users.GetById(id);
        if (user is null)
            return NotFound();

        return View(new UserEditViewModel
        {
            Id = user.Id,
            FirstName = user.FirstName,
            LastName = user.LastName,
            Email = user.Email,
            Phone = user.Phone,
        });
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Update(UserEditViewModel model)
    {
        if (!ModelState.IsValid)
            return View(model);

        var user = _users.GetById(model.Id);
        if (user is null)
            return NotFound();

        // если поменяли email на уже занятый кем-то другим
        var byEmail = _users.TryGetByEmail(model.Email);
        if (byEmail is not null && byEmail.Id != model.Id)
        {
            ModelState.AddModelError(string.Empty, "Этот email уже занят другим пользователем");
            return View(model);
        }

        user.FirstName = model.FirstName;
        user.LastName = model.LastName;
        user.Email = model.Email;
        user.Phone = model.Phone;
        _users.Update(user);

        return RedirectToAction(nameof(Details), new { id = user.Id });
    }

    // ---- Смена пароля ----
    [HttpGet]
    public IActionResult ChangePassword(Guid id)
    {
        var user = _users.GetById(id);
        return user is null ? NotFound() : View(new ChangePasswordViewModel { Id = id });
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult ChangePassword(ChangePasswordViewModel model)
    {
        if (!ModelState.IsValid)
            return View(model);

        if (_users.GetById(model.Id) is null)
            return NotFound();

        _users.ChangePassword(model.Id, model.NewPassword);
        return RedirectToAction(nameof(Details), new { id = model.Id });
    }

    // ---- Смена прав доступа (опционально) ----
    [HttpGet]
    public IActionResult ChangeRole(Guid id)
    {
        var user = _users.GetById(id);
        if (user is null)
            return NotFound();

        ViewBag.Roles = _roles.GetAll();   // для выпадающего списка
        return View(new ChangeRoleViewModel { Id = id, Role = user.Role });
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult ChangeRole(ChangeRoleViewModel model)
    {
        if (_users.GetById(model.Id) is null)
            return NotFound();

        if (!ModelState.IsValid)
        {
            ViewBag.Roles = _roles.GetAll();   // не забыть при возврате формы
            return View(model);
        }

        _users.ChangeRole(model.Id, model.Role);
        return RedirectToAction(nameof(Details), new { id = model.Id });
    }

    // ---- Удаление ----
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Delete(Guid id)
    {
        _users.Delete(id);
        return RedirectToAction(nameof(Index));
    }
}