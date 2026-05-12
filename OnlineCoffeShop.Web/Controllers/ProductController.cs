using Microsoft.AspNetCore.Mvc;
using OnlineCoffeShop.Web.Models;

namespace OnlineCoffeShop.Web.Controllers;

public class ProductController : Controller
{
    public IActionResult Index(Guid id)
    {
        var product = HomeController.Products.FirstOrDefault(p => p.Id == id);

        if (product is null)
        {
            return NotFound();
        }

        return View(product);
    }
}
