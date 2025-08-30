using MediatR;
using UrlShortener.Domain.Interfaces.Repositories.Read;

namespace UrlShortener.Application.UseCases.Queries;

public class GetOriginalUrlQueryHandler(
    ILinkReadRepository linkReadRepository) : IRequestHandler<GetOriginalUrlQuery, string?>
{
    public async Task<string?> Handle(GetOriginalUrlQuery request, CancellationToken cancellationToken)
    {
        var url = await linkReadRepository.GetByShortCodeAsync(request.ShortCode, cancellationToken);
        
        if (url == null)
            throw new Exception("Link not found");  
        
        return url;  
    }
}

public record GetOriginalUrlQuery(string ShortCode) : IRequest<string?>;