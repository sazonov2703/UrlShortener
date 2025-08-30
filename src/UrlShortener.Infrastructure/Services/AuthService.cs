using UrlShortener.Infrastructure.Interfaces;

namespace UrlShortener.Infrastructure.Services;

public class AuthService : IAuthService
{
    public AuthService()
    {
        throw new NotImplementedException();   
    }

    public Task<UserInfo> ValidateTokenAsync(string token, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}