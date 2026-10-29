using System.Globalization;

namespace Wingineers.Sales.Models;

public class OrderLine
{
    public int Id { get; set; }
    public string OrderName { get; set; } = "";
    public double OrderPrice { get; set; }
    public string OrderDate { get; set; } = "";
    public int SalesPersonId { get; set; }

    private static readonly string[] DateFormats = { "yyyy-MM-dd HH:mm", "dd-MM-yyyy HH:mm" };

    public DateTime GetOrderDate()
    {
        return DateTime.ParseExact(OrderDate, DateFormats, CultureInfo.InvariantCulture, DateTimeStyles.None);
    }
    public bool IsBeforeHireDate(SalesPerson salesPerson)
    {
        return GetOrderDate() < salesPerson.GetHireDate();
    }
}