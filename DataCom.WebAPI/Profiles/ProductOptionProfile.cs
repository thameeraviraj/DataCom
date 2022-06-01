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
        
        CreateMap<Entity.ProductOption, Entity.ProductOption>()
            .ForMember(m => m.Id, opt => opt.Ignore())
            .ForMember(m => m.ProductId, opt => opt.Ignore())
            .ForMember(m => m.Product, opt => opt.Ignore());
    }
}