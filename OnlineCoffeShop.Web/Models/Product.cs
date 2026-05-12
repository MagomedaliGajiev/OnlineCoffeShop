namespace OnlineCoffeShop.Web.Models;

public class Product
{
    public Product(Guid id, string name, decimal cost, string description)
    {
        Id = id;
        Name = name;
        Cost = cost;
        Description = description;
    }
    
    public Guid Id { get; init; }

    public string Name { get; private set; } 

    public decimal Cost { get; private set; }

    public string Description { get; private set; }
}
