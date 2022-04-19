using AutoMapper;
using BlogManager.Domain.Entities.Catalog;
using BlogManager.Application.Features.Departments.Commands.Create;
using BlogManager.Application.Features.Departments.Queries.GetById;
using BlogManager.Application.Features.Departments.Queries.GetAllCached;
using BlogManager.Application.Features.Departments.Queries.GetAllPaged;
using BlogManager.Application.Features.Employees.Queries.GetByDepartmentId;

namespace BlogManager.Application.Mappings
{
    internal class DepartmentProfile : Profile
    {
        public DepartmentProfile()
        {
            CreateMap<CreateDepartmentCommand, Department>().ReverseMap();
            CreateMap<GetDepartmentByIdResponse, Department>().ReverseMap();
            CreateMap<GetAllDepartmentCacheRepose, Department>().ReverseMap();
            CreateMap<GetAllDepartmentRepose, Department>().ReverseMap();
        }
    }
}
