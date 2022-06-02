using AutoMapper;
using DataCom.WebAPI.Requests;
using Product = DataCom.WebAPI.Models.Product;

namespace DataCom.WebAPI.Profiles;

public class ProductProfile : Profile
{
    public ProductProfile()
    {
        CreateMap<ProductRequest, Entity.Product>();
        CreateMap<Entity.Product, Product>();
     }
}