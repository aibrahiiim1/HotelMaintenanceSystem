using System.ComponentModel.DataAnnotations;

namespace HotelMaintenanceSystem.Models;

public class Item
{
    public int Id { get; set; }
    
    [Required]
    public string Name { get; set; } = string.Empty;
    
    public string? Category { get; set; }
    public string? Class { get; set; }
    public string? Family { get; set; }
    public ItemStatus Status { get; set; }
    public string? Location { get; set; }
    public string? WarrantyInfo { get; set; }
    public string? Manufacturer { get; set; }
    public string? ImageUrl { get; set; }
    public int HotelId { get; set; }
    public Hotel? Hotel { get; set; }
    public ICollection<Order>? Orders { get; set; }
    public ICollection<SparePart>? SpareParts { get; set; }
}

public enum ItemStatus
{
    Active,
    Broken,
    Maintenance
}