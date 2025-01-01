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
    
    public CategoriesController(ApplicationDbContext context)
    {
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
        return await _context.Categories.ToListAsync();     
    }
    
    // GET: api/Categories/{id}
    [HttpGet("{id}")]
    public async Task<ActionResult<Category>> GetCategory(int id)
    {
        var category = await _context.Categories.FindAsync(id);

        if (category == null)
        {
            return NotFound();
        }
        return category;
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
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        try
        {
            var newCategory = await _categoryService.AddCategoryAsync(categoryDto);
            // CreatedAtAction(string? actionName, object? value);
            return CreatedAtAction(nameof(GetCategory), new {id = categoryDto.Id}, newCategory);
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