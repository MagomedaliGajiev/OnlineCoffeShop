using Microsoft.AspNetCore.Mvc;
using OnlineCoffeShop.Web.Repositories.Abstractions;

namespace OnlineCoffeShop.Web.Controllers;

public class FavController : Controller
{
    private readonly IFavoritesRepository _favs;

    public FavController(IFavoritesRepository favs)
    {
        _favs = favs;
    }

    [HttpPost, ValidateAntiForgeryToken]
    public IActionResult Toggle(Guid id, string? returnUrl = null)
    {
        _favs.Toggle(id);
        return !string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl)
            ? LocalRedirect(returnUrl)
            : RedirectToAction("Index", "Home");
    }
}