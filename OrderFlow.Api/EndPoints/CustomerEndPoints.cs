﻿using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
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
        }).WithTags("Customers");

        // GET /customers/{id}
        app.MapGet("/customers/{id}", async (ICustomerService service, long id) =>
        {
            var response = await service.GetCustomerByIdAsync(id);

            if (response is null)
            {
                return Results.NotFound();
            }

            return Results.Ok(response);
        }).WithTags("Customers");

        //GET /customers/{name}
        app.MapGet("/customers/filter", async (ICustomerService service, [FromQuery] string name) =>
        {
            var response = await service.GetCustomerByNameAsync(name);

            if (response is null)
            {
                return Results.NotFound();
            }

            return Results.Ok(response);
        }).WithTags("Customers");

        // POST /customers
        app.MapPost("/customers", async (ICustomerService service, CustomerModel customer) =>
        {
            var response = await service.CreateCustomerAsync(customer);

            return Results.Created($"/customers/{response}", response);
        }).WithTags("Customers");

        // PUT /customers/{id}
        app.MapPut("/customers/", async (ICustomerService service, CustomerModel customer) =>
        {
            var response = await service.UpdateCustomerAsync(customer);

            if (!response)
            {
                return Results.NotFound();
            }

            return Results.NoContent();
        }).WithTags("Customers");

        // DELETE /customers/address/{id}
        app.MapDelete("/customers/address/{id}", async (ICustomerService service, long id) =>
        {
            var response = await service.DeleteAddressAsync(id);

            if (!response)
            {
                return Results.NotFound();
            }

            return Results.NoContent();
        }).WithTags("Customers");


        // DELETE /customers/{id}
        app.MapDelete("/customers/{id}", async (ICustomerService service, long id) =>
        {
            var response = await service.DeleteCustomerAsync(id);

            if (!response)
            {
                return Results.NotFound();
            }

            return Results.NoContent();
        }).WithTags("Customers");

        return app;
    }
}