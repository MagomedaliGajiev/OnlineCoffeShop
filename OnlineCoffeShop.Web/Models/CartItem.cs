namespace OnlineCoffeShop.Web.Models;

public class CartItem
{
    public Guid ProductId { get; set; }
    
    public int Qty { get; set; }
}

public class CartLine
{
    public Product Product { get; init; } = null!;
    
    public  int Qty { get; init; }
    
    public  decimal LineTotal => Product.Price * Qty;
    
    public decimal LineOldTotal => (Product.OldPrice ?? Product.Price) * Qty;
}