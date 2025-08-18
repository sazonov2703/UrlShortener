using UrlShortener.Domain.Entities;
using UrlShortener.Domain.Interfaces.Repositories.Write;

namespace UrlShortener.Infrastructure.Persistence.Repositories.Write;

public class LinkWriteRepository(UrlShortenerDbContext context) 
    : BaseWriteRepository<Link>(context), ILinkWriteRepository
{
    
}