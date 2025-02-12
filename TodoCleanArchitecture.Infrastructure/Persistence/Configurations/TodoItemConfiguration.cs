using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TodoCleanArchitecture.Domain.Features.Todos;

namespace TodoCleanArchitecture.Infrastructure.Persistence.Configurations;

public class AccountSettingConfiguration : IEntityTypeConfiguration<TodoItem>
{
    public void Configure(EntityTypeBuilder<TodoItem> builder)
    {
        builder.ToTable("TodoItems");

        builder.HasKey(e => e.Id);
 
        builder.Property(e => e.Title).HasMaxLength(200).IsRequired();
        builder.Property(e => e.Note).HasMaxLength(1000);
        builder.Property(e => e.IsCompleted).HasDefaultValue(false);
    }
}