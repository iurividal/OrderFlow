namespace OrderFlow.Shared;

public class OrderModel
{
    public string Number { get; set; }
    
    public DateTime? Date { get; set; }
    
    public CustomerModel Customer { get; set; }
    public List<ItemOrder> Items { get; set; } = new List<ItemOrder>();
    
    public decimal TotalValue { get; set; }
}