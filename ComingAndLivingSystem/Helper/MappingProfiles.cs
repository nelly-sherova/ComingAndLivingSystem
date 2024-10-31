using AutoMapper;
using ComingAndLivingSystem.DTO;
using ComingAndLivingSystem.Models;

namespace ComingAndLivingSystem.Helper
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles() 
        {
            CreateMap<Employee, EmployeeDTO>().ReverseMap(); 
            CreateMap<JobTitle, JobTitleDTO>(); 
            CreateMap<Shift, ShiftDTO>();   
        }
    }
}
