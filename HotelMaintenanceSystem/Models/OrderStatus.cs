using System.ComponentModel.DataAnnotations;

namespace HotelMaintenanceSystem.Models;

public class OrderStatus
{
    public int Id { get; set; }
    
    [Required]
    public string Name { get; set; } = string.Empty;
    
    public bool IsFinal { get; set; }
}