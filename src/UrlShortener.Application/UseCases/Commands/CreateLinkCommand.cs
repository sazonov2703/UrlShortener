using MediatR;
using UrlShortener.Domain.Interfaces.Repositories.Read;
using UrlShortener.Domain.Interfaces.Repositories.Write;
using UrlShortener.Domain.Services;

namespace UrlShortener.Application.UseCases.Commands;

public class CreateLinkCommandHandler(
    ILinkReadRepository linkReadRepository,
    ILinkWriteRepository linkWriteRepository,
    LinkService linkService 
    ) : IRequestHandler<CreateLinkCommand, CreateLinkCommandResponseDto>
{
    private const int NumberOfCharsInShortLink = 7;
    private const string Alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz1234567890";
    private readonly Random _random = new();
    public async Task<CreateLinkCommandResponseDto> Handle(
        CreateLinkCommand request, CancellationToken cancellationToken)
    {
        var dto = request.Dto;
        
        var shortCodeChars = new char[NumberOfCharsInShortLink];

        string shortCode;
        while (true)
        {
            for (var i = 0; i < NumberOfCharsInShortLink; i++)
            {
                var randomIndex = _random.Next(Alphabet.Length - 1);
                shortCodeChars[i] = Alphabet[randomIndex];
            }

            shortCode = new string(shortCodeChars);

            if (!await linkReadRepository.ExistsByShortCodeAsync(shortCode, cancellationToken))
            {
                break;
            }
        }

        var shortLink = linkService.CreateLink(dto.Url, shortCode, null, request.UserId, cancellationToken);
        
        await linkWriteRepository.AddAsync(shortLink, cancellationToken);
        await linkWriteRepository.SaveChangesAsync(cancellationToken);
        
        return new CreateLinkCommandResponseDto(
            shortLink.Url, shortLink.ShortCode, shortLink.UserId, shortLink.ExpiresAt);
    }
}
public record CreateLinkCommand(Guid UserId, CreateLinkCommandDto Dto) : IRequest<CreateLinkCommandResponseDto>;

public record CreateLinkCommandDto(string Url);
public record CreateLinkCommandResponseDto(string Url, string ShortCode, Guid UserId, DateTime? ExpiresAt);