using DataCom.WebAPI.Entity;
using Microsoft.EntityFrameworkCore;

namespace DataCom.WebAPI.Data;

public class ProductRepository : BaseRepository<Product, ApplicationDbContext, Guid>, IProductRepository
{
    private readonly ApplicationDbContext _dbContext;

    public ProductRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }


    public async Task<IEnumerable<Product>> FindByNameAsync(string name)
    {
        return await _dbContext.Products
            .Where(p => p.Name.Contains(name, StringComparison.CurrentCultureIgnoreCase))
            .ToListAsync();
    }
}