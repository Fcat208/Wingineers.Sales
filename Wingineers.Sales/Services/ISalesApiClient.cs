using Wingineers.Sales.Models;

namespace Wingineers.Sales.Services;

public interface ISalesApiClient
{
    Task<List<SalesPerson>> GetSalesPersonsAsync();
    Task<List<OrderLine>> GetOrderLinesAsync();
}