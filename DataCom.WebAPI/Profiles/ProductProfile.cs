using AutoMapper;
using DataCom.WebAPI.Entity;
using DataCom.WebAPI.Models;
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