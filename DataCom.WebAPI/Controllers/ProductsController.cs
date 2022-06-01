using AutoMapper;

using DataCom.WebAPI.Models;
using DataCom.WebAPI.Requests;
using DataCom.WebAPI.Services;
using Microsoft.AspNetCore.Mvc;
using Product = DataCom.WebAPI.Models.Product;

namespace DataCom.WebAPI.Controllers
{
    [Route("/products")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly IMapper _mapper;

        public ProductsController(IProductService productService, IMapper mapper)
        {
            _productService = productService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<Products> GetAll([FromQuery] string? name)
        {
            var items = (name is null) ? await _productService.FindAllAsync() 
                : await _productService.FindByNameAsync(name);
            
            return new Products(_mapper.Map<IEnumerable<Product>>(items));
        }


        [Route("{id:guid}")]
        [HttpGet]
        public async Task<ActionResult<Product>> GetProduct(Guid id)
        {
            var product = await _productService.FindByIdAsync(id);
            if (product == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<Product>(product));
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ProductRequest request)
        {
            var productId = Guid.NewGuid(); 
            var product = _mapper.Map<Entity.Product>(request);
            product.Id = productId;

            await _productService.AddAsync(product);

            return CreatedAtAction("GetProduct", "Products", new {id = productId}, null);
        }

        [Route("{id:guid}")]
        [HttpPut]
        public async Task<IActionResult> Update(Guid id, [FromBody] ProductRequest request)
        {
            var product = await _productService.FindByIdAsync(id);
            
            if (product == null) return NotFound();
            
            _mapper.Map(request, product);

            await _productService.UpdateAsync(product);
            return NoContent();

        }

        [Route("{id:guid}")]
        [HttpDelete]
        public async Task<IActionResult> Delete(Guid id)
        {
            var product = await _productService.FindByIdAsync(id);
            if (product == null)
            {
                return NotFound();
            }

            await _productService.DeleteAsync(product.Id);
            return NoContent();
        }
    }
}