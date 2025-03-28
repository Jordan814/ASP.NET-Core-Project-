﻿using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Web_App_CarRentingSystem.Data.Models;

namespace Web_App_CarRentingSystem.Data;

public class CarRentingDbContext : IdentityDbContext
{
    
    public CarRentingDbContext(DbContextOptions<CarRentingDbContext> options)
        : base(options)
    {
    }

    public DbSet<Car> Cars { get; init; }

    public DbSet<Category> Categories { get; init; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<Car>()
            .HasOne(c => c.Category)
            .WithMany(c => c.Cars)
            .HasForeignKey(c => c.CategoryId)
            .OnDelete(DeleteBehavior.Restrict);

        base.OnModelCreating(builder);
    }
}
