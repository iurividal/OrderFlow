namespace OrderFlow.Shared;

public class ProductModel
{
    public int Id { get; set; }
    public string Codigo { get; set; }
    public string Nome { get; set; }
    public decimal Preco { get; set; }
    public int Estoque { get; set; }
}