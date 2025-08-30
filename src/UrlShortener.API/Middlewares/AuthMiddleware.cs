using System.Security.Claims;
using UrlShortener.Infrastructure.Interfaces;

namespace UrlShortener.API.Middlewares;

public class AuthMiddleware
{
    private readonly RequestDelegate _next;
    
    public AuthMiddleware(RequestDelegate next)
    {
        _next = next;
    }
    
    public async Task InvokeAsync(HttpContext context, IAuthService authService, CancellationToken cancellationToken)
    {
        var token = context.Request.Headers["Authorization"];
        
        if (string.IsNullOrEmpty(token))
            throw new Exception("Token is required");
        
        var user = await authService.ValidateTokenAsync(token, cancellationToken);
        
        if (user != null)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            };

            var identity = new ClaimsIdentity(claims, "Custom");
            context.User = new ClaimsPrincipal(identity);
        }
        
        await _next(context);
    }
}