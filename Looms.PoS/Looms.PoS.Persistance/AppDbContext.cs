using Looms.PoS.Domain.Daos;
using Microsoft.EntityFrameworkCore;

namespace Looms.PoS.Persistance;

public class AppDbContext : DbContext
{
    public DbSet<BusinessDao> Businesses { get; set; }
    public DbSet<UserDao> Users { get; set; }
    public DbSet<PaymentDao> Payments { get; set; }
    public DbSet<GiftCardDao> GiftCards { get; set; }

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<BusinessDao>().HasKey(b => b.Id);
        modelBuilder.Entity<UserDao>().HasKey(u => u.Id);
        modelBuilder.Entity<PaymentDao>().HasKey(p => p.Id);
        modelBuilder.Entity<GiftCardDao>().HasKey(p => p.Id);

        // Relationships
        modelBuilder.Entity<BusinessDao>()
            .HasMany(b => b.Users)
            .WithOne(u => u.Business)
            .HasForeignKey(u => u.BusinessId)
            .IsRequired();
    }
}
