namespace DataCom.WebAPI.Entity;

public class ProductOption
{
    public ProductOption()
    {
        Id = Guid.NewGuid();
    }

    public Guid Id { get; set; }

    public string Name { get; set; }

    public string Description { get; set; }

    public Guid ProductId { get; set; }
    public Product Product { get; set; }
}