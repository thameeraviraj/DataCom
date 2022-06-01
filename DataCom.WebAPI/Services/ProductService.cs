using AutoMapper;
using DataCom.WebAPI.Data;
using DataCom.WebAPI.Entity;
using DataCom.WebAPI.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace DataCom.WebAPI.Services;

public class ProductService : IProductService
{
    private readonly IProductRepository _productRepository;

    public ProductService(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }
    
    public async Task<IEnumerable<Product>> FindAllAsync()
    {
        return await _productRepository.FindAll().ToListAsync();
    }

    public Task<IEnumerable<Product>> FindByNameAsync(string name)
    {
        return  _productRepository.FindByNameAsync(name);
    }

    public async Task<Product?> FindByIdAsync(Guid id)
    {
        return await _productRepository.FindByIdAsync(id);
    }

    public async Task AddAsync(Product product)
    {
        await _productRepository.AddAsync(product);
    }

    public async Task UpdateAsync(Product product)
    {
        await _productRepository.UpdateAsync(product);
    }

    public Task DeleteAsync(Guid id)
    {
        return _productRepository.DeleteByIdAsync(id);
    }
}