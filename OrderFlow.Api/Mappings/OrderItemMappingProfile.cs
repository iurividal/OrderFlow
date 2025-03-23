using AutoMapper;
using OrderFlow.Api.Models;
using OrderFlow.Shared;

namespace OrderFlow.Api.Mappings;

public class OrderItemMappingProfile : Profile
{
    public OrderItemMappingProfile()
    {
        //mapear class ItemOrder para OrderItemEntity
        CreateMap<ItemOrder, OrderItemEntity>()
            .ForMember(dest => dest.Product, opt => opt.MapFrom(src => src.Product))
            .ForMember(dest => dest.Quantity, opt => opt.MapFrom(src => src.Quantity))
            .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Price));
        
        //mapear class OrderItemEntity para ItemOrder
        CreateMap<OrderItemEntity, ItemOrder>()
            .ForMember(dest => dest.Product, opt => opt.MapFrom(src => src.Product))
            .ForMember(dest => dest.Quantity, opt => opt.MapFrom(src => src.Quantity))
            .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Price));
    }
}