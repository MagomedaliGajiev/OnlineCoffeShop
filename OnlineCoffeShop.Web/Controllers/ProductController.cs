using Microsoft.AspNetCore.Mvc;
using OnlineCoffeShop.Web.Models;
using OnlineCoffeShop.Web.Repositories;
using OnlineCoffeShop.Web.Service.Abstractions;

namespace OnlineCoffeShop.Web.Controllers;

public class ProductController : Controller
{
    private readonly IFavoritesService _fav;

    public ProductController(IFavoritesService fav)
    {
        _fav = fav;
    }

    [Route("/product/{slug}")]
    public IActionResult Details(string slug)
    {
        var product = ProductsRepository.TryGetBySlug(slug) ?? ProductsRepository.GetAll()[0];

        var vm = new ProductDetailsViewModel
        {
            Product = product,
            IsFav = _fav.IsFav(product.Id),
        };

        return View(vm);
    }
}
