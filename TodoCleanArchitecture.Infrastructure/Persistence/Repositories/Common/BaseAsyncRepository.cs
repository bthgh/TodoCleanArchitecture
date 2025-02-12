
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;
using TodoCleanArchitecture.Domain.Common;

namespace TodoCleanArchitecture.Infrastructure.Persistence.Repositories.Common;

internal abstract class BaseAsyncRepository<TEntity, TDbContext> 
    where TEntity : class,IEntity
    where TDbContext : DbContext    
{
    public readonly TDbContext DbContext;
    protected DbSet<TEntity> Entities { get; }
    protected virtual IQueryable<TEntity> Table => Entities;
    protected virtual IQueryable<TEntity> TableNoTracking => Entities.AsNoTrackingWithIdentityResolution();

    protected BaseAsyncRepository(TDbContext dbContext)
    {
        DbContext = dbContext;
        Entities = DbContext.Set<TEntity>();
    }

    protected virtual async Task<List<TEntity>> ListAllAsync()
    {
        return await Entities.ToListAsync();
    }


    protected virtual async Task AddAsync(TEntity entity)
    {
        await Entities.AddAsync(entity);

    }

    protected virtual async Task UpdateAsync(
        Expression<Func<SetPropertyCalls<TEntity>, SetPropertyCalls<TEntity>>> updateExpression)
    {
        await Entities.ExecuteUpdateAsync(updateExpression);
    }

    protected virtual async Task UpdateAsync(
        Expression<Func<TEntity, bool>> whereExpression, Expression<Func<SetPropertyCalls<TEntity>, SetPropertyCalls<TEntity>>> updateExpression)
    {
        await Entities.Where(whereExpression).ExecuteUpdateAsync(updateExpression);
    }

    protected virtual async Task DeleteAsync(Expression<Func<TEntity, bool>> deleteExpression)
    {
        await Entities.Where(deleteExpression).ExecuteDeleteAsync();
    }
}