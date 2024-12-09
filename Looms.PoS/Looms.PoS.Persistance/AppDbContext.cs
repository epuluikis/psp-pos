using Looms.PoS.Domain.Daos;
using Microsoft.EntityFrameworkCore;

namespace Looms.PoS.Persistance;

public class AppDbContext : DbContext
{
    public DbSet<BusinessDao> Businesses { get; set; }
    public DbSet<UserDao> Users { get; set; }
    public DbSet<DiscountDao> Discounts { get; set; }
    public DbSet<RefundDao> Refunds { get; set; }
    public DbSet<PaymentDao> Payments { get; set; }
    public DbSet<GiftCardDao> GiftCards { get; set; }
    public DbSet<OrderDao> Orders { get; set; }
    public DbSet<OrderItemDao> OrderItems { get; set; }
    public DbSet<TaxDao> Taxes { get; set; }

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<BusinessDao>().HasKey(b => b.Id);
        modelBuilder.Entity<UserDao>().HasKey(u => u.Id);
        modelBuilder.Entity<DiscountDao>().HasKey(x => x.Id);
        modelBuilder.Entity<RefundDao>().HasKey(x => x.Id);
        modelBuilder.Entity<PaymentDao>().HasKey(p => p.Id);
        modelBuilder.Entity<GiftCardDao>().HasKey(p => p.Id);
        modelBuilder.Entity<OrderDao>(x => {
            x.HasKey(o => o.Id);

            x.HasOne(o => o.Business)
                .WithMany()
                .HasForeignKey(o => o.BussinessId)
                .OnDelete(DeleteBehavior.Restrict)
                .IsRequired(true);

            x.HasMany(o => o.OrderItems)
                .WithOne(oi => oi.Order)
                .HasForeignKey(oi => oi.OrderId)
                .OnDelete(DeleteBehavior.Cascade);

            x.HasOne(o => o.Discount)
                .WithMany()
                .HasForeignKey(o => o.DiscountId)
                .OnDelete(DeleteBehavior.SetNull)
                .IsRequired(false);

            x.HasMany(o => o.Payments)
                .WithOne(p => p.Order)
                .HasForeignKey(p => p.OrderId)
                .OnDelete(DeleteBehavior.Cascade);

            x.HasMany(o => o.Refunds)
                .WithOne(r => r.Order)
                .HasForeignKey(r => r.OrderId)
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired(false);
        });


        modelBuilder.Entity<OrderItemDao>(x => {
            x.HasKey(oi => oi.Id);

            x.HasOne(oi => oi.Order)
                .WithMany(o => o.OrderItems)
                .HasForeignKey(oi => oi.OrderId)
                .IsRequired(true)
                .OnDelete(DeleteBehavior.Cascade);

            x.HasOne(oi => oi.Discount)
                .WithMany()
                .HasForeignKey(oi => oi.DiscountId)
                .OnDelete(DeleteBehavior.SetNull)
                .IsRequired(false);
            
// TODO: Uncomment the following lines after ProductDao, VariationDao, and ServiceDao classes are in the project
/*             x.HasOne(oi => oi.Product)
                .WithMany()
                .HasForeignKey(oi => oi.ProductId)
                .OnDelete(DeleteBehavior.SetNull)
                .IsRequired(false);

            x.HasOne(oi => oi.Variation)
                .WithMany()
                .HasForeignKey(oi => oi.VariationId)
                .OnDelete(DeleteBehavior.SetNull)
                .IsRequired(false);

            x.HasOne(oi => oi.Service)
                .WithMany()
                .HasForeignKey(oi => oi.ServiceId)
                .OnDelete(DeleteBehavior.SetNull)
                .IsRequired(false); */
        });

        modelBuilder.Entity<TaxDao>().HasKey(x => x.Id);

        // Relationships
        modelBuilder.Entity<BusinessDao>()
            .HasMany(b => b.Users)
            .WithOne(u => u.Business)
            .HasForeignKey(u => u.BusinessId)
            .IsRequired();
    }
}
