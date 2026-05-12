using Microsoft.AspNetCore.Mvc;
using OnlineCoffeShop.Web.Models;
using OnlineCoffeShop.Web.Repositories;

namespace OnlineCoffeShop.Web.Controllers;

public class ProductController : Controller
{
    public IActionResult Index(Guid id)
    {
        var product = ProductsRepository
            .GetAll()
            .FirstOrDefault(p => p.Id == id);

        if (product is null)
        {
            return NotFound();
        }

        return View(product);
    }
}
