﻿using Looms.PoS.Domain.Daos;
using Microsoft.EntityFrameworkCore;

namespace Looms.PoS.Persistance;

public class AppDbContext : DbContext
{
    public DbSet<BusinessDao> Businesses { get; set; }

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<BusinessDao>().HasKey(b => b.Id);
    }
}
