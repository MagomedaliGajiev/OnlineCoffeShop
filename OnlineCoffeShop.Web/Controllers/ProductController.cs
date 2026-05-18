using Microsoft.AspNetCore.Mvc;
using OnlineCoffeShop.Web.Models;
using OnlineCoffeShop.Web.Repositories.Abstractions;

namespace OnlineCoffeShop.Web.Controllers;

public class ProductController : Controller
{
    private readonly IProductRepository _product;
    private readonly IFavoritesRepository _fav;

    public ProductController(IProductRepository product, IFavoritesRepository fav)
    {
        _product = product;
        _fav = fav;
    }

    [Route("/product/{slug}")]
    public IActionResult Details(string slug)
    {
        var product = _product.FindBySlug(slug);
        if (product is null)
            return NotFound();

        var vm = new ProductDetailsViewModel
        {
            Product = product,
            IsFav = _fav.IsFav(product.Id),
        };

        return View(vm);
    }
}