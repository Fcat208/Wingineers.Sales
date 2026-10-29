using Microsoft.AspNetCore.Mvc;
using Wingineers.Sales.Services;

namespace Wingineers.Sales.Controllers;

public class SalesPersonsController : Controller
{
    private readonly SalesService _service;

    public SalesPersonsController(SalesService service)
    {
        _service = service;
    }

    public async Task<IActionResult> Index()
    {
        var list = await _service.GetSalesPersonsWithOrderCountAsync();
        return View(list);
    }

    public async Task<IActionResult> Details(int id)
    {
        var details = await _service.GetSalesPersonDetailsAsync(id);

        if (details == null)
        {
            return NotFound();
        }

        return View(details);
    }
}