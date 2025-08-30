using UrlShortener.API.Middlewares;
using UrlShortener.Domain.Interfaces.Repositories.Read;
using UrlShortener.Domain.Interfaces.Repositories.Write;
using UrlShortener.Infrastructure.Interfaces;
using UrlShortener.Infrastructure.Persistence.Repositories.Read;
using UrlShortener.Infrastructure.Persistence.Repositories.Write;
using UrlShortener.Infrastructure.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(Program).Assembly));

builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<ILinkWriteRepository, LinkWriteRepository>();
builder.Services.AddScoped<ILinkReadRepository, LinkReadRepository>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<AuthMiddleware>();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
