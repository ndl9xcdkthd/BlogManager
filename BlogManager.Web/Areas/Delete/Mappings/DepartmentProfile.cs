using AutoMapper;
using BlogManager.Application.Features.Departments.Commands.Create;
using BlogManager.Application.Features.Departments.Commands.Update;
using BlogManager.Application.Features.Departments.Queries.GetAllCached;
using BlogManager.Application.Features.Departments.Queries.GetById;
using BlogManager.Web.Areas.Delete.Models;

namespace BlogManager.Web.Areas.Delete.Mappings
{
    internal class DepartmentProfile : Profile
    {
        public DepartmentProfile()
        {
            CreateMap<GetAllDepartmentCacheRepose, DepartmentViewModel>().ReverseMap();
            CreateMap<GetDepartmentByIdResponse, DepartmentViewModel>().ReverseMap();
            CreateMap<CreateDepartmentCommand, DepartmentViewModel>().ReverseMap();
            CreateMap<UpdateDepartmentCommand, DepartmentViewModel>().ReverseMap();
        }
    }
}
