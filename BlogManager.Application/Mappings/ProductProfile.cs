
using BlogManager.Domain.Entities.Catalog;
using AutoMapper;
using BlogManager.Application.Features.Products.Commands.Create;
using BlogManager.Application.Features.Products.Queries.GetById;
using BlogManager.Application.Features.Products.Queries.GetAllCached;
using BlogManager.Application.Features.Products.Queries.GetAllPaged;

namespace BlogManager.Application.Mappings
{
    internal class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<CreateProductCommand, Product>().ReverseMap();
            CreateMap<GetProductByIdResponse, Product>().ReverseMap();
            CreateMap<GetAllProductsCachedResponse, Product>().ReverseMap();
            CreateMap<GetAllProductsResponse, Product>().ReverseMap();
        }
    }
}