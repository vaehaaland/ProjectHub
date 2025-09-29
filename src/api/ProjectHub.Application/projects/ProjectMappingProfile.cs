using AutoMapper;
using ProjectHub.Domain.Entities;

namespace ProjectHub.Application.Projects;

public sealed class ProjectMappingProfile : Profile
{
  public ProjectMappingProfile()
  {
    // Domain → DTO
    CreateMap<Project, ProjectDto>();

    CreateMap<Project, ProjectListDto>();

    // Create DTO → Domain
    CreateMap<CreateProjectDto, Project>()
      .ForMember(dest => dest.Id, opt => opt.Ignore())
      .ForMember(dest => dest.Tasks, opt => opt.Ignore())
      .ForMember(dest => dest.Created, opt => opt.Ignore())
      .ForMember(dest => dest.Updated, opt => opt.Ignore());

    // Update DTO → Domain
    CreateMap<UpdateProjectDto, Project>()
      .ForMember(dest => dest.Id, opt => opt.Ignore())
      .ForMember(dest => dest.Tasks, opt => opt.Ignore())
      .ForMember(dest => dest.Created, opt => opt.Ignore())
      .ForMember(dest => dest.Updated, opt => opt.Ignore())
      .ForAllMembers(opt => opt.Condition((_, _, srcMember) => srcMember is not null));
  }
}
