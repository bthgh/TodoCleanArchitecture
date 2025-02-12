using TodoCleanArchitecture.Domain.Common;

namespace TodoCleanArchitecture.Domain.Features.Todos;

public class TodoItem : BaseAuditableEntity<int>
{ 
    public required string Title { get; set; }
    
    public string? Note { get; set; }
    
    public Priority Priority { get; set; }

    public DateTime? DueDate { get; set; }
    
    public bool IsCompleted { get; set; }
    
    public DateTime? CompletedAt { get; set; } 
}