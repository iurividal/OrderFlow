using AutoMapper;
using OrderFlow.Api.Models;
using OrderFlow.Shared;

namespace OrderFlow.Api.Mappings;

public class AccesslevelMappingProfile : Profile
{
    public AccesslevelMappingProfile()
    {
        // Mapear AccessLevelEntity para AccesslevelModel
        CreateMap<AccessLevelEntity, AccesslevelModel>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Role, opt => opt.MapFrom(src => src.Name));

        // Mapear AccesslevelModel para AccessLevelEntity
        CreateMap<AccesslevelModel, AccessLevelEntity>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Role));
    }
}