namespace OnlineCoffeShop.Web.Models.Search;

/// <summary>Документ товара в индексе Elasticsearch.</summary>
public class ProductSearchDocument
{
    /// <summary>Идентификатор товара (совпадает с Product.Id).</summary>
    public Guid Id { get; set; }

    /// <summary>Имя товара — основное поле для полнотекстового поиска.</summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>Описание товара (Product.Blurb).</summary>
    public string Description { get; set; } = string.Empty;

    /// <summary>Вкусовые ноты товара (Product.Notes).</summary>
    public string[] Notes { get; set; } = Array.Empty<string>();
}