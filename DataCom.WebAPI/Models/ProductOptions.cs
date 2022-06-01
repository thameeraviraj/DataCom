namespace DataCom.WebAPI.Models;

public class ProductOptions : Resource<ProductOption>
{
    public ProductOptions(IEnumerable<ProductOption> items) : base(items)
    {
    }
}