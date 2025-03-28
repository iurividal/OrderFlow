using AutoMapper;
using OrderFlow.Api.Data;
using OrderFlow.Api.Models;
using OrderFlow.Shared;

namespace OrderFlow.Api.Services;

public interface ICustomerService
{
    Task<CustomerModel> GetCustomerByIdAsync(long customerId);
    Task<IEnumerable<CustomerModel>> GetAllCustomersAsync();
    Task<long> CreateCustomerAsync(CustomerModel customer);
    Task<bool> UpdateCustomerAsync(CustomerModel customer);
    Task<bool> DeleteCustomerAsync(long customerId);

    Task<IEnumerable<CustomerModel>> GetCustomerByNameAsync(string name);
}

public class CustomerService : ICustomerService
{
    private readonly ICustomerRepository _customerRepository;
    private readonly IMapper _mapper;

    public CustomerService(ICustomerRepository customerRepository, IMapper mapper)
    {
        _customerRepository = customerRepository;
        _mapper = mapper;
    }

    public async Task<CustomerModel> GetCustomerByIdAsync(long customerId)
    {
        var customer = await _customerRepository.GetCustomerByIdAsync(customerId);
        return customer == null ? null : _mapper.Map<CustomerModel>(customer);
    }


    public async Task<IEnumerable<CustomerModel>> GetAllCustomersAsync()
    {
        var customer = _mapper.Map<IEnumerable<CustomerModel>>(await _customerRepository.GetAllCustomersAsync());
        return customer;
    }

    public async Task<IEnumerable<CustomerModel>> GetCustomerByNameAsync(string name)
    {
        var customer = _mapper.Map<IEnumerable<CustomerModel>>(await _customerRepository.GetCustomerByNameAsync(name));
        return customer;
    }

    public async Task<long> CreateCustomerAsync(CustomerModel customer)
    {
        var customerEntity = _mapper.Map<CustomerEntity>(customer);

        // Aqui você pode adicionar regras de negócios, como validação, antes de criar o cliente.
        return await _customerRepository.CreateCustomerAsync(customerEntity);
    }

    public async Task<bool> UpdateCustomerAsync(CustomerModel customer)
    {
        var customerEntity = _mapper.Map<CustomerEntity>(customer);
        // Lógica de validação ou transformação pode ser aplicada antes de chamar o repositório.
        return await _customerRepository.UpdateCustomerAsync(customerEntity);
    }

    public async Task<bool> DeleteCustomerAsync(long customerId)
    {
        // Realize ações antes de excluir o cliente, se necessário.
        return await _customerRepository.DeleteCustomerAsync(customerId);
    }
}