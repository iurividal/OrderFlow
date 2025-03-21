namespace OrderFlow.Shared;

public class OrderModel
{
    public string Numero { get; set; }
    public DateTime? Data { get; set; }
    public CustomerModel Cliente { get; set; }
    public List<ItemOrder> Itens { get; set; } = new List<ItemOrder>();
    public decimal ValorTotal { get; set; }
}