using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Diagnostics;
using TodoCleanArchitecture.Application.Abstractions.Identity;
using TodoCleanArchitecture.Domain.Common;

namespace TodoCleanArchitecture.Infrastructure.Persistence.Data.Interceptors;

public class AuditableEntityInterceptor(
    IUser user,
    TimeProvider dateTime) : SaveChangesInterceptor
{
    public override InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
    {
        UpdateEntities(eventData.Context);

        return base.SavingChanges(eventData, result);
    }

    public override ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result, CancellationToken cancellationToken = default)
    {
        UpdateEntities(eventData.Context);

        return base.SavingChangesAsync(eventData, result, cancellationToken);
    }

    private void UpdateEntities(DbContext? context)
    {
        if (context == null) return;

        foreach (var entry in context.ChangeTracker.Entries()
                     .Where(e => e.Entity is IBaseAuditableEntity))
        {
            if (entry.State is not (EntityState.Added or EntityState.Modified) &&
                !entry.HasChangedOwnedEntities()) continue;
            
            var utcNow = dateTime.GetUtcNow();
            var entity = (IBaseAuditableEntity)entry.Entity;
            if (entry.State == EntityState.Added)
            {
                entity.CreatedBy = user.Id;
                entity.CreatedAt = utcNow;
            } 
            entity.ModifiedBy = user.Id;
            entity.ModifiedAt = utcNow;
        }
    }
}

public static class AuditableEntityExtensions
{
    public static bool HasChangedOwnedEntities(this EntityEntry entry) =>
        entry.References.Any(r => 
            r.TargetEntry != null && 
            r.TargetEntry.Metadata.IsOwned() && 
            (r.TargetEntry.State == EntityState.Added || r.TargetEntry.State == EntityState.Modified));
}