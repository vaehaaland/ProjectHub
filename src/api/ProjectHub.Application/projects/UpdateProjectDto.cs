using System.ComponentModel.DataAnnotations;

namespace ProjectHub.Application.Projects;

public class UpdateProjectDto
{
  [MaxLength(200)]
  public string? Name { get; set; } = null;

  [MaxLength(2000)]
  public string? Description { get; set; } = null;
}
