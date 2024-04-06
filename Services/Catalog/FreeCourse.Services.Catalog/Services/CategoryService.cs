﻿using AutoMapper;
using FreeCourse.Services.Catalog.Dtos;
using FreeCourse.Services.Catalog.Models;
using FreeCourse.Services.Catalog.Settings;
using FreeCourse.Shared;
using MongoDB.Driver;

namespace FreeCourse.Services.Catalog.Services;

public class CategoryService : ICategoryService
{
    private readonly IMongoCollection<Category> _categoryCollection;
    private readonly IMapper _mapper;

    public CategoryService(IMapper mapper, IDatabaseSettings databaseSettings)
    {
        var client = new MongoClient(databaseSettings.ConnectionString);
        var dataBase = client.GetDatabase(databaseSettings.DatabaseName);

        _categoryCollection = dataBase.GetCollection<Category>(databaseSettings.CategoryCollectionName);
        _mapper = mapper;
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

        return Response<CategoryDto>.Success(_mapper.Map<CategoryDto>(categoryDto), 200);
    }

    public async Task<Response<CategoryDto>> GetByIdAsync(string id)
    {
        var category = await _categoryCollection.Find(x => x.Id == id).FirstOrDefaultAsync();
        if (category == null) return Response<CategoryDto>.Fail("Category not found", 404);

        return Response<CategoryDto>.Success(_mapper.Map<CategoryDto>(category), 200);
    }
}