using UrlShortener.Domain.Entities;
using UrlShortener.Domain.Interfaces.Repositories.Read;

namespace UrlShortener.Domain.Services;

public class LinkService()
{
    public Link CreateLink(
        string url, string shortCode, DateTime? expiresAt, Guid userId, CancellationToken cancellationToken)
    {
        if (expiresAt.HasValue && expiresAt.Value < DateTime.UtcNow)
            throw new Exception("ExpiresAt must be greater than current date");   

        if (userId.Equals(Guid.Empty))
            throw new Exception("User id must be provided");
        
        return Link.Create(url, shortCode, expiresAt, userId);   
    }
}