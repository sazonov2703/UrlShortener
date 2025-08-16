using UrlShortener.Domain.Entities;

namespace UrlShortener.Domain.Interfaces.Repositories.Read;

public interface ILinkReadRepository : IBaseReadRepository<Link>
{
    Task<bool> ExistsByShortCodeAsync(string shortCode, CancellationToken cancellationToken);
}