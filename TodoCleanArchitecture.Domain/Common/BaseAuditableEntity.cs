namespace TodoCleanArchitecture.Domain.Common;


public interface IBaseAuditableEntity
{
    DateTimeOffset CreatedAt { get; set; }

    string? CreatedBy { get; set; }

    DateTimeOffset ModifiedAt { get; set; }

    string? ModifiedBy { get; set; }
}


public abstract class BaseAuditableEntity<TId> : BaseEntity<TId>, IBaseAuditableEntity
    where TId : notnull
{
    public DateTimeOffset CreatedAt { get; set; }

    public string? CreatedBy { get; set; }

    public DateTimeOffset ModifiedAt { get; set; }

    public string? ModifiedBy { get; set; }
}