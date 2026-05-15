namespace OnlineCoffeShop.Web.Models;

public class Cart
{
    public Guid Id { get; set; }

    public string? UserId { get; set; }

    public Guid? SessionId { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

    public List<CartItem> Items { get; set; } = new();
}

public class CartItem
{
    public Guid Id { get; set; }

    public Guid CartId { get; set; }

    public Cart Cart { get; set; } = null!;

    public Guid ProductId { get; set; }

    public Product Product { get; set; } = null!;

    public int Qty { get; set; }
}

public class CartLine
{
    public Product Product { get; init; } = null!;

    public int Qty { get; init; }

    public decimal LineTotal => Product.Price * Qty;

    public decimal LineOldTotal => (Product.OldPrice ?? Product.Price) * Qty;
}
