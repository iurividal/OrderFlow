namespace OrderFlow.Api.Models;

public class OrderEntity
{
    public long Id { get; set; }               // ID do pedido
    public string Number { get; set; }         // Número do pedido
    public DateTime? Date { get; set; }        // Data do pedido (pode ser nula)
    public long CustomerId { get; set; }       // Chave estrangeira para o cliente
    public decimal TotalAmount { get; set; }   // Valor total do pedido
    
    // Relacionamento com o cliente
    public CustomerEntity Customer { get; set; }
    
    
    // Relacionamento com os itens do pedido
    public List<OrderItemEntity> OrderItems { get; set; } = new List<OrderItemEntity>();
    
    
}

public class OrderItemEntity
{
    public long Id { get; set; }               // ID do item do pedido
    public long OrderId { get; set; }          // Chave estrangeira para o pedido
    public long ProductId { get; set; }        // Chave estrangeira para o produto
    public int Quantity { get; set; }          // Quantidade do produto no pedido
    public decimal Price { get; set; }         // Preço do produto no momento da compra

    // Relacionamento com o pedido
    public OrderEntity Order { get; set; }

    // Relacionamento com o produto
    public ProductEntity Product { get; set; }
}