using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HotelMaintenanceSystem.Models;

public class SparePart
{
    public int Id { get; set; }
    
    [Required]
    public string Name { get; set; } = string.Empty;
    
    public int ItemId { get; set; }
    public Item? Item { get; set; }
    
    public int DepartmentId { get; set; }
    public Department? Department { get; set; }
    
    public int Quantity { get; set; }
    public int ReorderThreshold { get; set; }
    
    [Column(TypeName = "decimal(18,2)")]
    public decimal Cost { get; set; }
}