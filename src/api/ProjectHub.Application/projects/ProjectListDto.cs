namespace ProjectHub.Application.Projects;

public class ProjectListDto
{
  public Guid Id { get; set; }
  public string Name { get; set; } = string.Empty;
  public string? Description { get; set; } = null;
  public DateTimeOffset Created { get; set; } = DateTimeOffset.UtcNow;
  public DateTimeOffset Updated { get; set; } = DateTimeOffset.UtcNow;
}
