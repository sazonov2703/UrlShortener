using System.Linq.Expressions;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.EntityFrameworkCore;
using UrlShortener.Domain.Interfaces.Repositories.Read;

namespace UrlShortener.Infrastructure.Persistence.Repositories.Read;

public abstract class BaseReadRepository<T>(UrlShortenerDbContext context) 
    : IBaseReadRepository<T> where T : class
{
    private readonly DbSet<T> _dbSet = context.Set<T>();
    
    public async Task<T> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _dbSet.FindAsync(id, cancellationToken) ?? 
               throw new KeyNotFoundException($"Entity {typeof(T).Name} with Id {id} does not exist.");
    }

    public Task<IQueryable<T>> GetQueryableAsync(
        ODataQueryOptions<T>? oDataQueryOptions, CancellationToken cancellationToken)
    {
        IQueryable<T> queryable = _dbSet.AsQueryable();
        
        if (oDataQueryOptions != null) 
            queryable = (IQueryable<T>)oDataQueryOptions.ApplyTo(queryable, new ODataQuerySettings());
        
        return Task.FromResult(queryable);
    }

    public async Task<IEnumerable<T>> FindAsync(
        Expression<Func<T, bool>> predicate, CancellationToken cancellationToken)
    {
        return await _dbSet.Where(predicate).ToListAsync(cancellationToken);   
    }
}