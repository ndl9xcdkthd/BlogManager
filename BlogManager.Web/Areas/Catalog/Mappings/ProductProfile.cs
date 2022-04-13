using BlogManager.Application.Features.Products.Commands.Create;
using BlogManager.Application.Features.Products.Commands.Update;
using BlogManager.Application.Features.Products.Queries.GetAllCached;
using BlogManager.Application.Features.Products.Queries.GetById;
using BlogManager.Web.Areas.Catalog.Models;
using AutoMapper;

namespace BlogManager.Web.Areas.Catalog.Mappings
{
    internal class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<GetAllProductsCachedResponse, ProductViewModel>().ReverseMap();
            CreateMap<GetProductByIdResponse, ProductViewModel>().ReverseMap();
            CreateMap<CreateProductCommand, ProductViewModel>().ReverseMap();
            CreateMap<UpdateProductCommand, ProductViewModel>().ReverseMap();
        }
    }
}