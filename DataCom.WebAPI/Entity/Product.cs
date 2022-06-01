namespace DataCom.WebAPI.Entity
{
    public class Product
    {
        public Product()
        {
            Id = Guid.NewGuid();
        }

        public List<ProductOption> Options { get; set; }

        public Guid Id { get; set; }

        public string Name { get; set; }

        public string? Description { get; set; }

        public decimal Price { get; set; }

        public decimal DeliveryPrice { get; set; }
    }
}