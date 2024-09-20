using EducationCourseApp.Catalog.Dtos;
using EducationCourseApp.Catalog.Services;
using EducationCourseApp.Shared.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace EducationCourseApp.Catalog.Controllers;
[ApiController]
[Route("api/[controller]")]
public class CategoryController : CustomBaseController
{  
    private readonly ICategoryService _categoryService;

    public CategoryController(ICategoryService categoryService)
    {
        _categoryService = categoryService;
    }
    
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var response = await _categoryService.GetAllAsync();
        return CreateActionResultInstance(response);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(string id)
    {
        var response = await _categoryService.GetById(id);
        return CreateActionResultInstance(response);
    }
    [HttpPost]
    public async Task<IActionResult> Create(CategoryDto categoryDto)
    {
        var response = await _categoryService.CreateAsync(categoryDto);
        return CreateActionResultInstance(response);
    }
}