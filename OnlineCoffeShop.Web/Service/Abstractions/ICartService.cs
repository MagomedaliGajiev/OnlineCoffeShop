using OnlineCoffeShop.Web.Models;

namespace OnlineCoffeShop.Web.Service.Abstractions;

public interface ICartService
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
    
    void ApplyPromo(string? code);
}