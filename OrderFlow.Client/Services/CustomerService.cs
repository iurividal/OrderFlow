using System.Net.Http.Json;
using Microsoft.AspNetCore.Components.Forms;
using OrderFlow.Shared;

namespace OrderFlow.Client.Services;

public interface ICustomerService
{
    Task<List<CustomerModel>> GetCustomersAsync();
    Task<CustomerModel> GetCustomersByIdAsync(int id);

    Task AddCustomerAsync(CustomerModel customer);

    Task<bool> UpdateCustomerAsync(CustomerModel customer);

    Task<List<CustomerModel>> GetCustomerByNameAsync(string name);

    Task<string> UploadProfileImageAsync(IBrowserFile fileStream);
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

    public async Task<CustomerModel> GetCustomersByIdAsync(int id)
    {
        return await _httpClient.GetFromJsonAsync<CustomerModel>($"{ENDPOINT}/{id}");
    }

    //BUSCAR CLIENTE POR NOME
    public async Task<List<CustomerModel>> GetCustomerByNameAsync(string name)
    {
        return await _httpClient.GetFromJsonAsync<List<CustomerModel>>(
            $"{ENDPOINT}/filter?name={Uri.EscapeDataString(name)}");
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

    // update Customer
    public async Task<bool> UpdateCustomerAsync(CustomerModel customer)
    {
        var result = await _httpClient.PutAsJsonAsync(ENDPOINT, customer);

        if (!result.IsSuccessStatusCode)
        {
            throw new Exception("Failed to update customer");
        }

        return true;
    }

    public async Task<string> UploadProfileImageAsync(IBrowserFile fileStream)
    {
        var content = new MultipartFormDataContent();
        var fileContent = new StreamContent(fileStream.OpenReadStream(maxAllowedSize: 10 * 1024 * 1024));
        fileContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue(fileStream.ContentType);
        content.Add(fileContent, "file", fileStream.Name);

        var response = await _httpClient.PostAsync($"{ENDPOINT}/upload/photo", content);

        if (!response.IsSuccessStatusCode)
        {
            throw new Exception("Failed to upload profile image");
        }

        return await response.Content.ReadAsStringAsync();
    }
}