using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using HotelMaintenanceSystem.Data;
using HotelMaintenanceSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace HotelMaintenanceSystem.Controllers;

[Authorize]
public class SparePartsController : Controller
{
    private readonly ApplicationDbContext _context;

    public SparePartsController(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        return View(await _context.SpareParts.Include(s => s.Item).Include(s => s.Department).ToListAsync());
    }

    public IActionResult Create()
    {
        ViewData["Items"] = _context.Items.ToList();
        ViewData["Departments"] = _context.Departments.ToList();
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(SparePart sparePart)
    {
        _context.SpareParts.Add(sparePart);
        await _context.SaveChangesAsync();
        return RedirectToAction("Index");
    }
}