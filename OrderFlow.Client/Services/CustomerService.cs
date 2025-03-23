using System.Net.Http.Json;
using OrderFlow.Shared;

namespace OrderFlow.Client.Services;

public interface ICustomerService
{
    Task<List<CustomerModel>> GetCustomersAsync();

    Task AddCustomerAsync(CustomerModel customer);
}

public class CustomerService : ICustomerService
{
    private readonly HttpClient _httpClient;
    private const string ENDPOINT = "/customers";

    public CustomerService(HttpClient http)
    {
        _httpClient = http;
    }

    public async Task<List<CustomerModel>> GetCustomersAsync()
    {
        return await _httpClient.GetFromJsonAsync<List<CustomerModel>>(ENDPOINT);
    }

    // add new Customer
    public async Task AddCustomerAsync(CustomerModel customer)
    {
        var result = await _httpClient.PostAsJsonAsync(ENDPOINT, customer);

        if (!result.IsSuccessStatusCode)
        {
            throw new Exception("Failed to add customer");
        }
    }
}