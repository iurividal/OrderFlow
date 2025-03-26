using System.ComponentModel.DataAnnotations;

namespace OrderFlow.Shared;

public class ProductModel
{
    public int Id { get; set; }

    [Required(ErrorMessage = "O código é obrigatório")]
    public string Code { get; set; }

    public string Name { get; set; }

    [Required(ErrorMessage = "O preço é obrigatório")]
    [Range(0.01, double.MaxValue, ErrorMessage = "O preço deve ser maior que zero")]
    public decimal Price { get; set; }

    public int Stock { get; set; }

    public int MinimumStock { get; set; }

    public string Image { get; set; }

    public string Description { get; set; }

    public bool Active { get; set; } = true;

    [Required(ErrorMessage = "O tipo de medida é obrigatório")]
    public string UnitType { get; set; }

    public string Createdby { get; set; } = "admin";

    public DateTime CreatedAt { get; set; }
}