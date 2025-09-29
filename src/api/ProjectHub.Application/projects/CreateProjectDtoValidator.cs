using FluentValidation;
namespace ProjectHub.Application.Projects;

public sealed class CreateProjectDtoValidator : AbstractValidator<CreateProjectDto>
{
  public CreateProjectDtoValidator()
  {
    RuleFor(x => x.Name)
        .NotEmpty()
        .MaximumLength(200);

    RuleFor(x => x.Description)
        .MaximumLength(2000);
  }
}
