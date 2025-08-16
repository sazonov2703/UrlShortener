using FluentValidation;
using UrlShortener.Domain.Interfaces;

namespace UrlShortener.Domain.Entities;

public abstract class BaseEntity<T> where T : BaseEntity<T>
{
    private readonly List<IDomainEvent> _domainEvents = [];
    
    protected BaseEntity()
    {
        Id = Guid.NewGuid();
    }
    
    public Guid Id { get; protected init; }
    
    protected void ValidateEntity(AbstractValidator<T> validator)
    {
        var validationResult = validator.Validate((T)this);
        if (validationResult.IsValid)
        {
            return;
        }

        var errorMessages = string.Join("; ", validationResult.Errors.Select(e => e.ErrorMessage));
        throw new ValidationException(errorMessages);
    }
    
    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(this, obj)) return true;
        if (obj is null || obj.GetType() != GetType()) return false;

        return Id.Equals(((BaseEntity<T>)obj).Id);
    }
    
    public override int GetHashCode() => Id.GetHashCode();
    
    public static bool operator ==(BaseEntity<T>? left, BaseEntity<T>? right)
    {
        if (left is null) return right is null;
        return left.Equals(right);
    }
    
    public static bool operator !=(BaseEntity<T>? left, BaseEntity<T>? right)
    {
        return !(left == right);
    }

    public IReadOnlyList<IDomainEvent> GetDomainEvents()
    {
        return _domainEvents.AsReadOnly();
    }

    public void ClearDomainEvents()
    {
        _domainEvents.Clear();
    }

    protected void AddDomainEvent(IDomainEvent domainEvent)
    {
        _domainEvents.Add(domainEvent);
    }
}