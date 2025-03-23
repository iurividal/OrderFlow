namespace OrderFlow.Shared;

public class ItemOrder
{
    public ProductModel Product { get; set; }
    public decimal Price { get; set; }

    public decimal Subtotal { get; set; }

    public int Quantity { get; set; }


    public void AtualizarSubtotal()
    {
        Subtotal = Price * Quantity;
    }
}