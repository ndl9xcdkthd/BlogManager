using BlogManager.Application.Features.Brands.Commands.Create;
using BlogManager.Application.Features.Brands.Commands.Update;
using BlogManager.Application.Features.Brands.Queries.GetAllCached;
using BlogManager.Application.Features.Brands.Queries.GetById;
using BlogManager.Web.Areas.Catalog.Models;
using AutoMapper;

namespace BlogManager.Web.Areas.Catalog.Mappings
{
    internal class BrandProfile : Profile
    {
        public BrandProfile()
        {
            CreateMap<GetAllBrandsCachedResponse, BrandViewModel>().ReverseMap();
            CreateMap<GetBrandByIdResponse, BrandViewModel>().ReverseMap();
            CreateMap<CreateBrandCommand, BrandViewModel>().ReverseMap();
            CreateMap<UpdateBrandCommand, BrandViewModel>().ReverseMap();
        }
    }
}