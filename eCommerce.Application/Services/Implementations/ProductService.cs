using AutoMapper;
using eCommerce.Application.DTOs;
using eCommerce.Application.DTOs.Product;
using eCommerce.Application.Services.Interfaces;
using eCommerce.Domain.Entities;
using eCommerce.Domain.Interfaces;

namespace eCommerce.Application.Services.Implementations;

public class ProductService : IProductService
{
    private readonly IGeneric<Product> _productInterface;
    private readonly IMapper _mapper;

    public ProductService(IGeneric<Product> productInterface, IMapper mapper)
    {
        _productInterface = productInterface;
        _mapper = mapper;
    }

    public async Task<IEnumerable<GetProduct>> GetAllAsync()
    {
        var result = await _productInterface.GetAllAsync();
        if (!result.Any())
            return [];
        return _mapper.Map<IEnumerable<GetProduct>>(result);
    }

    public async Task<GetProduct> GetByIdAsync(Guid id)
    {
        var result = await _productInterface.GetByIdAsync(id); 
        return result == null ? null : _mapper.Map<GetProduct>(result);
    }

    public async Task<ServiceResponse> AddAsync(CreateProduct product)
    {
        var mappedProduct = _mapper.Map<Product>(product);
        int result = await _productInterface.AddAsync(mappedProduct);
        return new ServiceResponse(result > 0,
            (result > 0) ? "Product was added successfully" : "Product Failed to added");
    }

    public async Task<ServiceResponse> UpdateAsync(UpdateProduct product)
    {
        var mappedProduct = _mapper.Map<Product>(product);
        int result = await _productInterface.UpdateAsync(mappedProduct);
        return new ServiceResponse(result > 0,
            (result > 0) ? "Product was Updated successfully" : "Product Failed to Updated");
    }

    public async Task<ServiceResponse> DeleteAsync(Guid id)
    {
        var result = await _productInterface.DeleteAsync(id);
        return new ServiceResponse(result > 0,
            (result > 0) ? "Product was deleted successfully" : "Product not found or failed to delete");
    }
}