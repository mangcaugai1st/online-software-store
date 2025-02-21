using System.Runtime.InteropServices.JavaScript;
using backend.Models;
using backend.Models.Entities;
using backend.Models.DTOs;
using backend.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace backend.Controllers;

[ApiController]
[Route("api/[controller]")] 
public class CategoriesController : ControllerBase
{
    private readonly ApplicationDbContext _context;
    private readonly ICategoryService _categoryService;
    
    public CategoriesController(ICategoryService categoryService, ApplicationDbContext context)
    {
        _categoryService = categoryService ?? throw new ArgumentNullException(nameof(categoryService));
        _context = context;
    }
    
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Category>>> GetCategories()
    {
        var categories = await _categoryService.GetAllCategoriesAsync();
        
        return Ok(categories);
    }
    
    [HttpGet("{id}")]
    public async Task<ActionResult<Category>> GetCategoryById(int id)
    {
        var category = await _categoryService.GetCategoryByIdAsync(id);

        if (category == null)
        {
            return NotFound();
        }
        return Ok(category);
    }

    [HttpGet("category/{categorySlug}")]
    public async Task<ActionResult<IEnumerable<Product>>> GetProductsByCategory(string categorySlug)
    {
        var category = await _context.Categories
            .FirstOrDefaultAsync(c => c.Slug == categorySlug);
        
        var products = await _context.Products
            .Where(p => p.CategoryId == category.Id)
            .ToListAsync();
 
        return Ok(products);
    }
    
    [HttpPost]
    public async Task<ActionResult<Category>> AddCategory([FromBody] CategoryDto categoryDto)
    {
        if (categoryDto == null)
        {
            return BadRequest("Category data is required.");
        }
        
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        try
        {
            var createdCategory = await _categoryService.AddCategoryAsync(categoryDto);
            
            return Ok();
        }
        catch (Exception ex)
        {
            return StatusCode(500, new {message = "Có lỗi xảy ra khi thêm danh mục: " + ex.Message});
        }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateCategory(int id, [FromBody] CategoryDto categoryDto)
    {
        if (categoryDto == null)
        {
            return BadRequest("Dữ liệu không được để trống");
        }

        try
        {
            var updatedCategory = await _categoryService.UpdateCategoryAsync(id, categoryDto);
            return Ok(updatedCategory); // trả về đối tượng danh mục đã cập nhật 
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(ex.Message); // trả về lỗi 404 nếu không tìm thấy danh mục
        }
        catch (Exception ex)
        {
            return StatusCode(500, new {message = ex.Message}); // trả về lỗi server
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCategory(int id)
    {
        var selectedCategory = await _categoryService.DeleteCategoryAsync(id);
        if (!selectedCategory)
        {
            return NotFound($"Không tìm thấy danh mục {id}");
        }
        
        return NoContent();
    }
}

// nameof(GetCategory): Tham số đầu tiên chỉ định tên action method sẽ được sử dụng tạo URL
// nameof: là một operator trong c# trả về tên của method dưới dạng string.
// CreatedAtAction(string? actionName, object? value);
// Lambda expression: (input-parameters) => expression

// Ex:
// int[] numbers = {2,3,4,5};
// var squaredNumbers = numbers.Select(x => x * x); 
// Console.WriteLine(string.Join(" ", squaredNumbers));

// BadRequest: trả về lỗi 400 (null)
// NotFound: trả về lỗi 404 (Not Found)
// Internal Server Error: trả về lỗi 500 (lỗi khác trong quá trình xử lý)