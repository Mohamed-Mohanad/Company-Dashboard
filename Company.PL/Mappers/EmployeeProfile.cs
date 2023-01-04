using AutoMapper;
using Company.DAL.Entities;
using Company.PL.Models;

namespace Company.PL.Mappers
{
    public class EmployeeProfile : Profile
    {
        public EmployeeProfile()
        {
            CreateMap<Employee, EmployeeViewModel>().ReverseMap();
        }
    }
}
