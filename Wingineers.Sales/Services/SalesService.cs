using Wingineers.Sales.ViewModels;

namespace Wingineers.Sales.Services;

public class SalesService
{
    private readonly ISalesApiClient _api;

    public SalesService(ISalesApiClient api)
    {
        _api = api;
    }

    public async Task<List<SalesPersonListItem>> GetSalesPersonsWithOrderCountAsync()
    {
        var salesPersons = await _api.GetSalesPersonsAsync();
        var orders = await _api.GetOrderLinesAsync();

        var result = new List<SalesPersonListItem>();
        foreach (var person in salesPersons)
        {
            result.Add(new SalesPersonListItem
            {
                Id = person.Id,
                Name = person.Name,
                OrderCount = orders.Count(o => o.SalesPersonId == person.Id),
                OrdersBeforeHire = orders.Count(o => o.SalesPersonId == person.Id && o.IsBeforeHireDate(person))
            });
        }
        return result;
    }

    public async Task<SalesPersonDetailsViewModel?> GetSalesPersonDetailsAsync(int id)
    {
        var salesPersons = await _api.GetSalesPersonsAsync();
        var person = salesPersons.FirstOrDefault(p => p.Id == id);

        if (person == null)
        {
            return null;
        }

        var orders = await _api.GetOrderLinesAsync();

        return new SalesPersonDetailsViewModel
        {
            SalesPerson = person,
            Orders = orders
                .Where(o => o.SalesPersonId == id)
                .OrderByDescending(o => o.GetOrderDate())
                .ToList()
        };
    }

    public async Task<List<MonthlyOrderCount>> GetOrdersPerMonthAsync()
    {
        var orders = await _api.GetOrderLinesAsync();

        if (orders.Count == 0)
        {
            return new List<MonthlyOrderCount>();
        }

        var countPerMonth = orders
            .GroupBy(o => new DateTime(o.GetOrderDate().Year, o.GetOrderDate().Month, 1))
            .ToDictionary(g => g.Key, g => g.Count());

        var firstMonth = countPerMonth.Keys.Min();
        var lastMonth = countPerMonth.Keys.Max();

        var result = new List<MonthlyOrderCount>();
        for (var month = firstMonth; month <= lastMonth; month = month.AddMonths(1))
        {
            result.Add(new MonthlyOrderCount
            {
                Month = month,
                OrderCount = countPerMonth.GetValueOrDefault(month, 0)
            });
        }
        return result;
    }
}