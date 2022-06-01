using Microsoft.EntityFrameworkCore;

namespace DataCom.WebAPI.Data;

public abstract class BaseRepository<TEntity, TContext, TKey> : IRepository<TEntity, TKey> 
    where TEntity : class
    where TContext : DbContext
{
    private readonly TContext _context;

    protected BaseRepository(TContext context)
    {
        _context = context;
    }

    public virtual IQueryable<TEntity> FindAll()
    {
        return _context.Set<TEntity>().AsQueryable();
    }

    public virtual ValueTask<TEntity?> FindByIdAsync(TKey id)
    {
        return _context.Set<TEntity>().FindAsync(id);
    }

    public virtual async Task AddAsync(TEntity entity)
    {
        await _context.Set<TEntity>().AddAsync(entity);
        await _context.SaveChangesAsync();
    }

    public virtual async Task UpdateAsync(TEntity entity)
    {
        _context.Entry(entity).State = EntityState.Modified;
        await _context.SaveChangesAsync();
    }

    public virtual async  Task DeleteByIdAsync(TKey id)
    {
        var entity = await _context.Set<TEntity>().FindAsync(id);
        if (entity is not null)
        {
            _context.Set<TEntity>().Remove(entity);
            await _context.SaveChangesAsync();
        }
    }
}