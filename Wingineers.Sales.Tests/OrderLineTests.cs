using Wingineers.Sales.Models;

namespace Wingineers.Sales.Tests;

public class OrderLineTests
{
    [Theory]
    [InlineData("2021-03-15 10:30")]
    [InlineData("15-03-2021 10:30")]
    public void GetOrderDate_ReadsBothFormats(string orderDate)
    {
        var order = new OrderLine { OrderDate = orderDate };

        Assert.Equal(new DateTime(2021, 3, 15, 10, 30, 0), order.GetOrderDate());
    }

    [Fact]
    public void IsBeforeHireDate_OrderBeforeHire_ReturnsTrue()
    {
        var person = new SalesPerson { HireDate = "14-10-2022" };
        var order = new OrderLine { OrderDate = "2022-10-13 23:59" };

        Assert.True(order.IsBeforeHireDate(person));
    }

    [Fact]
    public void IsBeforeHireDate_OrderOnHireDay_ReturnsFalse()
    {
        var person = new SalesPerson { HireDate = "14-10-2022" };
        var order = new OrderLine { OrderDate = "2022-10-14 09:00" };

        Assert.False(order.IsBeforeHireDate(person));
    }
}
