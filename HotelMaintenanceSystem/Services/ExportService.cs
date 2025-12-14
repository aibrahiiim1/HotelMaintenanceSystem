using ClosedXML.Excel;
using PdfSharp.Pdf;
using PdfSharp.Drawing;
using HotelMaintenanceSystem.Data;
using Microsoft.EntityFrameworkCore;

namespace HotelMaintenanceSystem.Services;

public class ExportService
{
    private readonly ApplicationDbContext _context;

    public ExportService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<byte[]> ExportOrdersToExcelAsync()
    {
        var orders = await _context.Orders.Include(o => o.Status).ToListAsync();
        using var workbook = new XLWorkbook();
        var worksheet = workbook.Worksheets.Add("Orders");
        worksheet.Cell(1, 1).Value = "ID";
        worksheet.Cell(1, 2).Value = "Title";
        worksheet.Cell(1, 3).Value = "Status";
        worksheet.Cell(1, 4).Value = "Priority";
        worksheet.Cell(1, 5).Value = "Hotel";
        worksheet.Cell(1, 6).Value = "Department";
        for (int i = 0; i < orders.Count; i++)
        {
            worksheet.Cell(i + 2, 1).Value = orders[i].Id;
            worksheet.Cell(i + 2, 2).Value = orders[i].Title;
            worksheet.Cell(i + 2, 3).Value = orders[i].Status.Name;
            worksheet.Cell(i + 2, 4).Value = orders[i].Priority.ToString();
            worksheet.Cell(i + 2, 5).Value = orders[i].Hotel.Name;
            worksheet.Cell(i + 2, 6).Value = orders[i].Department.Name;
        }
        using var stream = new MemoryStream();
        workbook.SaveAs(stream);
        return stream.ToArray();
    }

    public async Task<byte[]> ExportOrdersToPdfAsync()
    {
        var orders = await _context.Orders.Include(o => o.Status).Include(o => o.Hotel).Include(o => o.Department).ToListAsync();
        var document = new PdfDocument();
        var page = document.AddPage();
        var gfx = XGraphics.FromPdfPage(page);
        var font = new XFont("Verdana", 12);
        int y = 50;
        foreach (var order in orders)
        {
            gfx.DrawString($"{order.Id}: {order.Title} - {order.Status.Name} - {order.Priority} - {order.Hotel.Name} - {order.Department.Name}", font, XBrushes.Black, new XRect(50, y, page.Width, 20));
            y += 20;
        }
        using var stream = new MemoryStream();
        document.Save(stream);
        return stream.ToArray();
    }
}