using Wingineers.Sales.Models;
using Wingineers.Sales.Services;

namespace Wingineers.Sales.Tests;

public class SalesServiceTests
{
    private static SalesService CreateService(FakeSalesApiClient fake)
    {
        return new SalesService(fake);
    }

    [Fact]
    public async Task OrdersPerMonth_MonthWithoutOrders_IsShownAsZero()
    {
        var fake = new FakeSalesApiClient
        {
            Orders = new List<OrderLine>
            {
                new OrderLine { Id = 1, OrderDate = "2021-01-10 12:00", SalesPersonId = 1 },
                new OrderLine { Id = 2, OrderDate = "2021-03-10 12:00", SalesPersonId = 1 }
            }
        };

        var months = await CreateService(fake).GetOrdersPerMonthAsync();

        Assert.Equal(3, months.Count);
        Assert.Equal(new DateTime(2021, 2, 1), months[1].Month);
        Assert.Equal(0, months[1].OrderCount);
    }

    [Fact]
    public async Task OrdersPerMonth_NoOrders_ReturnsEmptyList()
    {
        var fake = new FakeSalesApiClient();

        var months = await CreateService(fake).GetOrdersPerMonthAsync();

        Assert.Empty(months);
    }

    [Fact]
    public async Task SalesPersonList_CountsOrdersAndOrdersBeforeHire()
    {
        var fake = new FakeSalesApiClient
        {
            SalesPersons = new List<SalesPerson>
            {
                new SalesPerson { Id = 1, Name = "Test Sælger", HireDate = "01-06-2021" }
            },
            Orders = new List<OrderLine>
            {
                new OrderLine { Id = 1, OrderDate = "2021-05-01 10:00", SalesPersonId = 1 },
                new OrderLine { Id = 2, OrderDate = "2021-07-01 10:00", SalesPersonId = 1 },
                new OrderLine { Id = 3, OrderDate = "2021-07-01 10:00", SalesPersonId = 2 }
            }
        };

        var list = await CreateService(fake).GetSalesPersonsWithOrderCountAsync();

        Assert.Equal(2, list[0].OrderCount);
        Assert.Equal(1, list[0].OrdersBeforeHire);
    }

    [Fact]
    public async Task Details_UnknownId_ReturnsNull()
    {
        var fake = new FakeSalesApiClient();

        var details = await CreateService(fake).GetSalesPersonDetailsAsync(999);

        Assert.Null(details);
    }

    [Fact]
    public async Task Details_OrdersAreSortedNewestFirst()
    {
        var fake = new FakeSalesApiClient
        {
            SalesPersons = new List<SalesPerson> { new SalesPerson { Id = 1, HireDate = "01-01-2020" } },
            Orders = new List<OrderLine>
            {
                new OrderLine { Id = 1, OrderDate = "2021-01-01 10:00", SalesPersonId = 1 },
                new OrderLine { Id = 2, OrderDate = "01-03-2021 10:00", SalesPersonId = 1 }
            }
        };

        var details = await CreateService(fake).GetSalesPersonDetailsAsync(1);

        Assert.Equal(2, details!.Orders[0].Id);
    }
}
