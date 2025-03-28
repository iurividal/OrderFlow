using AutoMapper;
using OrderFlow.Api.Data;
using OrderFlow.Api.Models;
using OrderFlow.Shared;

namespace OrderFlow.Api.Services;

public interface IOrderService
{
    Task<List<OrderModel>> GetAllOrdersAsync();

    Task<OrderModel> GetOrderByIdAsync(long orderId);

    Task<OrderModel> GetOrderByNumberAsync(string orderNumber);

    Task<List<OrderModel>> GetOrdersByCustomerIdAsync(int customerId);

    Task<long> CreateOrderAsync(OrderModel order);

    Task<bool> UpdateOrderAsync(OrderModel order);

    Task<bool> UpdatePaymentOrderAsync(OrderModel order);

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


    public async Task<OrderModel> GetOrderByNumberAsync(string orderNumber)
    {
        var order = await _orderRepository.GetOrderByNumberAsync(orderNumber);
        return order == null ? null : _mapper.Map<OrderModel>(order);
    }
    
    public async Task<List<OrderModel>> GetOrdersByCustomerIdAsync(int customerId)
    {
        var orders = await _orderRepository.GetOrdersByCustomerIdAsync(customerId);
        return orders == null ? null : _mapper.Map(orders, new List<OrderModel>());
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

    public Task<bool> UpdatePaymentOrderAsync(OrderModel order)
    {
        var orderEntity = _mapper.Map<OrderEntity>(order);
        return _orderRepository.UpdatePaymentOrderAsync(orderEntity);
    }

    public async Task<bool> DeleteOrderAsync(long orderId)
    {
        return await _orderRepository.DeleteOrderAsync(orderId);
    }
}