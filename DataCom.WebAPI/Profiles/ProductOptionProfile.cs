using AutoMapper;
using DataCom.WebAPI.Models;
using DataCom.WebAPI.Requests;

namespace DataCom.WebAPI.Profiles;

public class ProductOptionProfile : Profile
{
    public ProductOptionProfile()
    {
        CreateMap<ProductOptionRequest, Entity.ProductOption>();
        CreateMap<Entity.ProductOption, ProductOption>();
    }
}