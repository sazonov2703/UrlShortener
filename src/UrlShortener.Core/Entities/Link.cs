using UrlShortener.Domain.Events;
using UrlShortener.Domain.Validators;

namespace UrlShortener.Domain.Entities;

public class Link : BaseEntity<Link>
{
    private Link() { }

    private Link(string url, string shortCode, DateTime? expiresAt, Guid userId)
    {
        Url = url;
        ShortCode = shortCode;
        ExpiresAt = expiresAt;
        UserId = userId;
        ClickCount = 0;
        
        ValidateEntity(new LinkValidator());
    }
    
    public string Url { get; private set; }
    public string ShortCode { get; private set; }
    public DateTime? ExpiresAt { get; private set; }
    public Guid UserId { get; private set; }
    public long ClickCount { get; private set; }

    public void IncrementClickCount() =>
        ClickCount++;   

    public bool IsExpired(DateTime now) =>
        ExpiresAt.HasValue && ExpiresAt.Value < now;

    public static Link Create(string url, string shortCode, DateTime? expiresAt, Guid userId)
    {
        var link = new Link(url, shortCode, expiresAt, userId);
        
        link.AddDomainEvent(new LinkCreatedEvent(link.Id, url, shortCode, expiresAt, userId));
        
        return link;
    }
}