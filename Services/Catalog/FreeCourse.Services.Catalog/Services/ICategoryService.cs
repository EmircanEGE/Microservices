using FreeCourse.Services.Catalog.Dtos;
using FreeCourse.Shared;

namespace FreeCourse.Services.Catalog.Services;

public interface ICategoryService
{
    Task<Response<List<CategoryDto>>> GetAllAsync();
    Task<Response<CategoryDto>> CreateAsync(CategoryDto categoryDto);
    Task<Response<CategoryDto>> GetByIdAsync(string id);
}