using Looms.PoS.Domain.Daos;
using Microsoft.EntityFrameworkCore;

namespace Looms.PoS.Persistance;

public class AppDbContext : DbContext
{
    public DbSet<BusinessDao> Businesses { get; set; }
    public DbSet<PaymentDao> Payments { get; set; }
    public DbSet<GiftCardDao> GiftCards { get; set; }
    public DbSet<ReservationDao> Reservations { get; set; }
    public DbSet<ServiceDao> Services { get; set; }

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<BusinessDao>().HasKey(b => b.Id);
        modelBuilder.Entity<PaymentDao>().HasKey(p => p.Id);
        modelBuilder.Entity<GiftCardDao>().HasKey(p => p.Id);
        modelBuilder.Entity<ReservationDao>().HasKey(b => b.Id);
        modelBuilder.Entity<ReservationDao>()
            .HasOne<ServiceDao>()
            .WithMany()
            .HasForeignKey(r => r.ServiceId);
        modelBuilder.Entity<ServiceDao>().HasKey(b => b.Id);
    }

}
