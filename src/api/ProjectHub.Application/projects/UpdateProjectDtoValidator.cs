using FluentValidation;

namespace ProjectHub.Application.Projects;

public sealed class UpdateProjectDtoValidator : AbstractValidator<UpdateProjectDto>
{
  public UpdateProjectDtoValidator()
  {
    RuleFor(x => x.Name)
        .MaximumLength(200)
        .When(x => x.Name is not null);

    RuleFor(x => x.Description)
        .MaximumLength(2000)
        .When(x => x.Description is not null);

    RuleFor(x => x)
        .Must(dto => dto.Name is not null || dto.Description is not null)
        .WithMessage("At least one field must be provided.");
  }
}
