using DataCom.WebAPI.Entity;

namespace DataCom.WebAPI.Services;

public interface IProductService
{
    Task<IEnumerable<Product>> FindAllAsync();
    
    Task<IEnumerable<Product>> FindByNameAsync(string name);

    Task<Product?> FindByIdAsync(Guid id);

    Task AddAsync(Product product);

    Task UpdateAsync(Product product);

    Task DeleteAsync(Guid id);
}