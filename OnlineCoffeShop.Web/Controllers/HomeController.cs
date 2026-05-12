using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using OnlineCoffeShop.Web.Models;

namespace OnlineCoffeShop.Web.Controllers;

public class HomeController : Controller
{
    private static readonly List<Product> Products =
    [
        new Product
        {
            Id = Guid.NewGuid(),
            Name = "Эспрессо",
            Cost = 150m,
            Description = "Классический крепкий кофе, приготовленный под давлением."
        },
        new Product
        {
            Id = Guid.NewGuid(),
            Name = "Капучино",
            Cost = 220m,
            Description = "Эспрессо с молоком и густой молочной пенкой."
        },
        new Product
        {
            Id = Guid.NewGuid(),
            Name = "Латте",
            Cost = 250m,
            Description = "Нежный кофейный напиток с большим количеством молока."
        }
    ];

    public IActionResult Index()
    {
        return View(Products);
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
