﻿using Looms.PoS.Domain.Daos;
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
    public DbSet<ProductDao> Products { get; set; }
    public DbSet<ProductVariationDao> ProductVariations { get; set; }
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
        modelBuilder.Entity<TaxDao>().HasKey(x => x.Id);
        modelBuilder.Entity<ProductDao>().HasKey(p => p.Id);
        modelBuilder.Entity<ProductVariationDao>().HasKey(p => p.Id);
        
        // Relationships
        modelBuilder.Entity<BusinessDao>()
            .HasMany(b => b.Users)
            .WithOne(u => u.Business)
            .HasForeignKey(u => u.BusinessId)
            .IsRequired();



    }
}
