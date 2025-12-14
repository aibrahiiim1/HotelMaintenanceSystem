using System.ComponentModel.DataAnnotations;

namespace HotelMaintenanceSystem.Models;

public class Hotel
{
    public int Id { get; set; }
    
    [Required]
    public string Name { get; set; } = string.Empty;
    
    [Required]
    public string Location { get; set; } = string.Empty;
    
    public ICollection<Order>? Orders { get; set; }
    public ICollection<Item>? Items { get; set; }
}