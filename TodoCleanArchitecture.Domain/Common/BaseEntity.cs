namespace TodoCleanArchitecture.Domain.Common;

public interface IEntity { }

public interface IBaseEntity : IEntity
{
    List<IDomainEvent> DomainEvents { get; }
    void AddDomainEvent(IDomainEvent domainEvent);
    void RemoveDomainEvent(IDomainEvent domainEvent);
    void ClearDomainEvents();
}

public abstract class BaseEntity<TId>: IBaseEntity where TId : notnull
{
    public TId Id { get; protected set; }
    
    private readonly List<IDomainEvent> _domainEvents = [];

    public List<IDomainEvent> DomainEvents => [.. _domainEvents];
    
    public override bool Equals(object? obj)
    {
        return obj is not null
               && obj is BaseEntity<TId> entity &&
               obj.GetType() == GetType() &&
               Id.Equals(entity.Id);
    } 

    public static bool operator ==(BaseEntity<TId> left, BaseEntity<TId> right)
        => left.Equals(right);
    public static bool operator !=(BaseEntity<TId> left, BaseEntity<TId> right)
        => !left.Equals(right);

    public override int GetHashCode()
        => HashCode.Combine(GetType(), Id);

    public void AddDomainEvent(IDomainEvent domainEvent)
    {
        _domainEvents.Add(domainEvent);
    }

    public void RemoveDomainEvent(IDomainEvent domainEvent)
    {
        _domainEvents.Remove(domainEvent);
    }

    public void ClearDomainEvents()
    {
        _domainEvents.Clear();
    }
}