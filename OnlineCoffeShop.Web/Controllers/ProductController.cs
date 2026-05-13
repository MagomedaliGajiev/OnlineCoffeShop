using Microsoft.AspNetCore.Mvc;
using OnlineCoffeShop.Web.Repositories;

namespace OnlineCoffeShop.Web.Controllers;

public class ProductController : Controller
{
    public IActionResult Index(Guid id)
    {
        var product = ProductsRepository.TryGetById(id);

        if (product is null)
        {
            return NotFound();
        }

        return View(product);
    }
}
