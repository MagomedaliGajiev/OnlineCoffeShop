using Microsoft.AspNetCore.Mvc;
using OnlineCoffeShop.Web.Models;
using OnlineCoffeShop.Web.Repositories.Abstractions;

namespace OnlineCoffeShop.Web.Controllers;

public class CheckoutController : Controller
{
    private readonly ICartRepository _cart;
    private readonly IOrderRepository _orders;

    public CheckoutController(ICartRepository cart, IOrderRepository orders)
    {
        _cart = cart;
        _orders = orders;
    }

    public IActionResult Index()
    {
        var lines = _cart.GetLines();
        if (lines.Count == 0) return RedirectToAction("Index", "Cart");

        var vm = BuildVm(lines, "courier");
        vm.Email = string.Empty;
        return View(vm);
    }

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Place(CheckoutViewModel form)
    {
        var lines = _cart.GetLines();
        if (lines.Count == 0) return RedirectToAction("Index", "Cart");

        var subtotal = lines.Sum(l => l.LineTotal);
        var shipping = form.Delivery switch
        {
            "pickup" => 0m,
            "cdek"   => 290m,
            _        => subtotal >= 2000 ? 0m : 290m
        };
        var total = subtotal + shipping;

        var order = _orders.Place("fsddsfs", lines, total, form.Delivery);
        _cart.Clear();

        return RedirectToAction("Index", "Success", new { id = order.Id });
    }

    private CheckoutViewModel BuildVm(List<CartLine> lines, string delivery)
    {
        var subtotal = lines.Sum(l => l.LineTotal);
        var discount = lines.Sum(l => l.Product.OldPrice.HasValue ? (l.Product.OldPrice.Value - l.Product.Price) * l.Qty : 0);
        var shipping = delivery switch
        {
            "pickup" => 0m,
            "cdek"   => 290m,
            _        => subtotal >= 2000 ? 0m : 290m
        };
        return new CheckoutViewModel
        {
            Lines = lines,
            Subtotal = subtotal,
            Discount = discount,
            Shipping = shipping,
            Total = subtotal + shipping,
            Delivery = delivery
        };
    }
}