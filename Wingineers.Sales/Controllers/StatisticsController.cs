using Microsoft.AspNetCore.Mvc;
using Wingineers.Sales.Services;

namespace Wingineers.Sales.Controllers;

public class StatisticsController : Controller
{
    private readonly SalesService _service;

    public StatisticsController(SalesService service)
    {
        _service = service;
    }

    public async Task<IActionResult> Index()
    {
        var months = await _service.GetOrdersPerMonthAsync();
        return View(months);
    }
}