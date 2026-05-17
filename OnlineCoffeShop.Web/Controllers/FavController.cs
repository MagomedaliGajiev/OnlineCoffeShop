using Microsoft.AspNetCore.Mvc;
using OnlineCoffeShop.Web.Models;
using OnlineCoffeShop.Web.Repositories.Abstractions;

namespace OnlineCoffeShop.Web.Controllers;

public class FavController : Controller
{
    private readonly IFavoritesRepository _favs;
    private readonly IProductRepository _product;

    public FavController(IFavoritesRepository favs, IProductRepository product)
    {
        _favs = favs;
        _product = product;
    }

    public IActionResult Index()
    {
        var products = _favs.GetIds()
            .Select(_product.Find)
            .Where(p => p is not null)
            .Select(p => p!)
            .ToList();

        return View(new FavoritesViewModel { Products = products });
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Toggle(Guid id, string? returnUrl = null)
    {
        _favs.Toggle(id);
        return !string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl)
            ? LocalRedirect(returnUrl)
            : RedirectToAction("Index", "Home");
    }
}