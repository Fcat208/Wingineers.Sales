using Wingineers.Sales.Models;

namespace Wingineers.Sales.ViewModels;

public class SalesPersonDetailsViewModel
{
    public SalesPerson SalesPerson { get; set; } = new SalesPerson();
    public List<OrderLine> Orders { get; set; } = new List<OrderLine>();
    public int OrdersBeforeHire => Orders.Count(o => o.IsBeforeHireDate(SalesPerson));
}