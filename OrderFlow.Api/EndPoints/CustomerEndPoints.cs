﻿using Microsoft.AspNetCore.Http.HttpResults;
using OrderFlow.Api.Services;
using OrderFlow.Shared;

namespace OrderFlow.Api.EndPoints;

public static class CustomerEndPoints
{
    public static WebApplication MapCustomerEndpoints(this WebApplication app)
    {
        // GET /customers
        app.MapGet("/customers", async (ICustomerService service) =>
        {
            var response = await service.GetAllCustomersAsync();

            return Results.Ok(response);
        });
        
        // GET /customers/{id}
        app.MapGet("/customers/{id}", async (ICustomerService service, long id) =>
        {
            var response = await service.GetCustomerByIdAsync(id);

            if (response is null)
            {
                return Results.NotFound();
            }

            return Results.Ok(response);
        });
        
        // POST /customers
        app.MapPost("/customers", async (ICustomerService service, CustomerModel customer) =>
        {
            var response = await service.CreateCustomerAsync(customer);

            return Results.Created($"/customers/{response}", response);
        });

        return app;
    }
}