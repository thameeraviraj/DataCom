using AutoMapper;
using DataCom.WebAPI.Exceptions;
using DataCom.WebAPI.Models;
using DataCom.WebAPI.Requests;
using DataCom.WebAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace DataCom.WebAPI.Controllers;

[Route("/products/{productId:guid}/options")]
[ApiController]
public class ProductOptionsController : ControllerBase
{
    private readonly IProductOptionService _optionService;
    private readonly IMapper _mapper;

    public ProductOptionsController(IProductOptionService optionService, IMapper mapper)
    {
        _optionService = optionService;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<ProductOptions> GetOptions(Guid productId)
    {
        var options = await _optionService.GetByProductId(productId);
        return new ProductOptions(_mapper.Map<IEnumerable<ProductOption>>(options));
    }

    [Route("{optionId:guid}")]
    [HttpGet]
    public async Task<ActionResult<ProductOption>> GetOption(Guid productId, Guid optionId)
    {
        var option = await _optionService.FindByProductIdAndOptionIdAsync(productId, optionId);
        if (option == null)
        {
            NotFound();
        }

        return Ok(_mapper.Map<ProductOption>(option));
    }

    [HttpPost]
    public async Task<IActionResult> CreateOption(Guid productId, [FromBody] ProductOptionRequest request)
    {
        var optionId = Guid.NewGuid();
        
        var option = _mapper.Map<Entity.ProductOption>(request);
        await _optionService.AddByProductIdAsync(productId, option);

        return CreatedAtAction("GetOption", "ProductOptions", new
        {
            productId = productId,
            optionId = optionId
        });
    }

    [Route("{optionId:guid}")]
    [HttpPut]
    public async Task<IActionResult> UpdateOption(Guid productId, Guid optionId,
        [FromBody] ProductOptionRequest request)
    {
        await EnsureOptionExistsByProductIdAndOptionId(productId, optionId);

        var option = _mapper.Map<Entity.ProductOption>(request);
        option.Id = optionId;

        await _optionService.UpdateAsync(option);

        return NoContent();
    }

    [Route("{optionId:guid}")]
    [HttpDelete]
    public async Task<IActionResult> DeleteOption(Guid productId, Guid optionId)
    {
        await EnsureOptionExistsByProductIdAndOptionId(productId, optionId);
        
        await _optionService.DeleteByOptionIdAsync(optionId);
        return NoContent();
    }

    private async Task EnsureOptionExistsByProductIdAndOptionId(Guid productId, Guid optionId)
    {
        if (!await _optionService.ExistsByProductIdAndOptionIdAsync(productId, optionId))
        {
            throw new ResourceNotFoundException();
        }
    }
}