using FluentValidation;
using UrlShortener.Domain.Entities;

namespace UrlShortener.Domain.Validators;

public class LinkValidator : AbstractValidator<Link>
{
    public LinkValidator()
    {
        RuleFor(l => l.Url)
            .NotEmpty()
            .MaximumLength(2000)
            .Must(IsValidUrl);
        
        RuleFor(l => l.ShortCode)
            .NotEmpty()
            .MaximumLength(20);
    }
    private bool IsValidUrl(string url)
    {
        return Uri.TryCreate(url, UriKind.Absolute, out var uriResult) 
               && (uriResult.Scheme == Uri.UriSchemeHttp 
                   || uriResult.Scheme == Uri.UriSchemeHttps);
    }}