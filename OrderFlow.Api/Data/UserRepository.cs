using AutoMapper;
using Dapper;
using OrderFlow.Api.Models;

namespace OrderFlow.Api.Data;

public interface IUserRepository
{
    Task<UserEntity> GetUserByEmailAsync(string email);
    Task<UserEntity> GetUserByIdAsync(int id);
    Task<List<UserEntity>> GetAllUsersAsync();
    Task<bool> CreateUserAsync(UserEntity user);

    Task<bool> UpdateUserAsync(UserEntity user);
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

    public async Task<UserEntity> GetUserByIdAsync(int id)
    {
        try
        {
            using var connection = _dbConnectionFactory.CreateConnection();

            var sql = @"SELECT U.ID, U.USERNAME, 
                         U.EMAIL, U.FULLNAME,U.INACTIVATEDAT, L.ID AS ACCESSLEVEL_ID,L.NAME
                         FROM USERS U INNER JOIN ACCESS_LEVELS L ON U.ACCESSLEVEL = L.ID 
                         WHERE U.ID = @ID";

            var users = connection.QueryFirstOrDefault(sql, new { ID = id });

            var user = new UserEntity
            {
                Id = users.id,
                Username = users.username,
                Email = users.email,
                FullName = users.fullname,
                InactivatedAt = users.inactivatedat,
                AccessLevelNavigation = new AccessLevelEntity
                {
                    Id = Convert.ToInt32(users.accesslevel_id),
                    Name = users.name
                }
            };

            return user;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<List<UserEntity>> GetAllUsersAsync()
    {
        using var connection = _dbConnectionFactory.CreateConnection();

        var sql = @"SELECT u.id,u.email, u.inactivatedat, u.fullname,l.id as AccessLevel_Id,l.""name"",l.id
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
            INSERT INTO Users (username,
                                fullname,
                                email,
                                password,
                                createdby,
                               inactivatedat,
                                accesslevel)
            VALUES (@UserName,@FullName, @Email, @Password, @CreatedBy,@InactivatedAt, @AccessLevel)";

        var result = await connection.ExecuteAsync(query, user);
        return result > 0;
    }

    public async Task<bool> UpdateUserAsync(UserEntity user)
    {
        using var connection = _dbConnectionFactory.CreateConnection();
        var query = @"
            UPDATE Users
            SET username = @UserName,
                fullname = @FullName,
                email = @Email, 
                inactivatedat = @InactivatedAt,
                accesslevel = @AccessLevel
            WHERE id = @Id";

        var result = await connection.ExecuteAsync(query, user);
        return result > 0;
    }
}