using AutoMapper;
using OrderFlow.Api.Models;
using OrderFlow.Shared;

namespace OrderFlow.Api.Mappings;

public class ProductMappingProfile : Profile
{
    public ProductMappingProfile()
    {
        //mapear class ProductEntity para ProductModel
        CreateMap<ProductEntity, ProductModel>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Code, opt => opt.MapFrom(src => src.Code))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Price))
            .ForMember(dest => dest.MinimumStock, opt => opt.MapFrom(src => src.MinimumStock))
            .ForMember(dest => dest.Createdby, opt => opt.MapFrom(src => src.Createdby))
            .ForMember(dest => dest.Stock, opt => opt.MapFrom(src => src.Stock));

        //mapear class ProductModel para ProductEntity
        CreateMap<ProductModel, ProductEntity>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Code, opt => opt.MapFrom(src => src.Code))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Price))
            .ForMember(dest => dest.MinimumStock, opt => opt.MapFrom(src => src.MinimumStock))
            .ForMember(dest => dest.Createdby, opt => opt.MapFrom(src => src.Createdby))
            .ForMember(dest => dest.Stock, opt => opt.MapFrom(src => src.Stock));
    }
}