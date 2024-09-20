using AutoMapper;
using EducationCourseApp.Catalog.Entities;
using MongoDB.Driver;

namespace EducationCourseApp.Catalog.Services;

public class CourseService : ICourseService
{
    private readonly IMongoCollection<Category> _categoryColleciton;
    private readonly IMapper _mapper;

    public CourseService(IMapper mapper, IMongoCollection<Category> categoryColleciton)
    {
        _mapper = mapper;
        _categoryColleciton = categoryColleciton;
    }
}