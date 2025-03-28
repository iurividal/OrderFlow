using AutoMapper;
using Dapper;
using OrderFlow.Api.Models;

namespace OrderFlow.Api.Data;

public interface IProductRepository
{
    Task<List<ProductEntity>> GetAllProductsAsync();

    Task<ProductEntity> GetProductByIdAsync(long productId);

    Task<long> CreateProductAsync(ProductEntity product);

    Task<bool> UpdateProductAsync(ProductEntity product);

    Task<bool> DeleteProductAsync(long productId);

    Task<IEnumerable<ProductEntity>> GetProductByCodeOrNameAsync(string codeOrName);
}

public class ProductRepository : IProductRepository
{
    private readonly IDbConnectionFactory _dbConnectionFactory;

    public ProductRepository(IDbConnectionFactory dbConnectionFactory)
    {
        _dbConnectionFactory = dbConnectionFactory;
    }

    public async Task<List<ProductEntity>> GetAllProductsAsync()
    {
        using var connection = _dbConnectionFactory.CreateConnection();
        return (await connection.QueryAsync<ProductEntity>("SELECT * FROM Products")).ToList();
    }

    public async Task<ProductEntity> GetProductByIdAsync(long productId)
    {
        using var connection = _dbConnectionFactory.CreateConnection();
        var query = "SELECT * FROM Products WHERE Id = @Id";
        return await connection.QueryFirstOrDefaultAsync<ProductEntity>(query, new { Id = productId });
    }
    
    //Consulta produto por codigo ou parte do nome
    public async Task<IEnumerable<ProductEntity>> GetProductByCodeOrNameAsync(string codeOrName)
    {
        using var connection = _dbConnectionFactory.CreateConnection();
        var query = "SELECT * FROM Products WHERE  Code ILIKE  @Code OR Name ILike @Name";
        return await connection.QueryAsync<ProductEntity>(query, new { Code = $"%{codeOrName}%", Name = $"%{codeOrName}%"});
    }

    public async Task<long> CreateProductAsync(ProductEntity product)
    {
        using var connection = _dbConnectionFactory.CreateConnection();
        var query = @"
            INSERT INTO Products (Name, Code ,Price, Stock, MinimumStock, UnitType, Createdby, CreatedAt)
            VALUES (@Name, @Code, @Price, @Stock, @MinimumStock, @UnitType, @Createdby, @CreatedAt)
            RETURNING Id";

        return await connection.ExecuteScalarAsync<long>(query, product);
    }

    public async Task<bool> UpdateProductAsync(ProductEntity product)
    {
        using var connection = _dbConnectionFactory.CreateConnection();
        var query = @"
            UPDATE Products
            SET Name = @Name, Description = @Description, Price = @Price, Stock = @Stock, MinimumStock = @MinimumStock, UnitType = @UnitType
            WHERE Id = @Id";

        return await connection.ExecuteAsync(query, product) > 0;
    }

    public async Task<bool> DeleteProductAsync(long productId)
    {
        using var connection = _dbConnectionFactory.CreateConnection();
        var query = "DELETE FROM Products WHERE Id = @Id";
        return await connection.ExecuteAsync(query, new { Id = productId }) > 0;
    }
}