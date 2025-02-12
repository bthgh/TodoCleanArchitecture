using TodoCleanArchitecture.Application.Models.Common;
using TodoCleanArchitecture.Domain.Features.Todos;

namespace TodoCleanArchitecture.Application.Abstractions.Repositories;

public interface ITodoItemRepository
{
    Task<TodoItem?> GetByIdAsync(int id, bool trackEntity = false);
    Task<PaginatedList<TodoItem>> GetAllAsync(int pageNumber, int pageSize);
    Task CreateAsync(TodoItem bannedWord);
    Task DeleteAsync(int id);
}