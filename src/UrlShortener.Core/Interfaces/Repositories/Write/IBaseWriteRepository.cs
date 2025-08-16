namespace UrlShortener.Domain.Interfaces.Repositories.Write;

public interface IBaseWriteRepository<T> where T : class
{
    Task AddAsync(T entity, CancellationToken cancellationToken);

    Task UpdateAsync(T entity, CancellationToken cancellationToken);

    Task DeleteAsync(Guid id, CancellationToken cancellationToken);
    
    Task SaveChangesAsync(CancellationToken cancellationToken);
}