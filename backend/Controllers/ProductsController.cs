using backend.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace backend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductsController : ControllerBase
{
    private readonly MyDbContext _context;

    public ProductsController(MyDbContext context)
    {
        _context = context;
    }
    
    // GET: api/Products
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
    {
        return await _context.Products.ToListAsync();
    }
    
    // GET: api/Products/{id} 
    [HttpGet("{id}")]
    public async Task<ActionResult<Product>> GetProduct(int id)
    {
        var product = await _context.Products.FindAsync(id);
        if (product == null)
        {
            return NotFound();
        }
        return product;
    }
    
    // GET: api/products/product/{slug}
    [HttpGet("product/{productSlug}")]
    public async Task<ActionResult<Product>> GetDetailProduct(string productSlug)
    {
        var product = await _context.Products.Where(p => p.Slug == productSlug).FirstOrDefaultAsync();
        if (product == null)
        {
            return NotFound();
        }
        return product;
    }

    [HttpDelete("[action]/{productId}")] /* api/products/deleteproduct/{productId} */
    public async Task<ActionResult<Product>> DeleteProduct(int productId)
    {
        var product = await _context.Products.FindAsync(productId);
        if (product == null)
        {
            return NotFound(); // Output 404 when product not found 
        }

        _context.Products.Remove(product); // Delete product
        await _context.SaveChangesAsync(); // Save changes in database 

        return NoContent(); // Return HTTP 204 if product delete successful
    }
}