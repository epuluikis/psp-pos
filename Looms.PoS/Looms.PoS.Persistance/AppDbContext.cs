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
    public DbSet<ReservationDao> Reservations { get; set; }
    public DbSet<ServiceDao> Services { get; set; }
    public DbSet<ProductDao> Products { get; set; }
    public DbSet<ProductVariationDao> ProductVariations { get; set; }
    public DbSet<TaxDao> Taxes { get; set; }
    public DbSet<PaymentProviderDao> PaymentProviders { get; set; }
    public DbSet<PaymentTerminalDao> PaymentTerminals { get; set; }

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
        modelBuilder.Entity<ReservationDao>().HasKey(r => r.Id);
        modelBuilder.Entity<ServiceDao>().HasKey(s => s.Id);
        modelBuilder.Entity<TaxDao>().HasKey(t => t.Id);
        modelBuilder.Entity<ProductDao>().HasKey(p => p.Id);
        modelBuilder.Entity<ProductVariationDao>().HasKey(p => p.Id);
        modelBuilder.Entity<PaymentProviderDao>().HasKey(p => p.Id);
        modelBuilder.Entity<PaymentTerminalDao>().HasKey(p => p.Id);

        // Relationships
        modelBuilder.Entity<BusinessDao>()
                    .HasMany(b => b.Users)
                    .WithOne(u => u.Business)
                    .HasForeignKey(u => u.BusinessId)
                    .IsRequired();

        modelBuilder.Entity<PaymentProviderDao>()
                    .HasOne(pp => pp.Business)
                    .WithMany(b => b.PaymentProviders)
                    .HasForeignKey(pp => pp.BusinessId)
                    .IsRequired();

        modelBuilder.Entity<ProductDao>()
            .HasOne(p => p.Tax)
            .WithMany()
            .HasForeignKey(p => p.TaxId)
            .IsRequired(true);

        modelBuilder.Entity<ProductDao>()
            .HasOne(p => p.Business)
            .WithMany()
            .HasForeignKey(p => p.BusinessId)
            .IsRequired(true);

        modelBuilder.Entity<ProductVariationDao>()
            .HasOne(pv => pv.Product)
            .WithMany()
            .HasForeignKey(pv => pv.ProductId)
            .IsRequired(true);

        modelBuilder.Entity<OrderDao>(x => {
            x.HasKey(o => o.Id);

            x.HasOne(o => o.Business)
                .WithMany()
                .HasForeignKey(o => o.BussinessId)
                .IsRequired(true);

            x.HasMany(o => o.OrderItems)
                .WithOne(oi => oi.Order)
                .HasForeignKey(oi => oi.OrderId)
                .IsRequired(true);

            x.HasOne(o => o.Discount)
                .WithMany()
                .HasForeignKey(o => o.DiscountId)
                .OnDelete(DeleteBehavior.SetNull)
                .IsRequired(false);

            x.HasMany(o => o.Payments)
                .WithOne(p => p.Order)
                .HasForeignKey(p => p.OrderId);

            x.HasMany(o => o.Refunds)
                .WithOne(r => r.Order)
                .HasForeignKey(r => r.OrderId)
                .IsRequired(false);
        });

        modelBuilder.Entity<OrderItemDao>(x => {
            x.HasKey(oi => oi.Id);

            x.HasOne(oi => oi.Order)
                .WithMany(o => o.OrderItems)
                .HasForeignKey(oi => oi.OrderId)
                .IsRequired(true);
                
            x.HasOne(oi => oi.Discount)
                .WithMany()
                .HasForeignKey(oi => oi.DiscountId)
                .OnDelete(DeleteBehavior.SetNull)
                .IsRequired(false);
            
            x.HasOne(oi => oi.Product)
                .WithMany()
                .HasForeignKey(oi => oi.ProductId)
                .OnDelete(DeleteBehavior.SetNull)
                .IsRequired(false);

            x.HasOne(oi => oi.ProductVariation)
                .WithMany()
                .HasForeignKey(oi => oi.ProductVariationId)
                .OnDelete(DeleteBehavior.SetNull)
                .IsRequired(false);

            x.HasOne(oi => oi.Service)
                .WithMany()
                .HasForeignKey(oi => oi.ServiceId)
                .OnDelete(DeleteBehavior.SetNull)
                .IsRequired(false); 
        });

        modelBuilder.Entity<PaymentTerminalDao>()
                    .HasOne(pt => pt.PaymentProvider)
                    .WithMany(pp => pp.PaymentTerminals)
                    .HasForeignKey(pt => pt.PaymentProviderId)
                    .IsRequired();

        modelBuilder.Entity<PaymentDao>()
                    .HasOne(p => p.PaymentTerminal)
                    .WithMany(pt => pt.Payments)
                    .HasForeignKey(p => p.PaymentTerminalId)
                    .IsRequired(false);

        modelBuilder.Entity<PaymentDao>()
                    .HasOne(p => p.GiftCard)
                    .WithMany(gc => gc.Payments)
                    .HasForeignKey(p => p.GiftCardId)
                    .IsRequired(false);
                    
        modelBuilder.Entity<ReservationDao>()
                    .HasOne(r => r.Service)
                    .WithMany(s => s.Reservations)
                    .HasForeignKey(r => r.ServiceId)
                    .IsRequired();

        modelBuilder.Entity<ReservationDao>()
                    .HasOne(r => r.Employee)
                    .WithMany(u => u.Reservations)
                    .HasForeignKey(r => r.EmployeeId)
                    .IsRequired();

         modelBuilder.Entity<ServiceDao>()
                    .HasOne(s => s.Tax)
                    .WithMany(t => t.Services)
                    .HasForeignKey(s => s.TaxId)
                    .IsRequired();

        modelBuilder.Entity<ServiceDao>()
                    .HasOne(s => s.Business)
                    .WithMany(b => b.Services)
                    .HasForeignKey(s => s.BusinessId)
                    .IsRequired();
    }

}
