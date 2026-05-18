using OnlineCoffeShop.Web.Models;
using OnlineCoffeShop.Web.Repositories.Abstractions;

namespace OnlineCoffeShop.Web.Repositories;

public class InMemoryOrderRepository : IOrderRepository
{
    private readonly object _lock = new();
    private readonly List<Order> _orders;
    private int _seq = 1248;

    public InMemoryOrderRepository()
    {
        _orders = new List<Order>
        {
            new()
            {
                Id = Guid.NewGuid(), Number = "BH-2026-1247", UserId = "_demo",
                PlacedAt = new DateTime(2026, 5, 10), Total = 5860,
                Status = OrderStatus.Pending, Delivery = DeliveryMethod.Courier,
                Items = ListArt(ArtStyle.Dark, ArtStyle.Medium, ArtStyle.Gear),
            },
            new()
            {
                Id = Guid.NewGuid(), Number = "BH-2026-1102", UserId = "_demo",
                PlacedAt = new DateTime(2026, 4, 28), Total = 1640,
                Status = OrderStatus.Delivered, Delivery = DeliveryMethod.Courier,
                Items = ListArt(ArtStyle.Dark, ArtStyle.Medium),
            },
            new()
            {
                Id = Guid.NewGuid(), Number = "BH-2026-0987", UserId = "_demo",
                PlacedAt = new DateTime(2026, 4, 15), Total = 3490,
                Status = OrderStatus.Delivered, Delivery = DeliveryMethod.Courier,
                Items = ListArt(ArtStyle.Gear),
            },
            new()
            {
                Id = Guid.NewGuid(), Number = "BH-2026-0844", UserId = "_demo",
                PlacedAt = new DateTime(2026, 4, 2), Total = 4720,
                Status = OrderStatus.Delivered, Delivery = DeliveryMethod.Courier,
                Items = ListArt(ArtStyle.Medium, ArtStyle.Dark, ArtStyle.Tan, ArtStyle.Gift), },
        };
    }

    public IReadOnlyList<Order> GetForUser(string userId)
    {
        lock (_lock)
        {
            return _orders
                .Where(o => o.UserId == userId || o.UserId == "_demo")
                .OrderByDescending(o => o.PlacedAt)
                .ToList();
        }
    }

    public Order? GetById(Guid id)
    {
        lock (_lock)
        {
            return _orders.FirstOrDefault(o => o.Id == id);
        }
    }

    public Order Place(string userId, IEnumerable<CartLine> lines, decimal total, CheckoutViewModel form)
    {
        lock (_lock)
        {
            _seq++;
            var items = lines.Select(l => new OrderItem
            {
                Name = l.Product.Name,
                Qty = l.Qty,
                Price = l.Product.Price,
                Art = l.Product.Art,
            }).ToList();

            var order = new Order
            {
                Id = Guid.NewGuid(),
                Number = $"BH-2026-{_seq}",
                UserId = userId,
                PlacedAt = DateTime.Now,
                Total = total,
                Status = OrderStatus.Pending,
                Delivery = form.Delivery switch
                {
                    "pickup" => DeliveryMethod.Pickup,
                    "cdek" => DeliveryMethod.Cdek,
                    _ => DeliveryMethod.Courier,
                },
                Items = items,
                CustomerName = $"{form.FirstName} {form.LastName}".Trim(),
                Phone = form.Phone,
                Email = form.Email,
                City = form.City,
                Address = form.Address,
                Apt = form.Apt,
                Entrance = form.Entrance,
                Floor = form.Floor,
                Comment = form.Comment,
                Payment = form.Payment,
            };

            _orders.Insert(0, order);
            return order;
        }
    }

    private static List<OrderItem> ListArt(params ArtStyle[] arts)
        => arts.Select(a => new OrderItem { Art = a, Name = string.Empty, Qty = 1, Price = 0 }).ToList();
}