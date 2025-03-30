using OrderFlow.Api.Services;

namespace OrderFlow.Api.EndPoints;

public static class UserEndPoints
{
    public static WebApplication MapUserEndpoints(this WebApplication app)
    {
        // GET /users
        app.MapGet("/users", async (IUserService service) =>
        {
            var response = await service.GetAllUsersAsync();

            return Results.Ok(response);
        }).WithTags("Users");

        return app;
    }
}