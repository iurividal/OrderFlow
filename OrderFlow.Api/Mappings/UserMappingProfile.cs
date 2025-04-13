using AutoMapper;
using OrderFlow.Api.Extensions;
using OrderFlow.Api.Models;
using OrderFlow.Shared;

namespace OrderFlow.Api.Mappings;

public class UserMappingProfile : Profile
{
    // Mapear UserEntity para UserModel
    public UserMappingProfile()
    {
        CreateMap<UserEntity, UserModel>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id.ToString()))
            .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
            .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => src.FullName))
            .ForMember(dest => dest.Role, opt => opt.MapFrom(src => src.AccessLevelNavigation.Name));
        
        // Mapear UserModel para UserEntity
        CreateMap<UserModel, UserEntity>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src=>src.Id))
            .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
            .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => src.FullName))
            .ForMember(dest => dest.Password, opt => opt.MapFrom(src => src.Password.HashPassword()))
            .ForMember(dest => dest.AccessLevel, opt => opt.MapFrom(src=>src.Role == "Administrador" ? 2 : 1));
    }
    
    
}