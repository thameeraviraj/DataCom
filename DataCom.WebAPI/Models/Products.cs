namespace DataCom.WebAPI.Models;

public class Products : Resource<Product>
{
    public Products(IEnumerable<Product> items) : base(items)
    {
    }
}