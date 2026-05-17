using Microsoft.EntityFrameworkCore;
using OnlineCoffeShop.Web.Data;
using OnlineCoffeShop.Web.Models;
using OnlineCoffeShop.Web.Repositories.Abstractions;

namespace OnlineCoffeShop.Web.Repositories;

public class DbCartRepository : ICartRepository
{
    private const string SessionAnchorKey = "cart_anchor";
    private const string PromoKey = "promo";
    private const decimal PromoDiscountAmount = 200m;

    private readonly AppDbContext _db;
    private readonly IHttpContextAccessor _http;
    private readonly IProductRepository _product;

    public DbCartRepository(AppDbContext db, IHttpContextAccessor http, IProductRepository product)
    {
        _db = db;
        _http = http;
        _product = product;
    }

    private ISession Session => _http.HttpContext!.Session;

    public List<CartItem> GetItems()
    {
        var cart = GetOrCreateCart(out var created);
        if (created)
        {
            SeedInitialItems(cart);
            _db.SaveChanges();
        }

        return cart.Items.ToList();
    }

    public List<CartLine> GetLines()
    {
        return GetItems()
            .Select(i => new { i, p = _product.Find(i.ProductId) })
            .Where(x => x.p is not null)
            .Select(x => new CartLine { Product = x.p!, Qty = x.i.Qty })
            .ToList();
    }

    public int CountUnits() => GetItems().Sum(i => i.Qty);

    public decimal Subtotal() => GetLines().Sum(l => l.LineTotal);

    public void Add(Guid productId, int qty = 1)
    {
        var cart = GetOrCreateCart(out _);
        var ex = cart.Items.FirstOrDefault(i => i.ProductId == productId);
        if (ex is null)
        {
            cart.Items.Add(new CartItem { ProductId = productId, Qty = qty });
        }
        else
        {
            ex.Qty += qty;
        }

        Touch(cart);
        _db.SaveChanges();
    }

    public void Remove(Guid productId)
    {
        var cart = GetOrCreateCart(out _);
        var ex = cart.Items.FirstOrDefault(i => i.ProductId == productId);
        if (ex is null) return;

        cart.Items.Remove(ex);
        _db.CartItems.Remove(ex);
        Touch(cart);
        _db.SaveChanges();
    }

    public void SetQty(Guid productId, int qty)
    {
        if (qty < 1) { Remove(productId); return; }

        var cart = GetOrCreateCart(out _);
        var ex = cart.Items.FirstOrDefault(i => i.ProductId == productId);
        if (ex is null) return;

        ex.Qty = qty;
        Touch(cart);
        _db.SaveChanges();
    }

    public void Clear()
    {
        var cart = GetOrCreateCart(out _);
        _db.CartItems.RemoveRange(cart.Items);
        cart.Items.Clear();
        Touch(cart);
        _db.SaveChanges();
        Session.Remove(PromoKey);
    }

    public string? Promo => Session.GetString(PromoKey);

    public bool PromoApplied => !string.IsNullOrEmpty(Promo);

    public void ApplyPromo(string? code)
    {
        if (string.IsNullOrWhiteSpace(code)) Session.Remove(PromoKey);
        else Session.SetString(PromoKey, code.Trim());
    }

    public decimal PromoDiscount => PromoApplied ? PromoDiscountAmount : 0m;

    private Cart GetOrCreateCart(out bool created)
    {
        var sessionId = EnsureSessionId();

        var cart = _db.Carts
            .Include(c => c.Items)
            .FirstOrDefault(c => c.SessionId == sessionId);

        if (cart is not null)
        {
            created = false;
            return cart;
        }

        cart = new Cart
        {
            Id = Guid.NewGuid(),
            SessionId = sessionId,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow,
        };
        _db.Carts.Add(cart);
        created = true;
        return cart;
    }

    private Guid EnsureSessionId()
    {
        if (Guid.TryParse(Session.GetString(SessionAnchorKey), out var existing))
        {
            return existing;
        }

        var fresh = Guid.NewGuid();
        Session.SetString(SessionAnchorKey, fresh.ToString());
        return fresh;
    }

    private static void SeedInitialItems(Cart cart)
    {
        cart.Items.Add(new CartItem { ProductId = ProductIds.Ethiopia,  Qty = 2 });
        cart.Items.Add(new CartItem { ProductId = ProductIds.Colombia,  Qty = 1 });
        cart.Items.Add(new CartItem { ProductId = ProductIds.HarioMini, Qty = 1 });
    }

    private static void Touch(Cart cart) => cart.UpdatedAt = DateTime.UtcNow;
}
