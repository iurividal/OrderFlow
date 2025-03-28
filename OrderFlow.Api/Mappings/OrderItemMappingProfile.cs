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
            .ForMember(dest => dest.ProductId, opt => opt.MapFrom(src => src.ProdutoId))
            .ForMember(dest => dest.Quantity, opt => opt.MapFrom(src => src.Quantity))
            .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Price));

        //mapear class OrderItemEntity para ItemOrder
        CreateMap<OrderItemEntity, ItemOrder>()
            .ForMember(dest => dest.ProdutoId, opt => opt.MapFrom(src => src.ProductId))
            .ForMember(dest => dest.Code, opt => opt.MapFrom(src => src.Product.Code))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Product.Name))
            .ForMember(dest => dest.Quantity, opt => opt.MapFrom(src => src.Quantity))
            .ForMember(dest => dest.Subtotal, opt => opt.MapFrom(src => src.Price * src.Quantity))
            .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Price));
    }
}