using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using HotelMaintenanceSystem.Models;

namespace HotelMaintenanceSystem.Data;

public static class DbInitializer
{
    public static async Task Initialize(IServiceProvider serviceProvider)
    {
        using var scope = serviceProvider.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        var userManager = scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();
        var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

        // Ensure database is created
        await context.Database.MigrateAsync();

        // Create roles
        string[] roleNames = { "Admin", "Manager", "Technician", "FrontDesk", "Housekeeping" };
        foreach (var roleName in roleNames)
        {
            if (!await roleManager.RoleExistsAsync(roleName))
            {
                await roleManager.CreateAsync(new IdentityRole(roleName));
            }
        }

        // Create admin user
        var adminEmail = "admin@hotel.com";
        var adminUser = await userManager.FindByEmailAsync(adminEmail);
        if (adminUser == null)
        {
            adminUser = new IdentityUser
            {
                UserName = adminEmail,
                Email = adminEmail,
                EmailConfirmed = true
            };
            await userManager.CreateAsync(adminUser, "Admin@123");
            await userManager.AddToRoleAsync(adminUser, "Admin");
        }

        // Seed sample hotels if none exist
        if (!await context.Hotels.AnyAsync())
        {
            var hotels = new List<Hotel>
            {
                new Hotel { Name = "Grand Hotel Downtown", Location = "123 Main St, City Center" },
                new Hotel { Name = "Luxury Resort & Spa", Location = "456 Beach Road, Coastal Area" },
                new Hotel { Name = "Business Hotel", Location = "789 Corporate Ave, Business District" }
            };
            context.Hotels.AddRange(hotels);
            await context.SaveChangesAsync();
        }

        // Seed sample items if none exist
        if (!await context.Items.AnyAsync())
        {
            var hotel = await context.Hotels.FirstAsync();
            var items = new List<Item>
            {
                new Item
                {
                    Name = "Air Conditioner Unit 101",
                    Category = "HVAC",
                    Class = "Climate Control",
                    Family = "Cooling",
                    Status = ItemStatus.Active,
                    Location = "Room 101",
                    Manufacturer = "CoolAir Inc",
                    WarrantyInfo = "3 years",
                    HotelId = hotel.Id
                },
                new Item
                {
                    Name = "Refrigerator Suite A",
                    Category = "Appliances",
                    Class = "Kitchen",
                    Family = "Cooling",
                    Status = ItemStatus.Active,
                    Location = "Suite A",
                    Manufacturer = "FridgePro",
                    WarrantyInfo = "2 years",
                    HotelId = hotel.Id
                },
                new Item
                {
                    Name = "Elevator Main",
                    Category = "Transportation",
                    Class = "Vertical",
                    Family = "Passenger",
                    Status = ItemStatus.Maintenance,
                    Location = "Main Lobby",
                    Manufacturer = "LiftCo",
                    WarrantyInfo = "5 years",
                    HotelId = hotel.Id
                }
            };
            context.Items.AddRange(items);
            await context.SaveChangesAsync();
        }

        // Seed sample spare parts if none exist
        if (!await context.SpareParts.AnyAsync())
        {
            var item = await context.Items.FirstAsync();
            var department = await context.Departments.FirstAsync(d => d.Name == "Engineering");
            var spareParts = new List<SparePart>
            {
                new SparePart
                {
                    Name = "AC Filter",
                    ItemId = item.Id,
                    DepartmentId = department.Id,
                    Quantity = 50,
                    ReorderThreshold = 10,
                    Cost = 15.50m
                },
                new SparePart
                {
                    Name = "Thermostat Sensor",
                    ItemId = item.Id,
                    DepartmentId = department.Id,
                    Quantity = 20,
                    ReorderThreshold = 5,
                    Cost = 45.00m
                }
            };
            context.SpareParts.AddRange(spareParts);
            await context.SaveChangesAsync();
        }

        // Seed sample orders if none exist
        if (!await context.Orders.AnyAsync())
        {
            var hotel = await context.Hotels.FirstAsync();
            var department = await context.Departments.FirstAsync(d => d.Name == "Engineering");
            var item = await context.Items.FirstAsync();
            var orders = new List<Order>
            {
                new Order
                {
                    Title = "AC not cooling in Room 101",
                    Description = "Guest complaint about AC not working properly",
                    HotelId = hotel.Id,
                    DepartmentId = department.Id,
                    ItemId = item.Id,
                    Location = "Room 101",
                    Priority = Priority.High,
                    ExpectedCompletionDate = DateTime.Now.AddDays(1),
                    StatusId = 1,
                    CreatedBy = adminEmail,
                    CreatedAt = DateTime.Now,
                    IsGuestRequest = true
                },
                new Order
                {
                    Title = "Routine maintenance - Elevator",
                    Description = "Monthly elevator inspection and maintenance",
                    HotelId = hotel.Id,
                    DepartmentId = department.Id,
                    Location = "Main Lobby",
                    Priority = Priority.Medium,
                    ExpectedCompletionDate = DateTime.Now.AddDays(3),
                    StatusId = 2,
                    CreatedBy = adminEmail,
                    CreatedAt = DateTime.Now.AddDays(-2),
                    IsGuestRequest = false
                }
            };
            context.Orders.AddRange(orders);
            await context.SaveChangesAsync();
        }
    }
}
