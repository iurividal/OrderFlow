using System.Net.Http.Json;
using OrderFlow.Shared;

namespace OrderFlow.Client.Services;

public interface IProductService
{
    Task<List<ProductModel>> GetProductsAsync();
    Task<ProductModel> GetProductByIdAsync(long id);
    Task<long> CreateProductAsync(ProductModel product);
    Task<bool> DeleteProductAsync(long id);
    Task UpdateProductAsync(ProductModel product);
}

public class ProductService : IProductService
{
    private readonly HttpClient _httpClient;
    private const string ENDPOINT = "/products";

    public ProductService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<List<ProductModel>> GetProductsAsync()
    {
        return await _httpClient.GetFromJsonAsync<List<ProductModel>>(ENDPOINT);
    }

    public async Task<ProductModel> GetProductByIdAsync(long id)
    {
        return await _httpClient.GetFromJsonAsync<ProductModel>($"{ENDPOINT}/{id}");
    }

    public async Task<long> CreateProductAsync(ProductModel product)
    {
        var response = await _httpClient.PostAsJsonAsync(ENDPOINT, product);
     
        return await response.Content.ReadFromJsonAsync<long>();
    }

    public async Task<bool> DeleteProductAsync(long id)
    {
        var response = await _httpClient.DeleteAsync($"{ENDPOINT}/{id}");
        return response.IsSuccessStatusCode;
    }

    public async Task UpdateProductAsync(ProductModel product)
    {
        var response = await _httpClient.PutAsJsonAsync($"{ENDPOINT}/{product.Id}", product);
        response.EnsureSuccessStatusCode();
    }
}