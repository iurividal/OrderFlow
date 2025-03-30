using AutoMapper;
using OrderFlow.Api.Data;
using OrderFlow.Api.Models;
using OrderFlow.Shared;

namespace OrderFlow.Api.Services;

public interface IUserService
{
    Task<UserModel> GetUserByEmailAsync(string email);
    Task<UserModel> GetUserByIdAsync(string id);
    Task<List<UserModel>> GetAllUsersAsync();
    Task<bool> CreateUserAsync(UserModel user);
}

public class UseService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;

    public UseService(IUserRepository userRepository, IMapper mapper)
    {
        _userRepository = userRepository;
        _mapper = mapper;
    }

    public async Task<UserModel> GetUserByEmailAsync(string email)
    {
        var userEntity = await _userRepository.GetUserByEmailAsync(email);
        if (userEntity == null)
        {
            return null;
        }

        var userModel = _mapper.Map<UserModel>(userEntity);
        return userModel;
    }

    public async Task<UserModel> GetUserByIdAsync(string id)
    {
        var userEntity = await _userRepository.GetUserByIdAsync(id);
        if (userEntity == null)
        {
            return null;
        }

        var userModel = _mapper.Map<UserModel>(userEntity);
        return userModel;
    }

    public async Task<List<UserModel>> GetAllUsersAsync()
    {
        var userEntities = await _userRepository.GetAllUsersAsync();
        var userModels = _mapper.Map<List<UserModel>>(userEntities);
        return userModels;
    }

    public async Task<bool> CreateUserAsync(UserModel user)
    {
        var userEntity = _mapper.Map<UserEntity>(user);
        return await _userRepository.CreateUserAsync(userEntity);
    }
}