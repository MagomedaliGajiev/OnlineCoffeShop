using Elastic.Clients.Elasticsearch;
using Microsoft.EntityFrameworkCore;
using OnlineCoffeShop.Web.Data;
using OnlineCoffeShop.Web.Repositories;
using OnlineCoffeShop.Web.Repositories.Abstractions;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((context, configuration) =>
    configuration.ReadFrom.Configuration(context.Configuration));

builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseInMemoryDatabase("OnlineCoffeShop"));

builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession();
builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<IProductRepository, InMemoryProductRepository>();
builder.Services.AddScoped<IFavoritesRepository, SessionFavoritesRepository>();
builder.Services.AddScoped<ICompareRepository, SessionCompareRepository>();
builder.Services.AddScoped<ICartRepository, DbCartRepository>();
builder.Services.AddSingleton<IOrderRepository, InMemoryOrderRepository>();
builder.Services.AddSingleton<IRolesRepository, InMemoryRolesRepository>();

builder.Services.AddSingleton(_ =>
{
    var uri = builder.Configuration["Elasticsearch:Uri"] ?? "http://localhost:9202";
    var settings = new ElasticsearchClientSettings(new Uri(uri));
    return new ElasticsearchClient(settings);
});
builder.Services.AddScoped<IProductSearchService>(sp =>
{
    var client = sp.GetRequiredService<ElasticsearchClient>();
    var logger = sp.GetRequiredService<ILogger<ElasticProductSearchService>>();
    var index = builder.Configuration["Elasticsearch:ProductsIndex"] ?? "products";
    return new ElasticProductSearchService(client, logger, index);
});

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var products = scope.ServiceProvider.GetRequiredService<IProductRepository>().GetAll;
    var search = scope.ServiceProvider.GetRequiredService<IProductSearchService>();
    await search.ReindexAsync(products);
}

app.UseHttpsRedirection();
app.UseRouting();
app.UseSession();

app.MapStaticAssets();

app.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();

try
{
    Log.Information("Запуск приложения");
    app.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, "Приложение неожиданно остановилось");
}
finally
{
    Log.CloseAndFlush();
}