using OnlineCoffeShop.Web.Models;

namespace OnlineCoffeShop.Web.Repositories.Abstractions;

public interface ICartRepository
{
    List<CartItem> GetItems();

    List<CartLine> GetLines();

    int CountUnits();

    decimal Subtotal();

    void Add(Guid productId, int qty = 1);

    void Remove(Guid productId);

    void SetQty(Guid productId, int qty);

    void Clear();

    string? Promo { get; }

    bool PromoApplied { get; }

    decimal PromoDiscount { get; }

    void ApplyPromo(string? code);
}