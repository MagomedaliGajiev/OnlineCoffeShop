using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using OnlineCoffeShop.Web.Models;
using OnlineCoffeShop.Web.Service.Abstractions;

namespace OnlineCoffeShop.Web.Controllers;

public class HomeController : Controller
{
    private readonly IProductService _product;
    private readonly IFavoritesService _favs;

    public HomeController(IProductService product, IFavoritesService favs)
    {
        _product = product;
        _favs = favs;
    }
    public IActionResult Index(string filter = "all", string? roast = null, string? origin = null, string sort = "popular")
    {
        var products = _product.Query(filter, roast, origin, sort).ToList();
        var vm = new HomeViewModel
        {
            Categories = _product.Categories,
            Products = products,
            Favs = _favs.GetIds(),
            Filter = filter,
            Roast = roast,
            Origin = origin,
            Sort = sort,
            Hero = _product.GetAll[0],
        };
        return View(vm);
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
