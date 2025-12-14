using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using HotelMaintenanceSystem.Models;

namespace HotelMaintenanceSystem.Data;

public class ApplicationDbContext : IdentityDbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<Hotel> Hotels { get; set; }
    public DbSet<Department> Departments { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderStatus> OrderStatuses { get; set; }
    public DbSet<Item> Items { get; set; }
    public DbSet<SparePart> SpareParts { get; set; }
    public DbSet<OrderHistory> OrderHistories { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        // Seed initial data
        builder.Entity<OrderStatus>().HasData(
            new OrderStatus { Id = 1, Name = "Created", IsFinal = false },
            new OrderStatus { Id = 2, Name = "Awaiting Assignment", IsFinal = false },
            new OrderStatus { Id = 3, Name = "Assigned", IsFinal = false },
            new OrderStatus { Id = 4, Name = "Sent to Contractor", IsFinal = false },
            new OrderStatus { Id = 5, Name = "Scheduled", IsFinal = false },
            new OrderStatus { Id = 6, Name = "Completed", IsFinal = false },
            new OrderStatus { Id = 7, Name = "Finalized", IsFinal = true },
            new OrderStatus { Id = 8, Name = "Canceled", IsFinal = true },
            new OrderStatus { Id = 9, Name = "Unsolved", IsFinal = true }
        );
        builder.Entity<Department>().HasData(
            new Department { Id = 1, Name = "Housekeeping" },
            new Department { Id = 2, Name = "Engineering" },
            new Department { Id = 3, Name = "Security" },
            new Department { Id = 4, Name = "Laundry" },
            new Department { Id = 5, Name = "Front Office" }
        );
    }
}