using Microsoft.AspNetCore.Mvc;
using OnlineCoffeShop.Web.Models;
using OnlineCoffeShop.Web.Models.Roles;
using OnlineCoffeShop.Web.Repositories.Abstractions;

namespace OnlineCoffeShop.Web.Areas.Admin.Controllers;

[Area("Admin")]
public class RolesController : Controller
{
    private readonly IRolesRepository _roles;

    public RolesController(IRolesRepository roles)
    {
        _roles = roles;
    }

    public IActionResult Index() => View(_roles.GetAll());

    [HttpGet]
    public IActionResult Add() => View(new AddRoleViewModel());

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Add(AddRoleViewModel model)
    {
        if (!ModelState.IsValid)
            return View(model);

        if (_roles.TryGetByName(model.Name) is not null)
        {
            ModelState.AddModelError(string.Empty, "Такая роль уже существует");
            return View(model);
        }

        _roles.Add(new Role { Name = model.Name });
        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Delete(string name)
    {
        _roles.Delete(name);
        return RedirectToAction(nameof(Index));
    }
}