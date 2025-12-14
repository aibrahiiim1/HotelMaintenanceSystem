using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using HotelMaintenanceSystem.Data;
using HotelMaintenanceSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace HotelMaintenanceSystem.Controllers;

[Authorize]
public class ItemsController : Controller
{
    private readonly ApplicationDbContext _context;

    public ItemsController(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        return View(await _context.Items.Include(i => i.Hotel).ToListAsync());
    }

    public IActionResult Create()
    {
        ViewData["Hotels"] = _context.Hotels.ToList();
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(Item item, IFormFile image)
    {
        if (image != null)
        {
            var fileName = Guid.NewGuid() + Path.GetExtension(image.FileName);
            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", fileName);
            Directory.CreateDirectory(Path.GetDirectoryName(path));
            using (var stream = new FileStream(path, FileMode.Create))
            {
                await image.CopyToAsync(stream);
            }
            item.ImageUrl = "/images/" + fileName;
        }
        _context.Items.Add(item);
        await _context.SaveChangesAsync();
        return RedirectToAction("Index");
    }
}