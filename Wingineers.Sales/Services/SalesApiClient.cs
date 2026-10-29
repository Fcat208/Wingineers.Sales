using Wingineers.Sales.Models;

namespace Wingineers.Sales.Services;

public class SalesApiClient : ISalesApiClient
{
    private readonly HttpClient _http;
    private static List<OrderLine>? _savedOrders;

    public SalesApiClient(HttpClient http)
    {
        _http = http;
    }

    public async Task<List<SalesPerson>> GetSalesPersonsAsync()
    {
        return await _http.GetFromJsonAsync<List<SalesPerson>>("SalesPersons") ?? new List<SalesPerson>();
    }

    public async Task<List<OrderLine>> GetOrderLinesAsync()
    {
        if (_savedOrders == null)
        {
            _savedOrders = await _http.GetFromJsonAsync<List<OrderLine>>("Orderlines") ?? new List<OrderLine>();
        }
        return _savedOrders;
    }
}