using UrlShortener.Domain.Entities;
using UrlShortener.Domain.Interfaces.Repositories.Read;

namespace UrlShortener.Infrastructure.Persistence.Repositories.Read;

public class LinkReadRepository(UrlShortenerDbContext context) : BaseReadRepository<Link>(context), ILinkReadRepository
{
    public async Task<bool> ExistsByShortCodeAsync(string shortCode, CancellationToken cancellationToken)
    {
        return context.Links.Any(l => l.ShortCode == shortCode);   
    }

    public async Task<string> GetByShortCodeAsync(string shortCode, CancellationToken cancellationToken)
    {
        return context.Links.Single(l => l.ShortCode == shortCode).ShortCode;
    }
}