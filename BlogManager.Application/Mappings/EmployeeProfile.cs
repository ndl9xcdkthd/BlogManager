using BlogManager.Domain.Entities.Catalog;
using AutoMapper;
using BlogManager.Application.Features.Employees.Commands.Create;
using BlogManager.Application.Features.Employees.Queries.GetById;
using BlogManager.Application.Features.Employees.Queries.GetAllCached;
using BlogManager.Application.Features.Employees.Queries.GetAllPaged;

namespace BlogManager.Application.Mappings
{
    internal class EmployeeProfile : Profile
    {
        public EmployeeProfile()
        {
            CreateMap<CreateEmployeeCommand, Employee>().ReverseMap();
            CreateMap<GetAllEmployeeCachedResponse, Employee>().ReverseMap();
            CreateMap<GetAllEmployeeResponse, Employee>().ReverseMap();
            CreateMap<GetEmployeeByIdResponse, Employee>().ReverseMap();
        }
    }
}
