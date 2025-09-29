using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProjectHub.Domain.Entities;
using TaskStatus = ProjectHub.Domain.Entities.TaskStatus;

namespace ProjectHub.Infrastructure.Configurations;

internal sealed class TaskItemConfiguration : IEntityTypeConfiguration<TaskItem>
{
  public void Configure(EntityTypeBuilder<TaskItem> builder)
  {
    builder.ToTable("TaskItems");

    builder.HasKey(t => t.Id);

    builder.Property(t => t.Title)
           .IsRequired()
           .HasMaxLength(200);

    builder.Property(t => t.Description)
           .HasMaxLength(4000);

    builder.Property(t => t.Status)
           .HasConversion<string>()
           .HasMaxLength(32)
           .HasDefaultValue(TaskStatus.Todo);

    builder.Property(t => t.Created)
           .HasDefaultValueSql("SYSUTCDATETIME()")
           .ValueGeneratedOnAdd();

    builder.Property(t => t.Updated)
           .HasDefaultValueSql("SYSUTCDATETIME()")
           .ValueGeneratedOnAddOrUpdate();

    builder.HasIndex(t => new { t.ProjectId, t.Status });
  }
}
