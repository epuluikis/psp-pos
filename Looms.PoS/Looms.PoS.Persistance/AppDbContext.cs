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

        modelBuilder.Entity<BusinessDao>(x =>
        {
            x.HasKey(b => b.Id);
        });

        modelBuilder.Entity<UserDao>(x =>
        {
            x.HasKey(u => u.Id);

            x.HasOne(u => u.Business)
             .WithMany(b => b.Users)
             .HasForeignKey(u => u.BusinessId)
             .IsRequired();
        });

        modelBuilder.Entity<DiscountDao>(x =>
        {
            x.HasKey(d => d.Id);

            x.HasOne(d => d.Product)
             .WithMany(p => p.Discounts)
             .HasForeignKey(d => d.ProductId)
             .IsRequired(false);

            x.HasOne(d => d.Business)
             .WithMany(b => b.Discounts)
             .HasForeignKey(d => d.BusinessId)
             .IsRequired();
        });

        modelBuilder.Entity<RefundDao>(x =>
        {
            x.HasKey(r => r.Id);

            x.HasOne(r => r.Order)
             .WithMany(o => o.Refunds)
             .HasForeignKey(r => r.OrderId)
             .IsRequired();

            x.HasOne(r => r.Payment)
             .WithMany(r => r.Refunds)
             .HasForeignKey(r => r.PaymentId)
             .IsRequired();

            x.HasOne(r => r.User)
             .WithMany(u => u.Refunds)
             .HasForeignKey(r => r.UserId)
             .IsRequired();
        });

        modelBuilder.Entity<PaymentDao>(x =>
        {
            x.HasKey(p => p.Id);

            x.HasOne(p => p.Order)
             .WithMany(o => o.Payments)
             .HasForeignKey(p => p.OrderId)
             .IsRequired();

            x.HasOne(p => p.GiftCard)
             .WithMany(g => g.Payments)
             .HasForeignKey(p => p.GiftCardId)
             .IsRequired(false);

            x.HasOne(p => p.PaymentTerminal)
             .WithMany(t => t.Payments)
             .HasForeignKey(p => p.PaymentTerminalId)
             .IsRequired(false);
        });

        modelBuilder.Entity<GiftCardDao>(x =>
        {
            x.HasKey(g => g.Id);

            x.HasOne(g => g.IssuedBy)
             .WithMany(u => u.GiftCards)
             .HasForeignKey(g => g.IssuedById)
             .IsRequired();

            x.HasOne(g => g.Business)
             .WithMany(b => b.GiftCards)
             .HasForeignKey(g => g.BusinessId)
             .IsRequired();
        });

        modelBuilder.Entity<ReservationDao>(x =>
        {
            x.HasKey(r => r.Id);

            x.HasOne(r => r.Service)
             .WithMany(s => s.Reservations)
             .HasForeignKey(r => r.ServiceId)
             .IsRequired();

            x.HasOne(r => r.Employee)
             .WithMany(e => e.Reservations)
             .HasForeignKey(r => r.EmployeeId)
             .IsRequired();
        });

        modelBuilder.Entity<ServiceDao>(x =>
        {
            x.HasKey(s => s.Id);

            x.HasOne(s => s.Business)
             .WithMany(b => b.Services)
             .HasForeignKey(s => s.BusinessId)
             .IsRequired();

            x.HasOne(s => s.Tax)
             .WithMany(t => t.Services)
             .HasForeignKey(s => s.TaxId)
             .IsRequired();
        });

        modelBuilder.Entity<TaxDao>(x =>
        {
            x.HasKey(t => t.Id);

            x.HasOne(t => t.Business)
             .WithMany(b => b.Taxes)
             .HasForeignKey(t => t.BusinessId)
             .IsRequired();
        });

        modelBuilder.Entity<ProductDao>(x =>
        {
            x.HasKey(p => p.Id);

            x.HasOne(p => p.Tax)
             .WithMany(t => t.Products)
             .HasForeignKey(p => p.TaxId)
             .IsRequired();

            x.HasOne(p => p.Business)
             .WithMany(b => b.Products)
             .HasForeignKey(p => p.BusinessId)
             .IsRequired();
        });

        modelBuilder.Entity<ProductVariationDao>(x =>
        {
            x.HasKey(pv => pv.Id);

            x.HasOne(pv => pv.Product)
             .WithMany(p => p.Variations)
             .HasForeignKey(p => p.ProductId)
             .IsRequired();
        });

        modelBuilder.Entity<PaymentProviderDao>(x =>
        {
            x.HasKey(pp => pp.Id);

            x.HasOne(pp => pp.Business)
             .WithMany(b => b.PaymentProviders)
             .HasForeignKey(pp => pp.BusinessId)
             .IsRequired();
        });

        modelBuilder.Entity<PaymentProviderDao>(x =>
        {
            x.HasKey(pp => pp.Id);

            x.HasOne(pp => pp.Business)
             .WithMany(b => b.PaymentProviders)
             .HasForeignKey(pp => pp.BusinessId)
             .IsRequired();
        });

        modelBuilder.Entity<PaymentTerminalDao>(x =>
        {
            x.HasKey(pt => pt.Id);

            x.HasOne(pt => pt.PaymentProvider)
             .WithMany(pp => pp.PaymentTerminals)
             .HasForeignKey(pt => pt.PaymentProviderId)
             .IsRequired();
        });

        modelBuilder.Entity<OrderDao>(x =>
        {
            x.HasKey(o => o.Id);

            x.HasOne(o => o.User)
             .WithMany(u => u.Orders)
             .HasForeignKey(o => o.UserId)
             .IsRequired();

            x.HasOne(o => o.Business)
             .WithMany(b => b.Orders)
             .HasForeignKey(o => o.BusinessId)
             .IsRequired();

            x.HasOne(o => o.Discount)
             .WithMany(d => d.Orders)
             .HasForeignKey(o => o.DiscountId)
             .IsRequired(false);
        });

        modelBuilder.Entity<OrderItemDao>(x =>
        {
            x.HasKey(oi => oi.Id);

            x.HasOne(oi => oi.Order)
             .WithMany(o => o.OrderItems)
             .HasForeignKey(oi => oi.OrderId)
             .IsRequired();

            x.HasOne(oi => oi.Product)
             .WithMany(p => p.OrderItems)
             .HasForeignKey(oi => oi.ProductId)
             .IsRequired(false);

            x.HasOne(oi => oi.ProductVariation)
             .WithMany(pv => pv.OrderItems)
             .HasForeignKey(oi => oi.ProductVariationId)
             .IsRequired(false);

            x.HasOne(oi => oi.Service)
             .WithMany(s => s.OrderItems)
             .HasForeignKey(oi => oi.ServiceId)
             .IsRequired(false);

            x.HasOne(oi => oi.Discount)
             .WithMany(d => d.OrderItems)
             .HasForeignKey(oi => oi.DiscountId)
             .IsRequired(false);
        });
    }
}
