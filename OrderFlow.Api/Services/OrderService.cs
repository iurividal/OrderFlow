using AutoMapper;
using OrderFlow.Api.Data;
using OrderFlow.Api.Models;
using OrderFlow.Shared;

namespace OrderFlow.Api.Services;

public interface IOrderService
{
    Task<List<OrderModel>> GetAllOrdersAsync();

    Task<OrderModel> GetOrderByIdAsync(long orderId);

    Task<long> CreateOrderAsync(OrderModel order);

    Task<bool> UpdateOrderAsync(OrderModel order);

    Task<bool> DeleteOrderAsync(long orderId);
}

public class OrderService : IOrderService
{
    private readonly IOrderRepository _orderRepository;
    private readonly IMapper _mapper;

    public OrderService(IOrderRepository orderRepository, IMapper mapper)
    {
        _orderRepository = orderRepository;
        _mapper = mapper;
    }

    public async Task<List<OrderModel>> GetAllOrdersAsync()
    {
        var ordes = await _orderRepository.GetAllOrdersAsync();
        return _mapper.Map(ordes, new List<OrderModel>());
    }

    public async Task<OrderModel> GetOrderByIdAsync(long orderId)
    {
        var order = await _orderRepository.GetOrderByIdAsync(orderId);
        return order == null ? null : _mapper.Map<OrderModel>(order);
    }

    public async Task<long> CreateOrderAsync(OrderModel order)
    {
        var orderEntity = _mapper.Map<OrderEntity>(order);
        return await _orderRepository.CreateOrderAsync(orderEntity);
    }

    public async Task<bool> UpdateOrderAsync(OrderModel order)
    {
        var orderEntity = _mapper.Map<OrderEntity>(order);
        return await _orderRepository.UpdateOrderAsync(orderEntity);
    }

    public async Task<bool> DeleteOrderAsync(long orderId)
    {
        return await _orderRepository.DeleteOrderAsync(orderId);
    }
}