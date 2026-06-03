using Microsoft.AspNetCore.Mvc;
using OnlineCoffeShop.Web.Models;
using OnlineCoffeShop.Web.Models.Orders;
using OnlineCoffeShop.Web.Repositories.Abstractions;

namespace OnlineCoffeShop.Web.Controllers;

public class AdminController : Controller
{
    private readonly IProductRepository _products;
    private readonly IOrderRepository _orders;

    public AdminController(IProductRepository products, IOrderRepository orders)
    {
        _products = products;
        _orders = orders;
    }

    // Точка входа кнопки: сразу открываем первый раздел — "Заказы"
    public IActionResult Index() => RedirectToAction(nameof(Orders));

    public IActionResult Orders() => View(_orders.GetAll());

    public IActionResult OrderDetails(Guid id)
    {
        var order = _orders.GetById(id);
        return order is null ? NotFound() : View(order);
    }

    // Приём смены статуса (POST)
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult UpdateOrderStatus(Guid id, OrderStatus status)
    {
        _orders.UpdateStatus(id, status);
        return RedirectToAction(nameof(OrderDetails), new { id }); // PRG-паттерн
    }

    public IActionResult Users() => View();

    public IActionResult Roles() => View();

    // --- Список всех товаров ---
    public IActionResult Products() => View(_products.GetAll);

    // --- Посмотреть подробно ---
    public IActionResult ProductDetails(Guid id)
    {
        var product = _products.Find(id);
        return product is null ? NotFound() : View(product);
    }

    // --- Добавить новый товар (показать пустую форму) ---
    [HttpGet]
    public IActionResult ProductCreate() => View("ProductForm", new ProductFormViewModel());

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult ProductCreate(ProductFormViewModel model)
    {
        if (!ModelState.IsValid)
            return View("ProductForm", model);

        _products.Add(MapToProduct(model, Guid.NewGuid()));
        return RedirectToAction(nameof(Products));
    }

    // --- Редактировать (показать форму с данными товара) ---
    [HttpGet]
    public IActionResult ProductEdit(Guid id)
    {
        var product = _products.Find(id);
        return product is null ? NotFound() : View("ProductForm", MapToViewModel(product));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult ProductEdit(ProductFormViewModel model)
    {
        if (!ModelState.IsValid)
            return View("ProductForm", model);

        var existing = _products.Find(model.Id);
        if (existing is null)
            return NotFound();

        _products.Update(MapToProduct(model, model.Id, existing));
        return RedirectToAction(nameof(Products));
    }

    // --- Удалить ---
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult ProductDelete(Guid id)
    {
        _products.Delete(id);
        return RedirectToAction(nameof(Products));
    }

    // === Преобразования между ViewModel и моделью ===
    private static Product MapToProduct(ProductFormViewModel m, Guid id, Product? existing = null) => new()
    {
        Id = id,
        Slug = m.Slug,
        Name = m.Name,
        Category = m.Category,
        Type = m.Type,
        Roast = string.IsNullOrWhiteSpace(m.Roast) ? null : m.Roast,
        Origin = string.IsNullOrWhiteSpace(m.Origin) ? null : m.Origin,
        WeightGrams = m.WeightGrams,
        Price = m.Price ?? 0,
        OldPrice = m.OldPrice,
        Tag = m.Tag,
        Art = m.Art,
        Blurb = m.Blurb,
        Notes = string.IsNullOrWhiteSpace(m.Notes)
            ? null
            : m.Notes.Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries),
        Specs = ParseSpecs(m.Specs),

        // рейтинг и отзывы admin не вводит — сохраняем при редактировании
        AverageRating = existing?.AverageRating ?? 0m,
        ReviewCount = existing?.ReviewCount ?? 0,
    };

    private static ProductFormViewModel MapToViewModel(Product p) => new()
    {
        Id = p.Id,
        Slug = p.Slug,
        Name = p.Name,
        Category = p.Category,
        Type = p.Type,
        Roast = p.Roast,
        Origin = p.Origin,
        WeightGrams = p.WeightGrams,
        Price = p.Price,
        OldPrice = p.OldPrice,
        Tag = p.Tag,
        Art = p.Art,
        Blurb = p.Blurb,
        Notes = p.Notes is null ? null : string.Join(", ", p.Notes),
        Specs = string.Join('\n', p.Specs.Select(kv => $"{kv.Key}={kv.Value}")),
    };

    private static Dictionary<string, string> ParseSpecs(string? raw)
    {
        var result = new Dictionary<string, string>();
        if (string.IsNullOrWhiteSpace(raw))
            return result;

        foreach (var line in raw.Split('\n', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries))
        {
            var parts = line.Split('=', 2);
            if (parts.Length == 2)
                result[parts[0].Trim()] = parts[1].Trim();
        }

        return result;
    }
}