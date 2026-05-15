using Microsoft.EntityFrameworkCore;
using OnlineCoffeShop.Web.Data;
using OnlineCoffeShop.Web.Service;
using OnlineCoffeShop.Web.Service.Abstractions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseInMemoryDatabase("OnlineCoffeShop"));

builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession();
builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IFavoritesService, FavoritesService>();
builder.Services.AddScoped<ICartService, CartService>();

var app = builder.Build();

app.UseHttpsRedirection();
app.UseRouting();
app.UseSession();

app.MapStaticAssets();

app.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();

app.Run();