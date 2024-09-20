using EducationCourseApp.Catalog.Dtos;
using EducationCourseApp.Shared.Dtos;

namespace EducationCourseApp.Catalog.Services;

public interface ICourseService
{
    
    Task<Response<List<CourseDto>>> GetAllAsync();
    Task<Response<CourseDto>> GetByIdAsync(string id);
    Task<Response<CourseDto>> CreateAsync(CourseCreateDto courseCreateDto);
    Task<Response<NoContent>> UpdateAsync(CourseUpdateDto courseUpdateDto);
    Task<Response<NoContent>> DeleteAsync(string id);
    Task<Response<List<CourseDto>>> GetAllByUserId(string userId);
}