using Microsoft.EntityFrameworkCore;
using TodoCleanArchitecture.Domain.Features.Todos;

namespace TodoCleanArchitecture.Application.Abstractions.Data;

public interface IApplicationDbContext
{

    DbSet<TodoItem> TodoItems { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}