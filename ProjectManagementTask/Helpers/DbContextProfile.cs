using AutoMapper;
using ProjectManagement.Models;
using ProjectManagementTask.Dtos;

namespace ProjectManagement.Helpers
{
    public class DbContextProfile : Profile 
    {
        public DbContextProfile()
        {

            CreateMap<Project, ProjectDto>();
            CreateMap<AddProjectDto,Project>();
            CreateMap<EditProjectDto, Project>();

        }
    }
}
