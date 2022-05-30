using BlogManager.Domain.Entities.Catalog;
using AutoMapper;
using BlogManager.Application.Features.Employees.Commands.Create;
using BlogManager.Application.Features.Employees.Queries.GetById;
using BlogManager.Application.Features.Employees.Queries.GetAllCached;
using BlogManager.Application.Features.Employees.Queries.GetAllPaged;
using BlogManager.Application.Features.Employees.Queries.GetAllActive;
using BlogManager.Application.Features.Employees.Queries.GetAllDelete;
using BlogManager.Application.Features.Employees.Commands.Update;
using BlogManager.Application.Features.Employees.Queries.GetByDepartmentId;
using BlogManager.Application.Features.Employees.Queries.GetAllJs;

namespace BlogManager.Application.Mappings
{
    internal class EmployeeProfile : Profile
    {
        public EmployeeProfile()
        {
            CreateMap<CreateEmployeeCommand, Employee>().ReverseMap();
            CreateMap<GetAllJsRepose, Employee>().ReverseMap();
            CreateMap<GetAllDeleteReponse, Employee>().ReverseMap();
            CreateMap<GetAllActiveRpsponse, Employee>().ReverseMap();
            CreateMap<GetEmployeeByDepartmentIdQuery, Employee>().ReverseMap();
            CreateMap<UpdateEmployeeCommand, Employee>().ReverseMap();
            CreateMap<GetAllEmployeeCachedResponse, Employee>().ReverseMap();
            CreateMap<GetAllEmployeeResponse, Employee>().ReverseMap();
            CreateMap<GetEmployeeByIdResponse, Employee>().ReverseMap();
        }
    }
}
