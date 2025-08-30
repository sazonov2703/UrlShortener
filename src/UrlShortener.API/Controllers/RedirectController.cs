using MediatR;
using Microsoft.AspNetCore.Mvc;
using UrlShortener.Application.UseCases.Queries;

namespace UrlShortener.API.Controllers;

[ApiController]
[Route("")]
public class RedirectController(IMediator mediator) : Controller
{
    [HttpGet("{code}")]
    public async Task<RedirectResult> Redirect(string code, CancellationToken cancellationToken)
    {
        var url = await mediator.Send(new GetOriginalUrlQuery(code), cancellationToken);
        
        return Redirect(url);   
    }
}