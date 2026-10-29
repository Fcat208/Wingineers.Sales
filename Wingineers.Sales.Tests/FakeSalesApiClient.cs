using Wingineers.Sales.Models;
using Wingineers.Sales.Services;

namespace Wingineers.Sales.Tests;

public class FakeSalesApiClient : ISalesApiClient
{
    public List<SalesPerson> SalesPersons { get; set; } = new List<SalesPerson>();
    public List<OrderLine> Orders { get; set; } = new List<OrderLine>();

    public Task<List<SalesPerson>> GetSalesPersonsAsync()
    {
        return Task.FromResult(SalesPersons);
    }

    public Task<List<OrderLine>> GetOrderLinesAsync()
    {
        return Task.FromResult(Orders);
    }
}
