using DataCom.WebAPI.Entity;

namespace DataCom.WebAPI.Services;

public interface IProductOptionService
{
    Task<List<ProductOption>> GetByProductId(Guid productId); 
    Task<ProductOption?> FindByProductIdAndOptionIdAsync(Guid productId, Guid optionId);
    Task<bool> ExistsByProductIdAndOptionIdAsync(Guid productId, Guid optionId);
    Task AddByProductIdAsync(Guid productId, ProductOption option);
    Task UpdateAsync(ProductOption option);
    Task DeleteByOptionIdAsync(Guid optionId);
}