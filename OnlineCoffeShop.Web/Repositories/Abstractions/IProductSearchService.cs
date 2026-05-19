using OnlineCoffeShop.Web.Models;

namespace OnlineCoffeShop.Web.Repositories.Abstractions;

/// <summary>Полнотекстовый поиск товаров через Elasticsearch.</summary>
public interface IProductSearchService
{
    /// <summary>Пересоздаёт индекс и загружает в него товары.</summary>
    Task ReindexAsync(IEnumerable<Product> products);

    /// <summary>Возвращает ID товаров по имени, описанию и вкусовым нотам, в порядке релевантности.</summary>
    Task<IReadOnlyList<Guid>> SearchAsync(string query);
}