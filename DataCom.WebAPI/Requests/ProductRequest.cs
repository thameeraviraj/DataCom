using System.ComponentModel.DataAnnotations;

namespace DataCom.WebAPI.Requests;

public class ProductRequest
{
    [Required]
    public string Name { get; set; }

    public string? Description { get; set; }

    [Required]
    [Range(0, double.MaxValue)]
    public decimal Price { get; set; }

    [Required]
    [Range(0, double.MaxValue)]
    public decimal DeliveryPrice { get; set; }
}