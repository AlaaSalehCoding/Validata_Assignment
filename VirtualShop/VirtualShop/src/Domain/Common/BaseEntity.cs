using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Cryptography;

namespace VirtualShop.Domain.Common;
 
public abstract class BaseEntity
{
    public long Id { get; set; } = 0;

    [NotMapped]
    public abstract IReadOnlyCollection<BaseEvent> DomainEvents { get; } 
    public abstract void AddDomainEvent(BaseEvent domainEvent);
    public abstract void RemoveDomainEvent(BaseEvent domainEvent);
    public abstract void ClearDomainEvents();
}
public abstract class BaseEntity<TId>  : BaseEntity 
{
    public BaseEntity(TId id)
    {
        Id = id;
    }
    [Key]
    public new TId Id { get; set; } 

    private readonly List<BaseEvent> _domainEvents = new();

    [NotMapped]
    public override IReadOnlyCollection<BaseEvent> DomainEvents => _domainEvents.AsReadOnly();
     
    public override void AddDomainEvent(BaseEvent domainEvent)
    {
        _domainEvents.Add(domainEvent);
    }

    public override void RemoveDomainEvent(BaseEvent domainEvent)
    {
        _domainEvents.Remove(domainEvent);
    }

    public override void ClearDomainEvents()
    {
        _domainEvents.Clear();
    }
}
