using AutoMapper;
using Demo_DAL.Models;
using Demo_PL.ViewModels;

namespace Demo_PL.Helpers
{
    public class MappingPorfiles : Profile
    {
        public MappingPorfiles()
        {
            CreateMap<EmployeeViewModel, Employee>().ReverseMap();
                //.ForMember(d => d.Name, o => o.MapFrom(s => s.EmpType));
        }
    }
}
