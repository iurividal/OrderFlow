using System.Net.Http.Json;
using OrderFlow.Shared;

namespace OrderFlow.Client.Services;

public interface IUserService
{
    Task<List<UserModel>> GetAllUsers();
    Task<UserModel> GetUserById(string id);
    Task<bool> CreateUser(UserModel user);
    
    Task<bool> UpdateUserAsync(UserModel user);
}

public class UserService : IUserService
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
        try
        {
            var response = await _httpClient.GetAsync(ENDPOINT);
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<List<UserModel>>();
            }

            return new List<UserModel>();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
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
    
    //atualizar um usuário
    public async Task<bool> UpdateUserAsync(UserModel user)
    {
        var response = await _httpClient.PutAsJsonAsync($"{ENDPOINT}/{user.Id}", user);
        return response.IsSuccessStatusCode;
    }
    
}