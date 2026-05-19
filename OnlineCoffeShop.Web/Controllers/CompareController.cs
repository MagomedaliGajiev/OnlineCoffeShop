using System.Globalization;
using Microsoft.AspNetCore.Mvc;
using OnlineCoffeShop.Web.Models;
using OnlineCoffeShop.Web.Repositories.Abstractions;

namespace OnlineCoffeShop.Web.Controllers;

public class CompareController : Controller
{
    private readonly ICompareRepository _compare;
    private readonly IProductRepository _products;

    public CompareController(ICompareRepository compare, IProductRepository products)
    {
        _compare = compare;
        _products = products;
    }

    public IActionResult Index(bool diffOnly = false)
    {
        var products = _compare.GetIds()
            .Select(_products.Find)
            .Where(p => p is not null)
            .Select(p => p!)
            .ToList();

        return View(new CompareViewModel
        {
            Products = products,
            Rows = BuildRows(products),
            DiffOnly = diffOnly,
        });
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Toggle(Guid id, string? returnUrl = null)
    {
        if (!_compare.Toggle(id))
            TempData["Toast"] = $"В сравнении можно держать не больше {SessionCompareRepository.MaxItems} товаров";

        return !string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl)
            ? LocalRedirect(returnUrl)
            : RedirectToAction(nameof(Index));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Clear()
    {
        _compare.Clear();
        return RedirectToAction(nameof(Index));
    }

    /// <summary>Строит строки таблицы: одна строка — одна характеристика.</summary>
    private static IReadOnlyList<CompareRow> BuildRows(IReadOnlyList<Product> products)
    {
        if (products.Count == 0)
            return Array.Empty<CompareRow>();

        var ru = new CultureInfo("ru-RU");
        var rows = new List<CompareRow>();

        void Add(string label, Func<Product, string> value)
        {
            var values = products.Select(value).ToList();
            rows.Add(new CompareRow
            {
                Label = label,
                Values = values,
                AllSame = values.Distinct().Count() == 1,
            });
        }

        Add("Цена", p => p.Price.ToString("N0", ru) + " ₽");
        Add("Категория", p => p.Category.ToString());
        Add("Тип", p => string.IsNullOrEmpty(p.Type) ? "—" : p.Type);
        Add("Обжарка", p => p.Roast ?? "—");
        Add("Регион", p => p.Origin ?? "—");
        Add("Вес", p => p.WeightGrams is null ? "—" : p.WeightGrams + " г");
        Add("Рейтинг", p => p.AverageRating.ToString("0.#", CultureInfo.InvariantCulture));
        Add("Отзывов", p => p.ReviewCount.ToString());
        Add("Вкусовые ноты", p => p.Notes is { Length: > 0 } n ? string.Join(", ", n) : "—");

        // дополнительные характеристики из Product.Specs —
        // объединяем ключи всех товаров, чтобы строки совпадали по колонкам
        var specKeys = products.SelectMany(p => p.Specs.Keys).Distinct();
        foreach (var key in specKeys)
            Add(key, p => p.Specs.TryGetValue(key, out var v) ? v : "—");

        return rows;
    }
}