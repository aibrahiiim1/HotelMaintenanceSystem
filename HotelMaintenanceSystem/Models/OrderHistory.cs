using System.ComponentModel.DataAnnotations;

namespace HotelMaintenanceSystem.Models;

public class OrderHistory
{
    public int Id { get; set; }
    public int OrderId { get; set; }
    public Order Order { get; set; }
    public int StatusId { get; set; }
    public OrderStatus Status { get; set; }
    public string ChangedBy { get; set; }
    public DateTime ChangedAt { get; set; }
    public string Notes { get; set; }
}