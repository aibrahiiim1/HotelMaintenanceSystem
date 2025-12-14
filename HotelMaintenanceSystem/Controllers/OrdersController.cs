using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using HotelMaintenanceSystem.Data;
using HotelMaintenanceSystem.Models;
using HotelMaintenanceSystem.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace HotelMaintenanceSystem.Controllers;

[Authorize]
public class OrdersController : Controller
{
    private readonly ApplicationDbContext _context;
    private readonly NotificationService _notificationService;
    private readonly UserManager<IdentityUser> _userManager;

    public OrdersController(ApplicationDbContext context, NotificationService notificationService, UserManager<IdentityUser> userManager)
    {
        _context = context;
        _notificationService = notificationService;
        _userManager = userManager;
    }

    public async Task<IActionResult> Index(int? hotelId, int? departmentId, int? statusId, DateTime? startDate, DateTime? endDate)
    {
        var orders = _context.Orders.Include(o => o.Status).Include(o => o.Department).Include(o => o.Hotel).AsQueryable();
        if (hotelId.HasValue) orders = orders.Where(o => o.HotelId == hotelId);
        if (departmentId.HasValue) orders = orders.Where(o => o.DepartmentId == departmentId);
        if (statusId.HasValue) orders = orders.Where(o => o.StatusId == statusId);
        if (startDate.HasValue) orders = orders.Where(o => o.CreatedAt >= startDate);
        if (endDate.HasValue) orders = orders.Where(o => o.CreatedAt <= endDate);
        ViewData["Hotels"] = await _context.Hotels.ToListAsync();
        ViewData["Departments"] = await _context.Departments.ToListAsync();
        ViewData["Statuses"] = await _context.OrderStatuses.ToListAsync();
        return View(await orders.ToListAsync());
    }

    public IActionResult Create()
    {
        ViewData["Departments"] = _context.Departments.ToList();
        ViewData["Hotels"] = _context.Hotels.ToList();
        ViewData["Items"] = _context.Items.ToList();
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(Order order, IFormFile image)
    {
        order.CreatedBy = User.Identity.Name;
        order.CreatedAt = DateTime.Now;
        order.StatusId = 1;
        if (image != null)
        {
            var fileName = Guid.NewGuid() + Path.GetExtension(image.FileName);
            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", fileName);
            Directory.CreateDirectory(Path.GetDirectoryName(path));
            using (var stream = new FileStream(path, FileMode.Create))
            {
                await image.CopyToAsync(stream);
            }
            order.ImageUrl = "/images/" + fileName;
        }
        _context.Orders.Add(order);
        await _context.SaveChangesAsync();
        await _notificationService.SendNotificationAsync($"New order created: {order.Title}");
        return RedirectToAction("Index");
    }

    [HttpPost]
    public async Task<IActionResult> Assign(int id, string assignedTo)
    {
        var order = await _context.Orders.FindAsync(id);
        if (order != null)
        {
            order.AssignedTo = assignedTo;
            order.AssignedAt = DateTime.Now;
            order.StatusId = 3;
            _context.OrderHistories.Add(new OrderHistory { OrderId = id, StatusId = 3, ChangedBy = User.Identity.Name, ChangedAt = DateTime.Now });
            await _context.SaveChangesAsync();
            await _notificationService.SendNotificationAsync($"Order {id} assigned to {assignedTo}");
        }
        return RedirectToAction("Index");
    }

    [HttpPost]
    public async Task<IActionResult> UpdateStatus(int id, int statusId, string notes)
    {
        var order = await _context.Orders.FindAsync(id);
        if (order != null)
        {
            order.StatusId = statusId;
            _context.OrderHistories.Add(new OrderHistory { OrderId = id, StatusId = statusId, ChangedBy = User.Identity.Name, ChangedAt = DateTime.Now, Notes = notes });
            if (statusId == 6) order.EndTime = DateTime.Now; // Completed
            await _context.SaveChangesAsync();
            await _notificationService.SendNotificationAsync($"Order {id} status updated to { (await _context.OrderStatuses.FindAsync(statusId)).Name }");
        }
        return RedirectToAction("Index");
    }

    public async Task<IActionResult> Details(int id)
    {
        var order = await _context.Orders.Include(o => o.Histories).ThenInclude(h => h.Status).FirstOrDefaultAsync(o => o.Id == id);
        return View(order);
    }
}