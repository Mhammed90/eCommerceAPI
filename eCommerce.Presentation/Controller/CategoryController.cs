using eCommerce.Application.DTOs.Category;
using eCommerce.Application.DTOs.Product;
using eCommerce.Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace eCommerce.Presentation.Controller;

[Route("api/[controller]")]
[ApiController]
public class CategoryController : ControllerBase
{
    private readonly ICategoryService _categoryService;

    public CategoryController(ICategoryService categoryService)
    {
        _categoryService = categoryService;
    }

    [HttpGet("all")]
    public async Task<IActionResult> GetAll()
    {
        var result = await _categoryService.GetAllAsync();
        return (!result.Any() ? NotFound() : Ok(result));
    }

    [HttpGet("single/{id}")]
    public async Task<IActionResult> GetOne(Guid id)
    {
        var result = await _categoryService.GetByIdAsync(id);
        return result != null ? Ok(result) : NotFound(result);
    }

    [HttpPost("add")]
    public async Task<IActionResult> Create(CreateCategory category)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _categoryService.AddAsync(category);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    [HttpPut("update")]
    public async Task<IActionResult> Update(UpdateCategory category)
    { 
        if (!ModelState.IsValid) return BadRequest(ModelState);

        var result = await _categoryService.UpdateAsync(category);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    [HttpDelete("delete/{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var result = await _categoryService.DeleteAsync(id);
        return result.Success ? Ok(result) : BadRequest(result);
    }
}