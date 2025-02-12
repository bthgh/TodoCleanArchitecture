using System.Transactions;
using TodoCleanArchitecture.Application.Abstractions.Repositories;
using TodoCleanArchitecture.Infrastructure.Persistence.Data;

namespace TodoCleanArchitecture.Infrastructure.Persistence.Repositories.Common;

public class UnitOfWork : IUnitOfWork, IDisposable
{
    private bool _disposed;

    private readonly ApplicationDbContext _dbContext; 
    
    public ITodoItemRepository TodoItemRepository { get; } 
    

    public UnitOfWork(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
        TodoItemRepository = new TodoItemRepository(_dbContext); 
    }

    

    public async Task<int> CommitAsync()
    {
        using var transaction = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);
        try
        {
            var result = await _dbContext.SaveChangesAsync(); 
            transaction.Complete();
            return result;
        }
        catch
        {
            throw;
        }
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (!_disposed)
        {
            if (disposing)
            {
                _dbContext?.Dispose(); 
            }

            _disposed = true;
        }
    }
 
    public Task RollBackAsync()
    {
        throw new NotImplementedException();
    }


}