using OrderFlow.Api.Services;
using OrderFlow.Shared;

namespace OrderFlow.Api.EndPoints;

public static class ProductEndPoints
{
    public static WebApplication MapProductEndpoints(this WebApplication app)
    {
        // GET /products
        app.MapGet("/products", async (IProductService service) =>
        {
            var response = await service.GetAllProductsAsync();

            return Results.Ok(response);
        }).WithTags("Products");

        // GET /products/{id}
        app.MapGet("/products/{id}", async (IProductService service, long id) =>
        {
            var response = await service.GetProductByIdAsync(id);

            if (response is null)
            {
                return Results.NotFound();
            }

            return Results.Ok(response);
        }).WithTags("Products");;

        // POST /products
        app.MapPost("/products", async (IProductService service, ProductModel product) =>
        {
            var response = await service.CreateProductAsync(product);

            return Results.Created($"/products/{response}", response);
        }).WithTags("Products");;

        // DELETE /products/{id}
        app.MapDelete("/products/{id}", async (IProductService service, long id) =>
        {
            var response = await service.DeleteProductAsync(id);

            if (!response)
            {
                return Results.NotFound();
            }

            return Results.NoContent();
        }).WithTags("Products");;

        return app;
    }
}