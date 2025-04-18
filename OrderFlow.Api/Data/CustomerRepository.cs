﻿using Dapper;
using OrderFlow.Api.Models;

namespace OrderFlow.Api.Data;

public interface ICustomerRepository
{
    Task<CustomerEntity> GetCustomerByIdAsync(long customerId);
    Task<IEnumerable<CustomerEntity>> GetAllCustomersAsync();
    Task<long> CreateCustomerAsync(CustomerEntity customer);
    Task<bool> UpdateCustomerAsync(CustomerEntity customer);
    Task<bool> DeleteCustomerAsync(long customerId);

    Task<bool> DeleteAddressAsync(long addressId);

    Task<IEnumerable<CustomerEntity>> GetCustomerByNameAsync(string name);
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

        if (customer != null)
        {
            customer.Address = connection.Query<AddressEntity>("Select * from addresses where CustomerId = @Id",
                new { Id = customer.Id }).ToList();
        }

        return customer;
    }

    // Obter todos os clientes
    public async Task<IEnumerable<CustomerEntity>> GetAllCustomersAsync()
    {
        using var connection = _connectionFactory.CreateConnection();
        var query = "SELECT * FROM customers";
        var customers = await connection.QueryAsync<CustomerEntity>(query);

        if (customers != null)
        {
            foreach (var customer in customers)
            {
                customer.Address = connection.Query<AddressEntity>("Select * from addresses where CustomerId = @Id",
                    new { Id = customer.Id }).ToList();
            }
        }

        return customers;
    }

    //OBter um cliente que contain o nome
    public async Task<IEnumerable<CustomerEntity>> GetCustomerByNameAsync(string name)
    {
        using var connection = _connectionFactory.CreateConnection();
        var query = "SELECT * FROM customers WHERE Name ILIKE  @Name";
        var customers =
            await connection.QueryAsync<CustomerEntity>(query, new { Name = "%" + name + "%" });

        if (customers != null)
        {
            foreach (var customer in customers)
            {
                customer.Address = connection.Query<AddressEntity>("Select * from addresses where CustomerId = @Id",
                    new { Id = customer.Id }).ToList();
            }
        }

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
        foreach (var address in customer.Address)
        {
            var addressQuery = @"
                INSERT INTO addresses (CustomerId, Street, Number, Neighborhood, City, State, ZipCode,AddressType)
                VALUES (@CustomerId, @Street, @Number, @Neighborhood, @City, @State, @ZipCode,@AddressType)";
            await connection.ExecuteAsync(addressQuery, new
            {
                CustomerId = customerId,
                address.Street,
                address.Number,
                address.Neighborhood,
                address.City,
                address.State,
                address.ZipCode,
                AddressType = address.AddressType,
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
        
        //verificar quais endereços foram removidos
        var existingAddresses = await connection.QueryAsync<AddressEntity>("Select * from addresses where CustomerId = @Id",
            new { Id = customer.Id });
        var addressesToDelete = existingAddresses.Where(a => !customer.Address.Any(ca => ca.Id == a.Id)).ToList();
        
        foreach (var address in addressesToDelete)
        {
            var deleteAddressQuery = "DELETE FROM addresses WHERE Id = @Id";
            await connection.ExecuteAsync(deleteAddressQuery, new { Id = address.Id });
        }
        
        // Atualizar os endereços do cliente
        foreach (var address in customer.Address)
        {
            var addressQuery = @"
                UPDATE addresses
                SET Street = @Street, Number = @Number, Neighborhood = @Neighborhood, City = @City, State = @State, ZipCode = @ZipCode
                WHERE CustomerId = @CustomerId AND AddressType = @AddressType";
            await connection.ExecuteAsync(addressQuery, new
            {
                CustomerId = customer.Id,
                address.Street,
                address.Number,
                address.Neighborhood,
                address.City,
                address.State,
                address.ZipCode,
                AddressType = address.AddressType,
            });
        }
        
        return rowsAffected > 0;
    }

    //deletar um endereço
    public async Task<bool> DeleteAddressAsync(long addressId)
    {
        using var connection = _connectionFactory.CreateConnection();
        var query = "DELETE FROM addresses WHERE Id = @Id";
        var rowsAffected = await connection.ExecuteAsync(query, new { Id = addressId });
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