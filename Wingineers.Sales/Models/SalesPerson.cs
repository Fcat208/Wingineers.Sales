using System.Globalization;

namespace Wingineers.Sales.Models;

public class SalesPerson
{
    public int Id { get; set; }
    public string Name { get; set; } = "";
    public string HireDate { get; set; } = "";
    public string Address { get; set; } = "";
    public string City { get; set; } = "";
    public string ZipCode { get; set; } = "";

    public DateTime GetHireDate()
    {
        return DateTime.ParseExact(HireDate, "dd-MM-yyyy", CultureInfo.InvariantCulture);
    }
}