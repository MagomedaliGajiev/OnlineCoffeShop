using Microsoft.AspNetCore.Mvc;
using OnlineCoffeShop.Web.Areas.Admin.Models; 
using OnlineCoffeShop.Web.Models;
using OnlineCoffeShop.Web.Repositories.Abstractions;

namespace OnlineCoffeShop.Web.Areas.Admin.Controllers;

[Area("Admin")]
public class ProductsController : Controller
{
    private readonly IProductRepository _products;

    public ProductsController(IProductRepository products)
    {
        _products = products;
    }

    // --- Список всех товаров ---
    public IActionResult Index() => View(_products.GetAll);

    // --- Посмотреть подробно ---
    public IActionResult Details(Guid id)
    {
        var product = _products.Find(id);
        return product is null ? NotFound() : View(product);
    }

    // --- Добавить новый товар (показать пустую форму) ---
    [HttpGet]
    public IActionResult Create() => View("Form", new ProductFormViewModel());

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(ProductFormViewModel model)
    {
        if (!ModelState.IsValid)
            return View("Form", model);

        _products.Add(MapToProduct(model, Guid.NewGuid()));
        return RedirectToAction(nameof(Index));
    }

    // --- Редактировать (показать форму с данными товара) ---
    [HttpGet]
    public IActionResult Edit(Guid id)
    {
        var product = _products.Find(id);
        return product is null ? NotFound() : View("Form", MapToViewModel(product));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(ProductFormViewModel model)
    {
        if (!ModelState.IsValid)
            return View("Form", model);

        var existing = _products.Find(model.Id);
        if (existing is null)
            return NotFound();

        _products.Update(MapToProduct(model, model.Id, existing));
        return RedirectToAction(nameof(Index));
    }

    // --- Удалить ---
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Delete(Guid id)
    {
        _products.Delete(id);
        return RedirectToAction(nameof(Index));
    }

    // === Преобразования между ViewModel и моделью (без изменений) ===
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