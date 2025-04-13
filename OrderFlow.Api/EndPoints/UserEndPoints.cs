using OrderFlow.Api.Services;
using OrderFlow.Shared;

namespace OrderFlow.Api.EndPoints;

public static class UserEndPoints
{
    public static WebApplication MapUserEndpoints(this WebApplication app)
    {
        // GET /users
        
        var userGroup = app.MapGroup("/users").WithTags("Users");

        userGroup.MapGet("/", async (IUserService service) =>
        {
            var response = await service.GetAllUsersAsync();

            return Results.Ok(response);
        });
        
        // GET /users/{id}
        userGroup.MapGet("/{id}", async (IUserService service, string id) =>
        {
            var response = await service.GetUserByIdAsync(id);

            return response != null ? Results.Ok(response) : Results.NotFound();
        });
        
        //post /users
        userGroup.MapPost("/", async (IUserService service, UserModel user) =>
        {
            var response = await service.CreateUserAsync(user);

            return response ? Results.Created($"/users/{user.Id}", user) : Results.BadRequest();
        });
        
        //put /users
        userGroup.MapPut("/{id}", async (IUserService service, UserModel user,int id) =>
        {
            var response = await service.UpdateUserAsync(user);

            return response ? Results.Ok(user) : Results.BadRequest();
        });
        
        

        return app;
    }
}