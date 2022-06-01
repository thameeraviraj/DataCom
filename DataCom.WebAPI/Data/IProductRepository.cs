using DataCom.WebAPI.Entity;

namespace DataCom.WebAPI.Data
{
    public interface IProductRepository : IRepository<Product, Guid>
    {
        Task<IEnumerable<Product>> FindByNameAsync(string name);
    }
}
