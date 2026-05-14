using Microsoft.AspNetCore.Mvc;
using OnlineCoffeShop.Web.Models;
using OnlineCoffeShop.Web.Service;
using OnlineCoffeShop.Web.Service.Abstractions;

namespace OnlineCoffeShop.Web.Controllers;

public class CartController : Controller
{
    private readonly ICartService _cart;

    public CartController(ICartService cart) => _cart = cart;
    
    public IActionResult Index()
    {
        var lines = _cart.GetLines();
        var subtotal = lines.Sum(l => l.LineTotal);
        var itemDiscount = lines.Sum(l => l.Product.OldPrice.HasValue ? (l.Product.OldPrice.Value - l.Product.Price) * l.Qty : 0);
        var promoDiscount = _cart.PromoApplied ? CartService.PromoDiscountValue : 0;
        var shipping = subtotal >= 2000 ? 0 : 290;
        var total = Math.Max(0, subtotal - promoDiscount + shipping);

        var vm = new CartViewModel
        {
            Lines = lines,
            Subtotal = subtotal,
            Discount = itemDiscount + promoDiscount,
            Shipping = shipping,
            Total = total,
            PromoApplied = _cart.PromoApplied,
            Promo = _cart.Promo,
            IsAuthenticated = User.Identity?.IsAuthenticated == true
        };
        return View(vm);
    }
    
    [HttpPost, ValidateAntiForgeryToken]
    public IActionResult Add(Guid id, int qty = 1, string? returnUrl = null)
    {
        _cart.Add(id, qty);
        return LocalRedirectOr(returnUrl, "/");
    }

    [HttpPost, ValidateAntiForgeryToken]
    public IActionResult SetQty(Guid id, int qty)
    {
        _cart.SetQty(id, qty);
        return RedirectToAction(nameof(Index));
    }

    [HttpPost, ValidateAntiForgeryToken]
    public IActionResult Remove(Guid id)
    {
        _cart.Remove(id);
        return RedirectToAction(nameof(Index));
    }

    [HttpPost, ValidateAntiForgeryToken]
    public IActionResult ApplyPromo(string? promo)
    {
        _cart.ApplyPromo(promo);
        return RedirectToAction(nameof(Index));
    }

    private IActionResult LocalRedirectOr(string? returnUrl, string fallback)
        => !string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl)
            ? LocalRedirect(returnUrl)
            : LocalRedirect(fallback);
}