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
        AddDomainEvent(new LinkCreatedEvent(Id, url, shortCode, expiresAt, userId));
    }
    
    public string Url { get; set; }
    public string ShortCode { get; set; }
    public DateTime? ExpiresAt { get; set; }
    public Guid UserId { get; set; }
    public decimal ClickCount { get; set; }

    public void IncrementClickCount()
    {
        ClickCount++;   
    }

    public bool IsExpired()
    {
        return ExpiresAt.HasValue && ExpiresAt.Value < DateTime.UtcNow;   
    }

    public static Link Create(string url, string shortUrl, DateTime? expiresAt, Guid userId)
    {
        return new Link(url, shortUrl, expiresAt, userId);
    }
}