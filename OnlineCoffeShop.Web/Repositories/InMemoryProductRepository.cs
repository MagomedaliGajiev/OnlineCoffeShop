using OnlineCoffeShop.Web.Models;
using OnlineCoffeShop.Web.Repositories.Abstractions;

namespace OnlineCoffeShop.Web.Repositories;

public class InMemoryProductRepository : IProductRepository
{
    private static readonly List<Product> _products = new()
    {
        new Product
        {
            Id = ProductIds.Ethiopia, Slug = "ethiopia-yirgacheffe",
            Name = "Эфиопия Иргачеффе",
            Category = ProductCategory.Coffee, Type = "Зерновой", Roast = "Светлая", Origin = "Эфиопия", WeightGrams = 250,
            Price = 890m, OldPrice = 1050m, AverageRating = 4.9m, ReviewCount = 124, Tag = ProductTag.New, Art = ArtStyle.Dark,
            Notes = new[] { "чёрная смородина", "цитрус", "жасмин", "молочный шоколад" },
            Blurb = "Высокогорный сорт из региона Иргачеффе. Промыт по эфиопской традиции, обжарен в Москве за 3 дня до отправки. Раскрывается в воронке Hario V60 и Aeropress — яркая кислотность, чистое сладкое послевкусие.",
            Specs = new()
            {
                ["Регион"] = "Иргачеффе, Эфиопия",
                ["Обработка"] = "Мытая (washed)",
                ["Сорт"] = "Heirloom",
                ["SCA скоринг"] = "87.5",
                ["Высота"] = "1900–2100 м",
                ["Дата обжарки"] = "08.05.2026",
            },
        },
        new Product
        {
            Id = ProductIds.Colombia, Slug = "colombia-huila",
            Name = "Колумбия Уила",
            Category = ProductCategory.Coffee, Type = "Зерновой", Roast = "Средняя", Origin = "Колумбия", WeightGrams = 250,
            Price = 750m, AverageRating = 4.6m, ReviewCount = 89, Art = ArtStyle.Medium,
            Notes = new[] { "красное яблоко", "карамель", "грецкий орех" },
            Blurb = "Классический колумбийский профиль с балансом сладости и кислотности. Хорошо проявляет себя в эспрессо и капельной заварке.",
            Specs = new()
            {
                ["Регион"] = "Уила, Колумбия",
                ["Обработка"] = "Мытая",
                ["Сорт"] = "Caturra",
                ["SCA скоринг"] = "85.0",
            },
        },
        new Product
        {
            Id = ProductIds.Guatemala, Slug = "guatemala-antigua",
            Name = "Гватемала Антигуа",
            Category = ProductCategory.Coffee, Type = "Зерновой", Roast = "Тёмная", Origin = "Гватемала", WeightGrams = 250,
            Price = 680m, OldPrice = 800m, AverageRating = 4.8m, ReviewCount = 56, Tag = ProductTag.Sale, Art = ArtStyle.Dark,
            Notes = new[] { "тёмный шоколад", "фундук", "тростниковый сахар" },
            Blurb = "Плотное тело, насыщенный вкус, мягкая горечь. Идеален для эспрессо и капучино.",
            Specs = new()
            {
                ["Регион"] = "Антигуа, Гватемала",
                ["Обработка"] = "Мытая",
                ["Сорт"] = "Bourbon",
                ["SCA скоринг"] = "86.0",
            },
        },
        new Product
        {
            Id = ProductIds.Brazil, Slug = "brazil-santos",
            Name = "Бразилия Сантос",
            Category = ProductCategory.Coffee, Type = "Зерновой", Roast = "Средне-тёмная", Origin = "Бразилия", WeightGrams = 250,
            Price = 590m, AverageRating = 4.5m, ReviewCount = 142, Art = ArtStyle.Medium,
            Notes = new[] { "молочный шоколад", "арахис", "ваниль" },
            Blurb = "Мягкий бразилец для повседневного эспрессо. Низкая кислотность, ореховая сладость.",
            Specs = new()
            {
                ["Регион"] = "Сантос, Бразилия",
                ["Обработка"] = "Натуральная",
                ["Сорт"] = "Mundo Novo",
                ["SCA скоринг"] = "83.5",
            },
        },
        new Product
        {
            Id = ProductIds.HarioMini, Slug = "hario-mini",
            Name = "Кофемолка Hario Mini",
            Category = ProductCategory.Gear, Type = "Ручная кофемолка",
            Price = 3490m, AverageRating = 4.9m, ReviewCount = 212, Art = ArtStyle.Gear,
            Blurb = "Компактная ручная кофемолка с керамическими жерновами и регулировкой помола от эспрессо до френч-пресса. Не нагревает зерно, легко разбирается для чистки.",
            Specs = new()
            {
                ["Жернова"] = "Керамические",
                ["Ёмкость"] = "24 г",
                ["Регулировка"] = "Ступенчатая",
                ["Материал"] = "Стекло, нерж. сталь",
            },
        },
        new Product
        {
            Id = ProductIds.Aeropress, Slug = "aeropress",
            Name = "Aeropress Original",
            Category = ProductCategory.Gear, Type = "Заварочное устройство",
            Price = 4990m, AverageRating = 4.9m, ReviewCount = 308, Art = ArtStyle.Gear,
            Blurb = "Лёгкий и быстрый способ заварить чистый и яркий кофе. В комплекте 350 фильтров.",
            Specs = new()
            {
                ["Объём"] = "250 мл",
                ["Материал"] = "Полипропилен",
                ["Фильтры"] = "350 шт. в комплекте",
            },
        },
        new Product
        {
            Id = ProductIds.HarioV60, Slug = "hario-v60",
            Name = "Воронка Hario V60",
            Category = ProductCategory.Gear, Type = "Пуровер",
            Price = 1290m, AverageRating = 4.8m, ReviewCount = 167, Art = ArtStyle.Tan,
            Blurb = "Культовая воронка для заваривания фильтр-кофе. Спиральные рёбра внутри обеспечивают равномерную экстракцию.",
            Specs = new()
            {
                ["Размер"] = "02 (1–4 чашки)",
                ["Материал"] = "Керамика",
                ["Цвет"] = "Белый",
            },
        },
        new Product
        {
            Id = ProductIds.GiftSet, Slug = "gift-tasting",
            Name = "Подарочный набор «Дегустация»",
            Category = ProductCategory.Gift, Type = "4 моносорта × 100 г",
            Price = 2890m, AverageRating = 5.0m, ReviewCount = 41, Tag = ProductTag.New, Art = ArtStyle.Gift,
            Blurb = "Четыре моносорта в подарочной упаковке: Эфиопия, Колумбия, Гватемала, Бразилия. Идеальный подарок ценителю.",
            Specs = new()
            {
                ["Состав"] = "4 × 100 г",
                ["Срок годности"] = "12 мес.",
                ["Упаковка"] = "Подарочная коробка",
            },
        },
    };

    private static readonly List<CategorySummary> _categories = Enum.GetValues<ProductCategory>()
        .Select(c => new CategorySummary
        {
            Id = c,
            Name = c.DisplayName(),
            Count = _products.Count(p => p.Category == c),
        })
        .ToList();

    public IReadOnlyList<Product> GetAll => _products;

    public IReadOnlyList<CategorySummary> Categories => _categories;

    public Product? Find(Guid id) => _products.FirstOrDefault(p => p.Id == id);

    public Product? FindBySlug(string slug) => _products.FirstOrDefault(p => p.Slug == slug);

    public IEnumerable<Product> Query(string? category, string? roast, string? origin, string? sort)
    {
        IEnumerable<Product> q = _products;
        var cat = ProductCategoryExtensions.TryParseSlug(category);
        if (cat is not null)
            q = q.Where(p => p.Category == cat);
        if (!string.IsNullOrEmpty(roast))
            q = q.Where(p => p.Roast == roast);
        if (!string.IsNullOrEmpty(origin))
            q = q.Where(p => p.Origin == origin);

        q = sort switch
        {
            "price-asc" => q.OrderBy(p => p.Price),
            "price-desc" => q.OrderByDescending(p => p.Price),
            "rating" => q.OrderByDescending(p => p.AverageRating),
            _ => q,
        };
        return q;
    }
}