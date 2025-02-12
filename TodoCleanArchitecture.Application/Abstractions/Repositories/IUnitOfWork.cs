namespace TodoCleanArchitecture.Application.Abstractions.Repositories;

public interface IUnitOfWork
{
    public ITodoItemRepository TodoItemRepository { get; }
    Task<int> CommitAsync();
    Task RollBackAsync();
}