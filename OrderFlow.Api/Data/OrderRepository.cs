using System.Data;
using Dapper;
using OrderFlow.Api.Models;

namespace OrderFlow.Api.Data;

public interface IOrderRepository
{
    Task<OrderEntity> GetOrderByIdAsync(long orderId);
    Task<long> CreateOrderAsync(OrderEntity order);
    Task<bool> UpdateOrderAsync(OrderEntity order);
    Task<bool> DeleteOrderAsync(long orderId);
    Task<IEnumerable<OrderEntity>> GetAllOrdersAsync();
}

public class OrderRepository : IOrderRepository
{
    private readonly IDbConnectionFactory _dbConnectionFactory;

    public OrderRepository(IDbConnectionFactory dbConnectionFactory, ILogger<OrderRepository> logger)
    {
        _dbConnectionFactory = dbConnectionFactory;
    }

    // Método para obter um pedido por ID
    public async Task<OrderEntity> GetOrderByIdAsync(long orderId)
    {
        using var connection = _dbConnectionFactory.CreateConnection();

        var orderQuery = "SELECT * FROM orders WHERE Id = @Id";
        var order = await connection.QueryFirstOrDefaultAsync<OrderEntity>(orderQuery, new { Id = orderId });

        if (order != null)
        {
            var itemsQuery = "SELECT * FROM order_items WHERE OrderId = @OrderId";
            order.OrderItems = (await connection.QueryAsync<OrderItemEntity>(itemsQuery, new { OrderId = orderId }))
                .AsList();
        }

        return order;
    }

    // Método para criar um novo pedido
    public async Task<long> CreateOrderAsync(OrderEntity order)
    {
        var connection = _dbConnectionFactory.CreateConnection();

        var orderQuery = @"
                INSERT INTO orders (Number, Date, CustomerId, TotalAmount)
                VALUES (@Number, @Date, @CustomerId, @TotalAmount)
                 RETURNING Id";

        var orderId = await connection.ExecuteScalarAsync<long>(orderQuery, order);

        // Inserir os itens do pedido
        foreach (var item in order.OrderItems)
        {
            var itemQuery = @"INSERT INTO order_items (OrderId, ProductId, Quantity, Price)
                                VALUES (@OrderId, @ProductId, @Quantity, @Price)";

            item.OrderId = orderId;
            await connection.ExecuteAsync(itemQuery, item);
        }

        return orderId;
    }

    // Método para atualizar um pedido
    public async Task<bool> UpdateOrderAsync(OrderEntity order)
    {
        var connection = _dbConnectionFactory.CreateConnection();

        var orderQuery = @"
                UPDATE orders
                SET Number = @Number, Date = @Date, CustomerId = @CustomerId, TotalAmount = @TotalAmount
                WHERE Id = @Id";


        var rowsAffected = await connection.ExecuteAsync(orderQuery, order);

        // Atualizar os itens do pedido
        foreach (var item in order.OrderItems)
        {
            var itemQuery = @"
                    UPDATE order_items
                    SET ProductId = @ProductId, Quantity = @Quantity, Price = @Price
                    WHERE Id = @Id AND OrderId = @OrderId";

            await connection.ExecuteAsync(itemQuery, item);
        }

        return rowsAffected > 0;
    }

    // Método para excluir um pedido
    public async Task<bool> DeleteOrderAsync(long orderId)
    {
        using var connection = _dbConnectionFactory.CreateConnection();
        // Excluir itens do pedido
        var deleteItemsQuery = "DELETE FROM order_items WHERE OrderId = @OrderId";
        await connection.ExecuteAsync(deleteItemsQuery, new { OrderId = orderId });

        // Excluir o pedido
        var deleteOrderQuery = "DELETE FROM orders WHERE Id = @OrderId";
        var rowsAffected = await connection.ExecuteAsync(deleteOrderQuery, new { OrderId = orderId });

        return rowsAffected > 0;
    }

    // Método para listar todos os pedidos
    public async Task<IEnumerable<OrderEntity>> GetAllOrdersAsync()
    {
        using var connection = _dbConnectionFactory.CreateConnection();
        var ordersQuery = "SELECT * FROM orders";
        var orders = await connection.QueryAsync<OrderEntity>(ordersQuery);

        foreach (var order in orders)
        {
            var itemsQuery = "SELECT * FROM order_items WHERE OrderId = @OrderId";
            order.OrderItems = (await connection.QueryAsync<OrderItemEntity>(itemsQuery, new { OrderId = order.Id }))
                .AsList();
        }

        return orders;
    }
}