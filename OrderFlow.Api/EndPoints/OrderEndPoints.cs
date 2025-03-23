﻿using OrderFlow.Api.Services;
using OrderFlow.Shared;

namespace OrderFlow.Api.EndPoints;

public static class OrderEndPoints
{
    public static WebApplication MapOrderEndpoints(this WebApplication app)
    {
        // GET /orders
        app.MapGet("/orders", async (IOrderService service) =>
        {
            var response = await service.GetAllOrdersAsync();

            return Results.Ok(response);
        }).WithTags("Orders");
        
        // GET /orders/{id}
        app.MapGet("/orders/{id}", async (IOrderService service, long id) =>
        {
            var response = await service.GetOrderByIdAsync(id);

            if (response is null)
            {
                return Results.NotFound();
            }

            return Results.Ok(response);
        }).WithTags("Orders");
        
        // POST /orders
        app.MapPost("/orders", async (IOrderService service, OrderModel order) =>
        {
            var response = await service.CreateOrderAsync(order);

            return Results.Created($"/orders/{response}", response);
        }).WithTags("Orders");
        
        // DELETE /orders/{id}
        app.MapDelete("/orders/{id}", async (IOrderService service, long id) =>
        {
            var response = await service.DeleteOrderAsync(id);

            if (!response)
            {
                return Results.NotFound();
            }

            return Results.NoContent();
        }).WithTags("Orders");

        return app;
    }
}