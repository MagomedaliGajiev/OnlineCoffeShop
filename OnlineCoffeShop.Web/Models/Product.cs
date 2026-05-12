namespace OnlineCoffeShop.Web.Models;

public class Product
{
    public Guid Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public decimal Cost { get; set; }

    public string Description { get; set; } = string.Empty;
}
