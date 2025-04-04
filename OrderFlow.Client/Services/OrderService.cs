﻿using System.Net.Http.Json;
using System.Text.Json.Serialization;
using OrderFlow.Shared;

namespace OrderFlow.Client.Services;

public interface IOrderService
{
    Task<List<OrderModel>> GetOrdersAsync();

    Task<OrderModel> GetOrderByIdAsync(long id);

    Task<OrderModel> GetOrderByNumberAsync(string number);

    Task<long> CreateOrderAsync(OrderModel order);

    Task<bool> DeleteOrderAsync(long id);

    Task UpdateOrderAsync(OrderModel order);

    Task<bool> UpdatePaymentOrderAsync(OrderModel order);
    
    Task<List<OrderModel>> GetOrdersByCustomerIdAsync(int customerId);
}

public class OrderService : IOrderService
{
    private readonly HttpClient _httpClient;

    public OrderService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<List<OrderModel>> GetOrdersAsync()
    {
        return await _httpClient.GetFromJsonAsync<List<OrderModel>>("/orders");
    }

    public async Task<OrderModel> GetOrderByIdAsync(long id)
    {
        return await _httpClient.GetFromJsonAsync<OrderModel>($"/orders/{id}");
    }
    
    public async Task<List<OrderModel>> GetOrdersByCustomerIdAsync(int customerId)
    {
        return await _httpClient.GetFromJsonAsync<List<OrderModel>>($"/orders/customer/{customerId}");
    }

    //Get Order by Number
    public async Task<OrderModel> GetOrderByNumberAsync(string number)
    {
        return await _httpClient.GetFromJsonAsync<OrderModel>($"/orders/ord-{number}");
    }

    public async Task<long> CreateOrderAsync(OrderModel order)
    {
        var response = await _httpClient.PostAsJsonAsync("/orders", order);

        try
        {
            return await response.Content.ReadFromJsonAsync<long>();
        }
        catch (Exception e)
        {
            throw new Exception(await response.Content.ReadAsStringAsync());
        }
    }

    public async Task<bool> DeleteOrderAsync(long id)
    {
        var response = await _httpClient.DeleteAsync($"/orders/{id}");
        return response.IsSuccessStatusCode;
    }

    public async Task UpdateOrderAsync(OrderModel order)
    {
        var response = await _httpClient.PutAsJsonAsync($"/orders/{order.Number}", order);
        response.EnsureSuccessStatusCode();
    }

    public async Task<bool> UpdatePaymentOrderAsync(OrderModel order)
    {
        var json = System.Text.Json.JsonSerializer.Serialize(order);
        var response = await _httpClient.PutAsJsonAsync($"/orders/payment", order);
        return response.IsSuccessStatusCode;
    }
}