using AutoMapper;
using TaskSchedulerAPI.DtoModels;
using TaskSchedulerAPI.Model;

namespace TaskSchedulerAPI.Helper
{
    public class ApplicationMapper : Profile
    {
        public ApplicationMapper()
        {
            CreateMap<RegisterUserDto, User>();
            CreateMap<User, RegisterUserDto>();
            CreateMap<RegisterTaskDto, TaskAssignment>().ReverseMap();
           
        }
    }
}
