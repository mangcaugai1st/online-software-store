using backend.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using backend.Services;

namespace backend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductsController : ControllerBase
{
    private readonly ApplicationDbContext _context;
    private readonly IProductService _productService;
    
    public ProductsController(IProductService productService, ApplicationDbContext context)
    {
        _productService = productService ?? throw new ArgumentNullException(nameof(productService));
        _context = context;
    }
    
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
    {
        var products = await _productService.GetAllProductsAsync();
        
        return Ok(products);
    }
    
    [HttpGet("{id}")]
    public async Task<ActionResult<Product>> GetProduct(int id)
    {
        var product = await _productService.GetProductByIdAsync(id);
        if (product == null)
        {
            return NotFound();
        }
        return Ok(product);
    }

    [HttpGet("product/{id}")]
    public async Task<ActionResult<Product>> GetProductDetailsById(int id)
    {
        var productDetails = await _productService.GetProductByIdAsync(id);

        if (productDetails == null)
        {
            return NotFound();
        }
        
        return Ok(productDetails);
    }

    [HttpGet("product/{productSlug}")]
    // public async Task<ActionResult<Product>> GetDetailProduct(string productSlug)
    // {
    //     var product = await _context.Products.Where(p => p.Slug == productSlug).FirstOrDefaultAsync();
    //     if (product == null)
    //     {
    //         return NotFound();
    //     }
    //     return product;
    // }
    public async Task<ActionResult<Product>> GetProductDetailsByProductSlug(string productSlug)
    {
        var productDetails = await _productService.GetProductDetailsBySlugNameAsync(productSlug);

        if (productDetails == null)
        {
            return NotFound();
        }
        return productDetails;
    }
    
    [HttpDelete("[action]/{productId}")] 
    public async Task<ActionResult<Product>> DeleteProduct(int productId)
    {
        var product = await _context.Products.FindAsync(productId);
        if (product == null)
        {
            return NotFound();
        }

        _context.Products.Remove(product); // Delete product
        await _context.SaveChangesAsync(); // Save changes in database 

        return NoContent(); // Return HTTP 204 if product delete successful
    }
}