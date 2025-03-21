namespace OrderFlow.Shared;

public class ItemOrder
{
    public ProductModel Produto { get; set; }
    public decimal PrecoUnitario { get; set; }
    public int Quantidade { get; set; }
    public decimal Subtotal { get; set; }

    public void AtualizarSubtotal()
    {
        Subtotal = PrecoUnitario * Quantidade;
    }
}