using Looms.PoS.Domain.Daos;
using Microsoft.EntityFrameworkCore;

namespace Looms.PoS.Persistance;

public class AppDbContext : DbContext
{
    public DbSet<BusinessDao> Businesses { get; set; }
    public DbSet<PaymentDao> Payments { get; set; }
    public DbSet<GiftCardDao> GiftCards { get; set; }
    public DbSet<ProductDao> Products { get; set; }
    public DbSet<ProductStockDao> ProductStock { get; set; }
    public DbSet<ProductVariationDao> ProductVariations { get; set; }
    
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<BusinessDao>().HasKey(b => b.Id);
        modelBuilder.Entity<PaymentDao>().HasKey(p => p.Id);
        modelBuilder.Entity<GiftCardDao>().HasKey(p => p.Id);

        modelBuilder.Entity<ProductDao>().HasKey(p => p.Id);
        modelBuilder.Entity<ProductStockDao>().HasKey(p => p.Id);
        modelBuilder.Entity<ProductVariationDao>().HasKey(p => p.Id);

    }
}
