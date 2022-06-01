using AutoMapper;
using DataCom.WebAPI.Data;
using DataCom.WebAPI.Entity;
using DataCom.WebAPI.Exceptions;

namespace DataCom.WebAPI.Services;

public class ProductOptionService : IProductOptionService
{
    private readonly IProductRepository _productRepository;
    private readonly IProductOptionRepository _optionsRepository;
    private readonly IMapper _mapper;

    public ProductOptionService(IProductRepository productRepository, IProductOptionRepository optionsRepository, IMapper mapper)
    {
        _productRepository = productRepository;
        _optionsRepository = optionsRepository;
        _mapper = mapper;
    }

    public Task<List<ProductOption>> GetByProductId(Guid productId)
    {
        return _optionsRepository.FindAllByProductIdAsync(productId);
    }

    public Task<ProductOption?> FindByProductIdAndOptionIdAsync(Guid productId, Guid optionId)
    {
        return _optionsRepository.FindByProductIdAndOptionIdAsync(productId, optionId);
    }

    public Task<bool> ExistsByProductIdAndOptionIdAsync(Guid productId, Guid optionId)
    {
        return _optionsRepository.ExistsByProductIdAndOptionIdAsync(productId, optionId);
    }

    public async Task AddByProductIdAsync(Guid productId, ProductOption option)
    {
        var product = await EnsureAndGetProductByIdAsync(productId);
        option.Product = product;
        await _optionsRepository.AddAsync(option);
    }

    public async Task UpdateAsync(ProductOption newOption)
    {
        var option = await EnsureAndGetOptionByIdAsync(newOption.Id);
        _mapper.Map(newOption, option);
        
        await _optionsRepository.UpdateAsync(option);
    }

    public async Task DeleteByOptionIdAsync(Guid optionId)
    {
        var option = await EnsureAndGetOptionByIdAsync(optionId);
        await _optionsRepository.DeleteByIdAsync(option.Id);
    }
    
    private async Task<ProductOption> EnsureAndGetOptionByIdAsync(Guid optionId)
    {
        var option = await _optionsRepository.FindByIdAsync(optionId);
        
        if (option is null)
        {
            throw new ResourceNotFoundException();
        }

        return option;
    }

    private async Task<Product> EnsureAndGetProductByIdAsync(Guid productId)
    {
        var product = await _productRepository.FindByIdAsync(productId);
        if (product is null)
        {
            throw new ResourceNotFoundException();
        }

        return product;
    }
}