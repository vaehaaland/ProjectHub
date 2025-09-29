using System.ComponentModel.DataAnnotations;

namespace ProjectHub.Domain.Entities;

public class Project
{
  public Guid Id { get; set; } = Guid.NewGuid();

  [Required, MaxLength(200)]
  public string Name { get; set; } = string.Empty;

  [MaxLength(2000)]
  public string? Description { get; set; }

  public DateTimeOffset Created { get; set; } = DateTimeOffset.UtcNow;
  public DateTimeOffset Updated { get; set; } = DateTimeOffset.UtcNow;

  public ICollection<TaskItem> Tasks { get; set; } = new List<TaskItem>();
}
