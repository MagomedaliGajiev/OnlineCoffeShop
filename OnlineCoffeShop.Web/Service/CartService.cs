using System.Text.Json;
using OnlineCoffeShop.Web.Models;
using OnlineCoffeShop.Web.Repositories;
using OnlineCoffeShop.Web.Service.Abstractions;

namespace OnlineCoffeShop.Web.Service;

public class CartService : ICartService
{
    private const string CartKey = "cart";
    private const string PromoKey = "promo";
    private const decimal PromoDiscount = 200m;

    private readonly IHttpContextAccessor _http;

    public CartService(IHttpContextAccessor http)
    {
        _http = http;
    }

    private ISession Session => _http.HttpContext!.Session;

    public List<CartItem> GetItems()
    {
        var json = Session.GetString(CartKey);
        if (string.IsNullOrEmpty(json))
        {
            var seed = new List<CartItem>
            {
                new() { ProductId = ProductsRepository.EthiopiaId,  Qty = 2 },
                new() { ProductId = ProductsRepository.ColombiaId,  Qty = 1 },
                new() { ProductId = ProductsRepository.HarioMiniId, Qty = 1 }
            };
            Save(seed);
            return seed;
        }
        return JsonSerializer.Deserialize<List<CartItem>>(json) ?? new();
    }

    public List<CartLine> GetLines()
    {
        return GetItems()
            .Select(i => new { i, p = ProductsRepository.TryGetById(i.ProductId) })
            .Where(x => x.p is not null)
            .Select(x => new CartLine { Product = x.p!, Qty = x.i.Qty })
            .ToList();
    }

    public int CountUnits() => GetItems().Sum(i => i.Qty);
    public decimal Subtotal() => GetLines().Sum(l => l.LineTotal);

    public void Add(Guid productId, int qty = 1)
    {
        var items = GetItems();
        var ex = items.FirstOrDefault(i => i.ProductId == productId);
        if (ex is null) items.Add(new CartItem { ProductId = productId, Qty = qty });
        else ex.Qty += qty;
        Save(items);
    }

    public void Remove(Guid productId)
    {
        var items = GetItems().Where(i => i.ProductId != productId).ToList();
        Save(items);
    }

    public void SetQty(Guid productId, int qty)
    {
        if (qty < 1) { Remove(productId); return; }
        var items = GetItems();
        var ex = items.FirstOrDefault(i => i.ProductId == productId);
        if (ex is not null) { ex.Qty = qty; Save(items); }
    }

    public void Clear()
    {
        Save(new List<CartItem>());
        Session.Remove(PromoKey);
    }

    public string? Promo => Session.GetString(PromoKey);
    public bool PromoApplied => !string.IsNullOrEmpty(Promo);

    public void ApplyPromo(string? code)
    {
        if (string.IsNullOrWhiteSpace(code)) Session.Remove(PromoKey);
        else Session.SetString(PromoKey, code.Trim());
    }

    private void Save(List<CartItem> items)
        => Session.SetString(CartKey, JsonSerializer.Serialize(items));

    public static decimal PromoDiscountValue => PromoDiscount;
}