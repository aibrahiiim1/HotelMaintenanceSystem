using System.ComponentModel.DataAnnotations;

namespace HotelMaintenanceSystem.Models;

public class Order
{
    public int Id { get; set; }
    [Required]
    public string Title { get; set; }
    public string Description { get; set; }
    public int HotelId { get; set; }
    public Hotel Hotel { get; set; }
    public int DepartmentId { get; set; }
    public Department Department { get; set; }
    public int? ItemId { get; set; }
    public Item? Item { get; set; }
    public string Location { get; set; }
    public Priority Priority { get; set; }
    public DateTime ExpectedCompletionDate { get; set; }
    public int StatusId { get; set; }
    public OrderStatus Status { get; set; }
    public string CreatedBy { get; set; }
    public DateTime CreatedAt { get; set; }
    public string AssignedTo { get; set; }
    public DateTime? AssignedAt { get; set; }
    public string ImageUrl { get; set; }
    public bool IsGuestRequest { get; set; }
    public DateTime? StartTime { get; set; }
    public DateTime? EndTime { get; set; }
    public decimal? Cost { get; set; }
    public ICollection<OrderHistory> Histories { get; set; }
}

public enum Priority
{
    Low,
    Medium,
    High,
    Urgent
}