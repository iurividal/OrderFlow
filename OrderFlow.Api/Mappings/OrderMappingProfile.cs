using AutoMapper;
using OrderFlow.Api.Models;
using OrderFlow.Shared;

namespace OrderFlow.Api.Mappings;

public class OrderMappingProfile : Profile
{
    public OrderMappingProfile()
    {
        //mapear class OrderModel para OrderEntity
        CreateMap<OrderModel, OrderEntity>()
            .ForMember(dest => dest.Number, opt => opt.MapFrom(src => src.Number))
            .ForMember(dest => dest.Date, opt => opt.MapFrom(src => src.Date))
            .ForMember(dest => dest.Customer, opt => opt.MapFrom(src => src.Customer))
            .ForMember(dest => dest.OrderItems, opt => opt.MapFrom(src => src.Items))
            .ForMember(dest => dest.TotalAmount, opt => opt.MapFrom(src => src.TotalValue));
  
        //mapear class OrderEntity para OrderModel
        CreateMap<OrderEntity, OrderModel>()
            .ForMember(dest => dest.Number, opt => opt.MapFrom(src => src.Number))
            .ForMember(dest => dest.Date, opt => opt.MapFrom(src => src.Date))
            .ForMember(dest => dest.Customer, opt => opt.MapFrom(src => src.Customer))
            .ForMember(dest => dest.Items, opt => opt.MapFrom(src => src.OrderItems))
            .ForMember(dest => dest.TotalValue, opt => opt.MapFrom(src => src.TotalAmount));
    }
}