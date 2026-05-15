using Microsoft.AspNetCore.Mvc;
using OnlineCoffeShop.Web.Models;
using OnlineCoffeShop.Web.Service.Abstractions;

namespace OnlineCoffeShop.Web.Controllers;

public class ProductController : Controller
{
    private readonly IProductService _product;
    private readonly IFavoritesService _fav;

    public ProductController(IProductService product, IFavoritesService fav)
    {
        _product = product;
        _fav = fav;
    }

    [Route("/product/{slug}")]
    public IActionResult Details(string slug)
    {
        var product = _product.FindBySlug(slug) ?? _product.GetAll[0];

        var vm = new ProductDetailsViewModel
        {
            Product = product,
            IsFav = _fav.IsFav(product.Id),
        };

        return View(vm);
    }
}
