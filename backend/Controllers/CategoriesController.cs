using backend.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace backend.Controllers;

[ApiController]
[Route("api/[controller]")] 
public class CategoriesController : ControllerBase
{
    private readonly MyDbContext _context;

    public CategoriesController(MyDbContext context)
    {
        _context = context;
    }

    // GET: api/Categories
    [HttpGet]
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
    public async Task<ActionResult<Category>> CreateCategory(Category category)
    {
        _context.Categories.Add(category); 
        await _context.SaveChangesAsync();
        
        return CreatedAtAction(nameof(GetCategory), new { id = category.Id }, category);
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
        var category = await _context.Categories.FindAsync(id);
        if (category == null)
        {
            return NotFound();
        }
        
        _context.Categories.Remove(category);
        await _context.SaveChangesAsync();
        
        return NoContent();
    }

    private bool CategoryExists(int id)
    {
        return _context.Categories.Any(e => e.Id == id);
    }
}