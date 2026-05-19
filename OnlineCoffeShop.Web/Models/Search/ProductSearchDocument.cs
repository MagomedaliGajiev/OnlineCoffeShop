namespace OnlineCoffeShop.Web.Models.Search;

/// <summary>Документ товара в индексе Elasticsearch.</summary>
public class ProductSearchDocument
{
    /// <summary>Идентификатор товара (совпадает с Product.Id).</summary>
    public Guid Id { get; set; }

    /// <summary>Имя товара — основное поле для полнотекстового поиска.</summary>
    public string Name { get; set; } = string.Empty;
}