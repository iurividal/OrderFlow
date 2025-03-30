using AutoMapper;
using Dapper;
using OrderFlow.Api.Models;

namespace OrderFlow.Api.Data;

public interface IUserRepository
{
    Task<UserEntity> GetUserByEmailAsync(string email);
    Task<UserEntity> GetUserByIdAsync(string id);
    Task<List<UserEntity>> GetAllUsersAsync();
    Task<bool> CreateUserAsync(UserEntity user);
}

public class UserRepository : IUserRepository
{
    private readonly IDbConnectionFactory _dbConnectionFactory;

    public UserRepository(IDbConnectionFactory dbConnectionFactory)
    {
        _dbConnectionFactory = dbConnectionFactory;
    }

    public async Task<UserEntity> GetUserByEmailAsync(string email)
    {
        using var connection = _dbConnectionFactory.CreateConnection();
        var query = "SELECT * FROM Users WHERE Email = @Email";
        return await connection.QueryFirstOrDefaultAsync<UserEntity>(query, new { Email = email });
    }

    public async Task<UserEntity> GetUserByIdAsync(string id)
    {
        using var connection = _dbConnectionFactory.CreateConnection();
        var query = "SELECT * FROM Users WHERE Id = @Id";
        return await connection.QueryFirstOrDefaultAsync<UserEntity>(query, new { Id = id });
    }

    public async Task<List<UserEntity>> GetAllUsersAsync()
    {
        using var connection = _dbConnectionFactory.CreateConnection();

        var sql = @"SELECT u.id,u.""password"",u.email, u.fullname,l.id as AccessLevel_Id,l.""name"",l.id
                         FROM Users u inner join access_levels l on u.accesslevel = l.id";

        var users = connection.Query<UserEntity, AccessLevelEntity, UserEntity>(
            sql,
            (user, accessLevel) =>
            {
                user.AccessLevelNavigation = accessLevel;
                return user;
            },
            splitOn: "AccessLevel_Id"
        ).ToList();

        return users;
    }


    public async Task<bool> CreateUserAsync(UserEntity user)
    {
        using var connection = _dbConnectionFactory.CreateConnection();
        var query = @"
            INSERT INTO Users (Email, Password, FullName, CreatedAt, CreatedBy)
            VALUES (@Email, @Password, @FullName, @CreatedAt, @CreatedBy)";

        var result = await connection.ExecuteAsync(query, user);
        return result > 0;
    }
}