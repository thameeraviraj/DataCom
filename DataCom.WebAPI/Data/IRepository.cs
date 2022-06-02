namespace DataCom.WebAPI.Data;

public interface IRepository<TEntity, in TKey> where TEntity: class
{
    IQueryable<TEntity> FindAll();
    ValueTask<TEntity?> FindByIdAsync(TKey id);
    Task AddAsync(TEntity entity);
    Task UpdateAsync(TEntity entity);
    Task DeleteByIdAsync(TKey id);
}