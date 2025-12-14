namespace HotelMaintenanceSystem.Models;

public class SparePart
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int ItemId { get; set; }
    public Item Item { get; set; }
    public int DepartmentId { get; set; }
    public Department Department { get; set; }
    public int Quantity { get; set; }
    public int ReorderThreshold { get; set; }
    public decimal Cost { get; set; }
}