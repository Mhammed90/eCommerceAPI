using eCommerce.Application.DTOs.Product;
using eCommerce.Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace eCommerce.Presentation.Controller;

[Route("api/[controller]")]
[ApiController]
public class ProductController : ControllerBase
{
    private readonly IProductService _productService;

    public ProductController(IProductService productService)
    {
        _productService = productService;
    }

    [HttpGet("all")]
    public async Task<IActionResult> GetAll()
    {
        var result = await _productService.GetAllAsync();
        return (!result.Any() ? NotFound() : Ok(result));
    }

    [HttpGet("single/{id}")]
    public async Task<IActionResult> GetOne(Guid id)
    {
        var result = await _productService.GetByIdAsync(id);
        return result != null ? Ok(result) : NotFound(result);
    }

    [HttpPost("add")]
    public async Task<IActionResult> Create(CreateProduct product)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        var result = await _productService.AddAsync(product);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    [HttpPut("update")]
    public async Task<IActionResult> Update(UpdateProduct product)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        var result = await _productService.UpdateAsync(product);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    [HttpDelete("delete/{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var result = await _productService.DeleteAsync(id);
        return result.Success ? Ok(result) : BadRequest(result);
    }
}