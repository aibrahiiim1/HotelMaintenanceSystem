using Microsoft.AspNetCore.Mvc;
using HotelMaintenanceSystem.Services;

namespace HotelMaintenanceSystem.Controllers;

public class ExportController : Controller
{
    private readonly ExportService _exportService;

    public ExportController(ExportService exportService)
    {
        _exportService = exportService;
    }

    public async Task<IActionResult> ExportExcel()
    {
        var data = await _exportService.ExportOrdersToExcelAsync();
        return File(data, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Orders.xlsx");
    }

    public async Task<IActionResult> ExportPdf()
    {
        var data = await _exportService.ExportOrdersToPdfAsync();
        return File(data, "application/pdf", "Orders.pdf");
    }
}