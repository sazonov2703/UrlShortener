using Microsoft.EntityFrameworkCore;
using UrlShortener.Domain.Interfaces.Repositories.Write;

namespace UrlShortener.Infrastructure.Persistence.Repositories.Write;

public class BaseWriteRepository<T>(UrlShortenerDbContext context) 
    : IBaseWriteRepository<T> where T : class
{
    private readonly DbSet<T> _dbSet = context.Set<T>();
    
    public async Task AddAsync(T entity, CancellationToken cancellationToken)
    {
        await _dbSet.AddAsync(entity, cancellationToken);
    }

    public async Task UpdateAsync(T entity, CancellationToken cancellationToken)
    {
        await Task.Run(() => _dbSet.Update(entity), cancellationToken);
    }

    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken)
    {
        var entity = await _dbSet.FindAsync(id, cancellationToken);

        if (entity == null) 
            throw new KeyNotFoundException($"Entity {typeof(T).Name} with Id {id} does not exist.");
        
        await Task.Run(() => _dbSet.Remove(entity), cancellationToken);
    }

    public async Task SaveChangesAsync(CancellationToken cancellationToken)
    {
        await context.SaveChangesAsync(cancellationToken);
    }
}