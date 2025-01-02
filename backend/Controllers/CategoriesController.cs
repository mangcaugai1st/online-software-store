using System.Runtime.InteropServices.JavaScript;
using backend.Models;
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
    
    // public CategoriesController(ApplicationDbContext context)
    // {
    //     _context = context;
    // }
    
    public CategoriesController(ICategoryService categoryService, ApplicationDbContext context)
    {
        _categoryService = categoryService ?? throw new ArgumentNullException(nameof(categoryService));
        _context = context;
    }
    
    // GET: api/Categories
    [HttpGet]
    // public async Task<ActionResult<IEnumerable<CategoryDto>>> GetCategories()
    // {
    //     return await _context.Categories.Select(c => new CategoryDto
    //     {
    //         Id = c.Id,
    //         Name = c.Name, 
    //         Slug = c.Slug, 
    //         Description = c.Description,
    //     }).ToListAsync();
    // }
    public async Task<ActionResult<IEnumerable<Category>>> GetCategories()
    {
        // return await _context.Categories.ToListAsync();
        var categories = await _categoryService.GetAllCategoriesAsync();
        
        return Ok(categories);
    }

    // GET: api/Categories/{id}
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
    
    // POST: api/Categories
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

            // return CreatedAtAction(nameof(GetCategoryById), new { id = createdCategory.Id }, createdCategory);
            return Ok();
        }
        catch (Exception ex)
        {
            return StatusCode(500, new {message = "Có lỗi xảy ra khi thêm danh mục: " + ex.Message});
        }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateCategory(int id, Category category)
    {
        if (id != category.Id)
        {
            return BadRequest();
        }
        
        _context.Entry(category).State = EntityState.Modified;

        try
        { 
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        { 
            if (!CategoryExists(id))
            {
               return NotFound(); 
            }
            else
            { 
                throw;
            }
        }
        return NoContent(); 
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

    private bool CategoryExists(int id)
    {
        return _context.Categories.Any(e => e.Id == id);
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