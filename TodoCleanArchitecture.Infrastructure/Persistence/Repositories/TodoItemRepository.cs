using Microsoft.EntityFrameworkCore;
using TodoCleanArchitecture.Application.Abstractions.Repositories;
using TodoCleanArchitecture.Application.Extensions;
using TodoCleanArchitecture.Application.Models.Common;
using TodoCleanArchitecture.Domain.Features.Todos;
using TodoCleanArchitecture.Infrastructure.Persistence.Data;
using TodoCleanArchitecture.Infrastructure.Persistence.Repositories.Common;

namespace TodoCleanArchitecture.Infrastructure.Persistence.Repositories;

internal class TodoItemRepository(ApplicationDbContext dbContext)
    : BaseAsyncRepository<TodoItem, ApplicationDbContext>(dbContext), ITodoItemRepository
{

    public async Task<TodoItem?> GetByIdAsync(int id, bool trackEntity = false)
    {
        var todoItem = await base.TableNoTracking.FirstOrDefaultAsync(c => c.Id == id);

        if (todoItem is not null && trackEntity)
            DbContext.Attach(todoItem);

        return todoItem;
    }

    public async Task<PaginatedList<TodoItem>> GetAllAsync(int pageNumber, int pageSize)
    {
        return await base.TableNoTracking.PaginatedListAsync(pageNumber, pageSize);
    }


    public async Task CreateAsync(TodoItem bannedWord)
    {
        await base.AddAsync(bannedWord);
    }

    public async Task DeleteAsync(int id)
    {
        await base.DeleteAsync(c => c.Id == id);
    } 
}