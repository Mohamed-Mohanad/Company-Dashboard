using AutoMapper;
using Company.DAL.Entities;
using Company.PL.Models;

namespace Company.PL.Mappers
{
    public class DepartmentProfile : Profile
    {
        public DepartmentProfile()
        {
            CreateMap<Department, DepartmentViewModel>().ReverseMap();
        }
    }
}
