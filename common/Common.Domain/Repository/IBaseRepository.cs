using System.Linq.Expressions;

namespace Common.Domain.Repository;

public interface IBaseRepository<T> where T : BaseEntity
{
    Task<T?> GetAsync(long id, params Expression<Func<T, object>>[] includes);

    Task<T?> GetTracking(long id);

    Task AddAsync(T entity);
    void Add(T entity);

    void Remove(T entity);
    void RemoveRange(List<T> entities);

    Task AddRange(ICollection<T> entities);

    void Update(T entity);

    Task<int> Save();
    void SaveSync();

    Task<bool> ExistsAsync(Expression<Func<T, bool>> expression);

    bool Exists(Expression<Func<T, bool>> expression);

    T? Get(long id);

    IQueryable<T> GetQueryable(Expression<Func<T, bool>>? predicate = null, Expression<Func<T, object>>? orderDescending = null, params Expression<Func<T, object>>[] includes);

    Task<List<T>> ToListAsync(Expression<Func<T, bool>>? predicate = null, Expression<Func<T, object>>? orderDescending = null, params Expression<Func<T, object>>[] includes);
}

public interface IMongoRepository<TEntity> where TEntity : BaseEntity
{
    Task Delete(long id);
    Task<TEntity?> GetById(long id);
    Task Insert(TEntity entity);
    Task Update(TEntity entity);

}