using Elastic.Clients.Elasticsearch;
using Elastic.Transport.Products.Elasticsearch;
using Microsoft.Extensions.Logging;
using OnlineCoffeShop.Web.Models;
using OnlineCoffeShop.Web.Models.Search;
using OnlineCoffeShop.Web.Repositories.Abstractions;

namespace OnlineCoffeShop.Web.Repositories;

/// <summary>Реализация поиска товаров на Elasticsearch.</summary>
public class ElasticProductSearchService : IProductSearchService
{
    private readonly ElasticsearchClient _client;
    private readonly ILogger<ElasticProductSearchService> _logger;
    private readonly string _index;

    public ElasticProductSearchService(
        ElasticsearchClient client,
        ILogger<ElasticProductSearchService> logger,
        string index)
    {
        _client = client;
        _logger = logger;
        _index = index;
    }

    public async Task ReindexAsync(IEnumerable<Product> products)
    {
        // Пересоздаём индекс при каждом старте — данных мало.
        var exists = await _client.Indices.ExistsAsync(_index);
        LogIfFailed(exists, "проверка наличия индекса");

        if (exists.Exists)
        {
            LogIfFailed(await _client.Indices.DeleteAsync(_index), "удаление индекса");
        }

        LogIfFailed(await _client.Indices.CreateAsync(_index), "создание индекса");

        var docs = products.Select(p => new ProductSearchDocument
        {
            Id = p.Id, Name = p.Name,
        });

        var bulk = await _client.IndexManyAsync(docs, _index);
        LogIfFailed(bulk, "индексация товаров");
        if (bulk.IsValidResponse && bulk.Errors)
        {
            // Запрос прошёл, но часть документов не проиндексировалась.
            _logger.LogError(
                "Часть товаров не проиндексирована в Elasticsearch. {DebugInformation}",
                bulk.ApiCallDetails?.DebugInformation);
        }

        // Делаем документы доступными для поиска сразу.
        LogIfFailed(await _client.Indices.RefreshAsync(_index), "обновление индекса");
    }

    public async Task<IReadOnlyList<Guid>> SearchAsync(string query)
    {
        if (string.IsNullOrWhiteSpace(query))
        {
            return Array.Empty<Guid>();
        }

        var response = await _client.SearchAsync<ProductSearchDocument>(s => s
            .Index(_index)
            .Query(q => q
                .Match(m => m
                    .Field(f => f.Name)
                    .Query(query)
                    .Fuzziness(new Fuzziness("AUTO")))));

        if (!response.IsValidResponse)
        {
            _logger.LogError(
                response.ApiCallDetails?.OriginalException,
                "Поиск товаров по запросу «{Query}» завершился ошибкой Elasticsearch. {DebugInformation}",
                query,
                response.ApiCallDetails?.DebugInformation);
            return Array.Empty<Guid>();
        }

        return response.Documents.Select(d => d.Id).ToList();
    }

    /// <summary>Логирует ошибку, если ответ Elasticsearch невалиден.</summary>
    private void LogIfFailed(ElasticsearchResponse response, string operation)
    {
        if (!response.IsValidResponse)
        {
            _logger.LogError(
                response.ApiCallDetails?.OriginalException,
                "Операция «{Operation}» (индекс «{Index}») завершилась ошибкой Elasticsearch. {DebugInformation}",
                operation,
                _index,
                response.DebugInformation);
        }
    }
}