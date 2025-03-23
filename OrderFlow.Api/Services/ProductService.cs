using AutoMapper;
using OrderFlow.Api.Data;
using OrderFlow.Api.Models;
using OrderFlow.Shared;

namespace OrderFlow.Api.Services;

public interface IProductService
{
    Task<List<ProductModel>> GetAllProductsAsync();

    Task<ProductModel> GetProductByIdAsync(long productId);

    Task<long> CreateProductAsync(ProductModel product);

    Task<bool> UpdateProductAsync(ProductModel product);

    Task<bool> DeleteProductAsync(long productId);
}

public class ProductService : IProductService
{
    private readonly IProductRepository _productRepository;
    private readonly IMapper _mapper;

    public ProductService(IProductRepository productRepository, IMapper mapper)
    {
        _productRepository = productRepository;
        _mapper = mapper;
    }

    public async Task<List<ProductModel>> GetAllProductsAsync()
    {
        var products = await _productRepository.GetAllProductsAsync();
        return _mapper.Map(products, new List<ProductModel>());
    }

    public async Task<ProductModel> GetProductByIdAsync(long productId)
    {
        var product = await _productRepository.GetProductByIdAsync(productId);
        return product == null ? null : _mapper.Map<ProductModel>(product);
    }

    public async Task<long> CreateProductAsync(ProductModel product)
    {
        var productEntity = _mapper.Map<ProductEntity>(product);
        return await _productRepository.CreateProductAsync(productEntity);
    }

    public async Task<bool> UpdateProductAsync(ProductModel product)
    {
        var productEntity = _mapper.Map<ProductEntity>(product);
        return await _productRepository.UpdateProductAsync(productEntity);
    }

    public async Task<bool> DeleteProductAsync(long productId)
    {
        return await _productRepository.DeleteProductAsync(productId);
    }
}