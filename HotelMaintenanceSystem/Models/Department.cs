namespace HotelMaintenanceSystem.Models;

public class Department
{
    public int Id { get; set; }
    public string Name { get; set; }
    public ICollection<Order> Orders { get; set; }
    public ICollection<SparePart> SpareParts { get; set; }
}