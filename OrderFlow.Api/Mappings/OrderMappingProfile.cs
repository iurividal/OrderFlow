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
            .ForMember(dest => dest.Payment_Method, opt => opt.MapFrom(src => src.Payment.PaymentMethod))
            .ForMember(dest => dest.Is_Paid, opt => opt.MapFrom(src => src.Payment.IsPaid))
            .ForMember(dest => dest.Date_Payment, opt => opt.MapFrom(src => src.Payment.DatePayment))
            .ForMember(dest => dest.TotalAmount, opt => opt.MapFrom(src => src.TotalValue));

        //mapear class OrderEntity para OrderModel
        CreateMap<OrderEntity, OrderModel>()
            .ForMember(dest => dest.Number, opt => opt.MapFrom(src => src.Number))
            .ForMember(dest => dest.Date, opt => opt.MapFrom(src => src.Date))
            .ForMember(dest => dest.Customer, opt => opt.MapFrom(src => src.Customer))
            .ForMember(dest => dest.Items, opt => opt.MapFrom(src => src.OrderItems))
            .ForPath(dest => dest.Payment.PaymentStatus, opt => opt.MapFrom(src => src.Is_Paid ? "Pago" : "Pedente"))
            .ForPath(dest => dest.Payment.DatePayment, opt => opt.MapFrom(src => src.Date_Payment))
            .ForPath(dest => dest.Payment.IsPaid, opt => opt.MapFrom(src => src.Is_Paid))
            .ForPath(dest => dest.Payment.PaymentMethod, opt => opt.MapFrom(src => src.Payment_Method))
            .ForMember(dest => dest.TotalValue, opt => opt.MapFrom(src => src.TotalAmount));
    }
}