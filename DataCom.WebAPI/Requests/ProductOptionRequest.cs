using System.ComponentModel.DataAnnotations;

namespace DataCom.WebAPI.Requests;

public class ProductOptionRequest
{
    [Required]
    public string Name { get; set; }
    
    [Required]
    public string Description { get; set; }
}