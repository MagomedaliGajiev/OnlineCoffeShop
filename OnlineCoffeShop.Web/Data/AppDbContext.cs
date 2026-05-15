using Microsoft.EntityFrameworkCore;
using OnlineCoffeShop.Web.Models;

namespace OnlineCoffeShop.Web.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<Cart> Carts => Set<Cart>();

    public DbSet<CartItem> CartItems => Set<CartItem>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Cart>(e =>
        {
            e.HasKey(c => c.Id);
            e.Property(c => c.UserId).HasMaxLength(450);
            e.HasIndex(c => c.UserId);
            e.HasIndex(c => c.SessionId);

            e.HasMany(c => c.Items)
                .WithOne(i => i.Cart)
                .HasForeignKey(i => i.CartId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<CartItem>(e =>
        {
            e.HasKey(i => i.Id);
            e.Property(i => i.Qty).IsRequired();
            e.HasIndex(i => new { i.CartId, i.ProductId }).IsUnique();

            e.Ignore(i => i.Product);
        });
    }
}
