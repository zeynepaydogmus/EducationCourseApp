using EducationCourseApp.Catalog.Dtos;
using EducationCourseApp.Shared.Dtos;

namespace EducationCourseApp.Catalog.Services;

public interface ICategoryService
{
    Task<Response<List<CategoryDto>>> GetAllAsync();
    Task<Response<CategoryDto>> CreateAsync(CategoryDto categoryDto);
    Task<Response<CategoryDto>> GetById(string id);
}