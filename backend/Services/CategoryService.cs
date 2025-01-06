using backend.Models;
using backend.Models.DTOs;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace backend.Services;

public interface ICategoryService
{
    Task<IEnumerable<Category>> GetAllCategoriesAsync();
    Task<Category> GetCategoryByIdAsync(int id);
    // Task<Category> GetCategoryBySlugAsync(string slug);
    Task<Category> AddCategoryAsync(CategoryDto categoryDto); 
    Task<Category> UpdateCategoryAsync(int id, CategoryDto categoryDto);
    Task<bool> DeleteCategoryAsync(int id);
}

public class CategoryService : ICategoryService
{
    private readonly ApplicationDbContext _context;
    
    public CategoryService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Category>> GetAllCategoriesAsync()
    {
        var categories = await _context.Categories.ToListAsync();          
        
        return  categories;
    }

    public async Task<Category> GetCategoryByIdAsync(int id)
    {
        var result = await _context.Categories.FirstOrDefaultAsync(category => category.Id == id);
        
        return result;
    }
    
    public async Task<Category> AddCategoryAsync(CategoryDto categoryDto)
    {
        var category = new Category
        {
            Name = categoryDto.Name,
            Slug = categoryDto.Slug,
            Description = categoryDto.Description,
        };
        
        await _context.Categories.AddAsync(category);
        await _context.SaveChangesAsync();
        
        return category;
    }

    public async Task<Category> UpdateCategoryAsync(int id, CategoryDto categoryDto)
    {
        var category = await _context.Categories.FindAsync(id);

        if (category == null)
        {
            throw new KeyNotFoundException($"Không tìm thấy danh mục với id: {id}");
        }

        category.Name = categoryDto.Name;
        category.Description = categoryDto.Description;
        category.Slug = categoryDto.Slug;
        
        _context.Categories.Update(category);
        await _context.SaveChangesAsync();

        return category;
    }

    public async Task<bool> DeleteCategoryAsync(int id)
    {
        var category = await _context.Categories.FindAsync(id);

        if (category == null)
        {
            return false;
        }
        
        _context.Categories.Remove(category);
        await _context.SaveChangesAsync();
        
        return true;
    }
}