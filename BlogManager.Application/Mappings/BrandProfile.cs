using BlogManager.Application.Features.Brands.Commands.Create;
using BlogManager.Application.Features.Brands.Queries.GetAllCached;
using BlogManager.Application.Features.Brands.Queries.GetById;
using BlogManager.Domain.Entities.Catalog;
using AutoMapper;

namespace BlogManager.Application.Mappings
{
    internal class BrandProfile : Profile
    {
        public BrandProfile()
        {
            CreateMap<CreateBrandCommand, Brand>().ReverseMap();
            CreateMap<GetBrandByIdResponse, Brand>().ReverseMap();
            CreateMap<GetAllBrandsCachedResponse, Brand>().ReverseMap();
        }
    }
}