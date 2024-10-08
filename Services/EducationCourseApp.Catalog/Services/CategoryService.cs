﻿using AutoMapper;
using EducationCourseApp.Catalog.Dtos;
using EducationCourseApp.Catalog.Entities;
using EducationCourseApp.Catalog.Settings;
using EducationCourseApp.Shared.Dtos;
using MongoDB.Driver;

namespace EducationCourseApp.Catalog.Services;

public class CategoryService : ICategoryService
{
    private readonly IMongoCollection<Category> _categoryCollection;
    private readonly IMapper _mapper;

    public CategoryService(IMapper mapper, IDatabaseSettings databaseSettings)
    {
        var client = new MongoClient(databaseSettings.ConnectionString);
        var database = client.GetDatabase(databaseSettings.DatabaseName);
        
        _mapper = mapper;
        _categoryCollection = database.GetCollection<Category>(databaseSettings.CategoryCollectionName);
    }

    public async Task<Response<List<CategoryDto>>> GetAllAsync()
    {
        var categories = await _categoryCollection.Find(category => true).ToListAsync();
        return Response<List<CategoryDto>>.Success(_mapper.Map<List<CategoryDto>>(categories), 200);
    }

    public async Task<Response<CategoryDto>> CreateAsync(CategoryDto categoryDto)
    {
        var category = _mapper.Map<Category>(categoryDto);
        await _categoryCollection.InsertOneAsync(category);
        return Response<CategoryDto>.Success(_mapper.Map<CategoryDto>(categoryDto),200);
    }

    public async Task<Response<CategoryDto>> GetById(string id)
    {
        var category = await _categoryCollection.Find<Category>(x => x.Id == id).FirstOrDefaultAsync();
        if (category is null)
        {
            return Response<CategoryDto>.Fail("Category not found.", 404);
        }
        
        return Response<CategoryDto>.Success(_mapper.Map<CategoryDto>(category), 200);
    }

}