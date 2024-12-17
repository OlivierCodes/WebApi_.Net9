using AutoMapper;
using Microsoft.Identity.Client;
using WebApi.src.Domain.Contract;
using WebApi.src.Domain.Entities;
using static WebApi.src.Domain.Contract.ProductsContracts;

namespace WebApi.src.Infrastrure.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Product, ProductResponse>();
            CreateMap<CreateProductRequest, Product>();
            CreateMap<UpdateProductRequest, Product>();
        }
    }
}
