using System.Linq.Expressions;
using AngleSharp.Dom;
using Common.Domain;
using Common.Domain.Repository;
using Microsoft.EntityFrameworkCore;

namespace Common.Infrastructure.Repository;

public class BaseRepository<T, TContext> : IBaseRepository<T>
    where TContext : DbContext where T : BaseEntity
{
    protected readonly TContext Context;
    public BaseRepository(TContext context)
    {
        Context = context;
    }
    public async Task<T?> GetAsync(long id, params Expression<Func<T, object>>[] includes)
    {
        IQueryable<T> query = Context.Set<T>();

        // Apply includes using Expression<Func<T, object>>[] includes
        foreach (var include in includes)
        {
            query = query.Include(include);
        }

        // Execute query and return result
        var result = await query.FirstOrDefaultAsync(t => t.Id.Equals(id));

        return result;
    }

    public async Task<T?> GetTracking(long id)
    {
        return await Context.Set<T>().AsTracking().FirstOrDefaultAsync(t => t.Id.Equals(id));
    }
    public void Add(T entity)
    {
        Context.Set<T>().Add(entity);
    }
    public async Task AddAsync(T entity)
    {
        await Context.Set<T>().AddAsync(entity);
    }
    public async Task AddRange(ICollection<T> entities)
    {
        await Context.Set<T>().AddRangeAsync(entities);
    }
    public void Update(T entity)
    {
        Context.Update(entity);
    }
    public async Task<int> Save()
    {
        return await Context.SaveChangesAsync();
    }

    public void SaveSync()
    {
        Context.SaveChanges();
    }

    public async Task<bool> ExistsAsync(Expression<Func<T, bool>> expression)
    {
        return await Context.Set<T>().AnyAsync(expression);
    }
    public bool Exists(Expression<Func<T, bool>> expression)
    {
        return Context.Set<T>().Any(expression);
    }
    public T? Get(long id)
    {
        return Context.Set<T>().FirstOrDefault(t => t.Id.Equals(id)); ;
    }
    public IQueryable<T> GetQueryable(Expression<Func<T, bool>>? predicate = null, Expression<Func<T, object>>? orderDescending = null, params Expression<Func<T, object>>[] includes)
    {
        var query = Context.Set<T>().AsQueryable();

        if (predicate != null)
        {
            query = query.Where(predicate);
        }
        if (orderDescending != null)
        {
            query = query.OrderByDescending(orderDescending);
        }
        foreach (var include in includes)
        {
            query = query.Include(include);
        }

        return query;
    }
    public async Task<List<T>> ToListAsync(Expression<Func<T, bool>>? predicate = null, Expression<Func<T, object>>? orderDescending = null, params Expression<Func<T, object>>[] includes)
    {
        var query = Context.Set<T>().AsQueryable();
        if (predicate != null)
        {
            query = query.Where(predicate);
        }
        if (orderDescending != null)
        {
            query = query.OrderByDescending(orderDescending);
        }
        foreach (var include in includes)
        {
            query = query.Include(include);
        }
        return await query.ToListAsync();
    }

    public void Remove(T entity)
    {
        Context.Set<T>().Remove(entity);
    }

    public void RemoveRange(List<T> entities)
    {
        Context.Set<T>().RemoveRange(entities);
    }
}