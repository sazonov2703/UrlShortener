using System.Security.Claims;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UrlShortener.Application.UseCases.Commands;

namespace UrlShortener.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class LinkController(IMediator mediator) : Controller
{
    [HttpPost("create")]
    [Authorize]
    public async Task<IActionResult> Create(
        [FromBody]CreateLinkCommandDto dto, CancellationToken cancellationToken)
    {
        var userId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
        
        var result = await mediator.Send(
            new CreateLinkCommand(userId, dto), cancellationToken);
        
        return Ok(result);
    }
}