namespace HotelMaintenanceSystem.Models;

public class Hotel
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Location { get; set; }
    public ICollection<Order> Orders { get; set; }
    public ICollection<Item> Items { get; set; }
}