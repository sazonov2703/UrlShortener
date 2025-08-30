namespace UrlShortener.Infrastructure.Interfaces;

public interface IAuthService
{
    public Task<UserInfo> ValidateTokenAsync(string token, CancellationToken cancellationToken);
}

public record UserInfo(Guid Id, string Email);