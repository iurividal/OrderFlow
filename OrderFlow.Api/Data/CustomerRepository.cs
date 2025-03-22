using Dapper;
using OrderFlow.Api.Models;

namespace OrderFlow.Api.Data;

public interface ICustomerRepository
{
    Task<CustomerEntity> GetCustomerByIdAsync(long customerId);
    Task<IEnumerable<CustomerEntity>> GetAllCustomersAsync();
    Task<long> CreateCustomerAsync(CustomerEntity customer);
    Task<bool> UpdateCustomerAsync(CustomerEntity customer);
    Task<bool> DeleteCustomerAsync(long customerId);
}

public class CustomerRepository : ICustomerRepository
{
    private readonly IDbConnectionFactory _connectionFactory;
    public CustomerRepository(IDbConnectionFactory connectionFactory)
    {
        _connectionFactory = connectionFactory;
    }
    
    // Obter um cliente por ID
    public async Task<CustomerEntity> GetCustomerByIdAsync(long customerId)
    {
        using var connection = _connectionFactory.CreateConnection();
        var query = "SELECT * FROM customers WHERE Id = @Id";
        var customer = await connection.QueryFirstOrDefaultAsync<CustomerEntity>(query, new { Id = customerId });
        
        if(customer != null)
            customer.Adress = await connection.QueryFirstOrDefaultAsync<List<AdressEntity>>("Select * from addresses where CustomerId = @Id", new { Id = customerId });
        
        
        return customer;
    }
    // Obter todos os clientes
    public async Task<IEnumerable<CustomerEntity>> GetAllCustomersAsync()
    {
        using var connection = _connectionFactory.CreateConnection();
        var query = "SELECT * FROM customers";
        var customers = await connection.QueryAsync<CustomerEntity>(query);
        return customers;
    }
    
    // Criar um novo cliente
    public async Task<long> CreateCustomerAsync(CustomerEntity customer)
    {
        using var connection = _connectionFactory.CreateConnection();
        var query = @"
            INSERT INTO customers (Name, Email, Phone, DocumentId, CreatedAt)
            VALUES (@Name, @Email, @Phone, @DocumentId, @CreatedAt)
            RETURNING Id";
        var customerId = await connection.ExecuteScalarAsync<long>(query, customer);
        
        // Inserir os endereços do cliente
        foreach (var address in customer.Adress)
        {
            var addressQuery = @"
                INSERT INTO addresses (CustomerId, Street, Number, City, State, ZipCode)
                VALUES (@CustomerId, @Street, @Number, @City, @State, @ZipCode)";
            await connection.ExecuteAsync(addressQuery, new
            {
                CustomerId = customerId,
                address.Street,
                address.Number,
                address.City,
                address.State,
                address.ZipCode
            });
        }
        return customerId;
    }
    
    // Atualizar um cliente
    public async Task<bool> UpdateCustomerAsync(CustomerEntity customer)
    {
        using var connection = _connectionFactory.CreateConnection();
        var query = @"
            UPDATE customers
            SET Name = @Name, Email = @Email, Phone = @Phone, DocumentId = @DocumentId
            WHERE Id = @Id";
        var rowsAffected = await connection.ExecuteAsync(query, customer);
        return rowsAffected > 0;
    }
    
    // Deletar um cliente
    public async Task<bool> DeleteCustomerAsync(long customerId)
    {
        using var connection = _connectionFactory.CreateConnection();
        var query = "DELETE FROM customers WHERE Id = @Id";
        var rowsAffected = await connection.ExecuteAsync(query, new { Id = customerId });
        return rowsAffected > 0;
    }
}