using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UrlShortener.Domain.Entities;

namespace UrlShortener.Infrastructure.Persistence.Configurations;

public class LinkConfiguration : IEntityTypeConfiguration<Link>
{
    public void Configure(EntityTypeBuilder<Link> builder)
    {
        builder.ToTable(nameof(Link));
        
        builder.HasKey(l => l.Id);
        
        builder.HasIndex(l => l.ShortCode)
            .IsUnique();
        
        builder.Property(l => l.Url)
            .HasColumnName(nameof(Link.Url))
            .IsRequired()
            .HasMaxLength(2000);
        
        builder.Property(l => l.ShortCode)
            .HasColumnName(nameof(Link.ShortCode))
            .IsRequired()
            .HasMaxLength(20);
        
        builder.Property(l => l.ExpiresAt)
            .HasColumnName(nameof(Link.ExpiresAt))
            .IsRequired(false);
        
        builder.Property(l => l.ClickCount)
            .HasColumnName(nameof(Link.ClickCount))
            .IsRequired()
            .HasDefaultValue(0);
        
        builder.Property(l => l.UserId)
            .HasColumnName(nameof(Link.UserId))
            .IsRequired();
        
    }
}