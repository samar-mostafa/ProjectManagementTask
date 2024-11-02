using AutoMapper;
using ProjectManagement.Models;
using ProjectManagement.Dtos;
using ProjectManagement.Enums;

namespace ProjectManagement.Helpers
{
    public class DbContextProfile : Profile 
    {
        public DbContextProfile()
        {

            CreateMap<Project, ProjectDto>();
            CreateMap<AddProjectDto,Project>();
            CreateMap<EditProjectDto, Project>().ReverseMap();

            CreateMap<ProjectManagement.Models.Task, TaskDto>()
                .ForMember(des=>des.ProjectName,op=>op.MapFrom(src=>src.Project.Name))
                .ForMember(des => des.Status, op => op.MapFrom(src =>
                Enum.GetName(typeof(TaskStatusEnum),src.Status)));
            CreateMap<AddTaskDto, ProjectManagement.Models.Task>();
            CreateMap<EditTaskDto, ProjectManagement.Models.Task>();

            CreateMap<AddUserDto, User>();
            CreateMap<User, UserDto>();

        }
    }
}
