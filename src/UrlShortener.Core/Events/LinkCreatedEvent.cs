using UrlShortener.Domain.Entities;
using UrlShortener.Domain.Interfaces;

namespace UrlShortener.Domain.Events;

public class LinkCreatedEvent : IDomainEvent
{
    public LinkCreatedEvent(
        Guid linkId, string url, string shortCode, DateTime? expiresAt, Guid userId)
    {
        LinkId = linkId;
        Url = url;
        ShortCode = shortCode;
        ExpiresAt = expiresAt;
        UserId = userId;
        CreatedAt = DateTime.UtcNow;
    }
    
    public Guid LinkId { get; set; }
    public string Url { get; set; }
    public string ShortCode { get; set; }
    public DateTime? ExpiresAt { get; set; }
    public Guid UserId { get; set; }
    public DateTime CreatedAt { get; set; }
}