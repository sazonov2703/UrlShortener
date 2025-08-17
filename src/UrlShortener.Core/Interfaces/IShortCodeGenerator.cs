namespace UrlShortener.Domain.Interfaces;

public interface IShortCodeGenerator
{
    public string Generate(string url);
}