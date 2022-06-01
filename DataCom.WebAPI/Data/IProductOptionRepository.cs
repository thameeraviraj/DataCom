using DataCom.WebAPI.Entity;

namespace DataCom.WebAPI.Data
{
    public interface IProductOptionRepository : IRepository<ProductOption, Guid>
    {
        Task<List<ProductOption>> FindAllByProductIdAsync(Guid productId); // Possibly limited
        Task<ProductOption?> FindByProductIdAndOptionIdAsync(Guid productId, Guid optionId);
        Task<bool> ExistsByProductIdAndOptionIdAsync(Guid productId, Guid optionId);
    }
}
