namespace OrderFlow.Shared;

public class ProductModel
{
    public int Id { get; set; }
    public string Code { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }
    public int Stock { get; set; }

    public int MinimumStock { get; set; }

    public string Image { get; set; }
    
    public string Description { get; set; }
    
    public bool Active { get; set; }
    
    public string UnitType { get; set; }

    public string Createdby { get; set; }

    public DateTime CreatedAt { get; set; }
}