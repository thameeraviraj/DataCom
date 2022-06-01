using DataCom.WebAPI.Entity;
using Microsoft.EntityFrameworkCore;

namespace DataCom.WebAPI.Data;

public class ProductOptionRepository : BaseRepository<ProductOption, ApplicationDbContext, Guid>,
    IProductOptionRepository
{
    private readonly DbSet<ProductOption> _productOptions;

    public ProductOptionRepository(ApplicationDbContext context) : base(context)
    {
        _productOptions = context.Set<ProductOption>();
    }

    public async Task<List<ProductOption>> FindAllByProductIdAsync(Guid productId)
    {
        return await _productOptions.Where(po => po.ProductId.Equals(productId))
            .ToListAsync();
    }

    public Task<ProductOption?> FindByProductIdAndOptionIdAsync(Guid productId, Guid optionId)
    {
        return _productOptions
            .FirstOrDefaultAsync(o => o.Id == optionId && o.ProductId == productId);
    }

    public Task<bool> ExistsByProductIdAndOptionIdAsync(Guid productId, Guid optionId)
    {
        return _productOptions
            .Where(o => o.Id == optionId && o.ProductId  ==productId).AnyAsync();
    }
}