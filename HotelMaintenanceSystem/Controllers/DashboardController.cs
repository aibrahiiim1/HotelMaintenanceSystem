using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using HotelMaintenanceSystem.Data;
using Microsoft.EntityFrameworkCore;

namespace HotelMaintenanceSystem.Controllers;

[Authorize]
public class DashboardController : Controller
{
    private readonly ApplicationDbContext _context;

    public DashboardController(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        var orderStats = await _context.Orders.GroupBy(o => o.Status.Name).Select(g => new { Status = g.Key, Count = g.Count() }).ToListAsync();
        var priorityStats = await _context.Orders.GroupBy(o => o.Priority).Select(g => new { Priority = g.Key.ToString(), Count = g.Count() }).ToListAsync();
        ViewData["OrderStats"] = orderStats;
        ViewData["PriorityStats"] = priorityStats;
        ViewData["TotalOrders"] = await _context.Orders.CountAsync();
        ViewData["CompletedOrders"] = await _context.Orders.CountAsync(o => o.StatusId == 6);
        return View();
    }
}