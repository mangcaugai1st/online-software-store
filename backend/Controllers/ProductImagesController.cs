using backend.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace backend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductImagesController : ControllerBase
{
    private readonly MyDbContext _context;

    public ProductImagesController(MyDbContext context)
    {
        _context = context;
    }
    
    // GET: api/ProductImages
    [HttpGet]
    public async Task<ActionResult<IEnumerable<ProductImage>>> GetProductImages()
    {
        return await _context.ProductImages.ToListAsync();
    }
    
}