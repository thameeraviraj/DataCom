namespace DataCom.WebAPI.Data;

public interface IRepository<TEntity, in TKey> where TEntity: class
{
    //Task<IEnumerable<TEntity>> FindAll() does not scale
    IQueryable<TEntity> FindAll();
    
    ValueTask<TEntity?> FindByIdAsync(TKey id);
    Task AddAsync(TEntity entity);
    Task UpdateAsync(TEntity entity);
    Task DeleteByIdAsync(TKey id);
}