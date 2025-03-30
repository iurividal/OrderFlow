using System.Net.Http.Json;
using OrderFlow.Shared;

namespace OrderFlow.Client.Services;

public class UserService
{
    private readonly HttpClient _httpClient;
    private const string ENDPOINT = "/users";

    public UserService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    // Método para obter todos os usuários
    public async Task<List<UserModel>> GetAllUsers()
    {
        var response = await _httpClient.GetAsync(ENDPOINT);
        if (response.IsSuccessStatusCode)
        {
            return await response.Content.ReadFromJsonAsync<List<UserModel>>();
        }

        return new List<UserModel>();
    }

    // Método para obter um usuário por ID
    public async Task<UserModel> GetUserById(string id)
    {
        var response = await _httpClient.GetAsync($"{ENDPOINT}/{id}");
        if (response.IsSuccessStatusCode)
        {
            return await response.Content.ReadFromJsonAsync<UserModel>();
        }

        return null;
    }


    // Criação de um novo usuário
    public async Task<bool> CreateUser(UserModel user)
    {
        var response = await _httpClient.PostAsJsonAsync(ENDPOINT, user);
        return response.IsSuccessStatusCode;
    }
}