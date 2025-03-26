using System.Net.Http.Json;
using OrderFlow.Shared;

namespace OrderFlow.Client.Services;

public interface IOrderService
{
    Task<List<OrderModel>> GetOrdersAsync();

    Task<OrderModel> GetOrderByIdAsync(long id);

    Task<long> CreateOrderAsync(OrderModel order);

    Task<bool> DeleteOrderAsync(long id);

    Task UpdateOrderAsync(OrderModel order);
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

    public async Task<long> CreateOrderAsync(OrderModel order)
    {
        var response = await _httpClient.PostAsJsonAsync("/orders", order);
        return await response.Content.ReadFromJsonAsync<long>();
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
}