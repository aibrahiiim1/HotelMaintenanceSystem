using System.ComponentModel.DataAnnotations;

namespace HotelMaintenanceSystem.Models;

public class Department
{
    public int Id { get; set; }
    
    [Required]
    public string Name { get; set; } = string.Empty;
    
    public ICollection<Order>? Orders { get; set; }
    public ICollection<SparePart>? SpareParts { get; set; }
}