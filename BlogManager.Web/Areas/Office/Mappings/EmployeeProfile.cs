using AutoMapper;
using BlogManager.Application.Features.Employees.Commands.Create;
using BlogManager.Application.Features.Employees.Commands.Update;
using BlogManager.Application.Features.Employees.Queries.GetAllCached;
using BlogManager.Application.Features.Employees.Queries.GetAllPaged;
using BlogManager.Application.Features.Employees.Queries.GetByDepartmentId;
using BlogManager.Application.Features.Employees.Queries.GetById;
using BlogManager.Web.Areas.Office.Models;

namespace BlogManager.Web.Areas.Office.Mappings
{
    internal class EmployeeProfile : Profile
    {
        public EmployeeProfile()
        {
            CreateMap<CreateEmployeeCommand, EmployeeViewModel>().ReverseMap();
            CreateMap<GetEmployeeByDepartmentIdQuery, EmployeeViewModel>().ReverseMap();
            CreateMap<UpdateEmployeeCommand, EmployeeViewModel>().ReverseMap();
            CreateMap<GetAllEmployeeCachedResponse, EmployeeViewModel>().ReverseMap();
            CreateMap<GetAllEmployeeResponse, EmployeeViewModel>().ReverseMap();
            CreateMap<GetEmployeeByIdResponse, EmployeeViewModel>().ReverseMap();
        }
    }
}
