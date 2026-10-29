namespace Wingineers.Sales.ViewModels;

public class SalesPersonListItem
{
    public int Id { get; set; }
    public string Name { get; set; } = "";
    public int OrderCount { get; set; }
    public int OrdersBeforeHire { get; set; }
}