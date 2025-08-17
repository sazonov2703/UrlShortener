using MediatR;
using UrlShortener.Domain.Interfaces.Repositories.Write;
using UrlShortener.Domain.Services;

namespace UrlShortener.Application.UseCases.Commands;

public class CreateLinkCommandHandler(
    ILinkWriteRepository linkWriteRepository,
    LinkService linkService   
) : IRequestHandler<CreateLinkCommand, CreateLinkCommandResponseDto>
{
    public async Task<CreateLinkCommandResponseDto> Handle(
        CreateLinkCommand request, CancellationToken cancellationToken)
    {
        var dto = request.Dto;

        var shortLink = await linkService.CreateLinkAsync(dto.Url, null, request.UserId, cancellationToken);;
        
        await linkWriteRepository.AddAsync(shortLink, cancellationToken);
        await linkWriteRepository.SaveChangesAsync(cancellationToken);
        
        return new CreateLinkCommandResponseDto(dto.Url, shortLink.ShortCode, shortLink.ExpiresAt);
    }
}
public record CreateLinkCommand(Guid UserId, CreateLinkCommandDto Dto) : IRequest<CreateLinkCommandResponseDto>;
public record CreateLinkCommandDto(string Url);
public record CreateLinkCommandResponseDto(string Url, string ShortUrl, DateTime? ExpiresAt);