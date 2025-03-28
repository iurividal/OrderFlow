namespace OrderFlow.Shared;

public class ItemOrder
{
    public long ProdutoId { get; set; }

    public string Code { get; set; }

    public string Name { get; set; }
    public decimal Price { get; set; }
    public decimal Subtotal { get; set; }
    public int Quantity { get; set; }

    public void AtualizarSubtotal()
    {
        Subtotal = Price * Quantity;
    }
}