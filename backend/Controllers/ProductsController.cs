using backend.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using backend.Services;
using backend.Models.DTOs;
using Microsoft.AspNetCore.Http.HttpResults;

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
    
    [HttpGet("product/{productId}")]
    public async Task<ActionResult<Product>> GetProduct(int productId)
    {
        var product = await _productService.GetProductByIdAsync(productId);
        
        if (product == null)
        {
            return NotFound();
        }
        
        return Ok(product);
    }

    // [HttpGet("product/{id}")]
    // public async Task<ActionResult<Product>> GetProductDetailsById(int id)
    // {
    //     var productDetails = await _productService.GetProductByIdAsync(id);
    //
    //     if (productDetails == null)
    //     {
    //         return NotFound();
    //     }
    //     
    //     return Ok(productDetails);
    // }

    [HttpGet("details/{productSlug}")]
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

    [HttpPost]
    public async Task<ActionResult<Product>> AddNewProduct([FromBody] ProductDto productDto)
    {
        if (productDto == null)
        {
            return BadRequest("Yêu cầu dữ liệu trong productDto.");
        }

        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState); 
        }

        try
        {
            var newProduct = await _productService.AddProductAsync(productDto);

            return Ok();
        }
        catch (Exception e)
        {
            return StatusCode(500, new {message = "Có lỗi xảy ra khi thêm sản phẩm mới"});
        }
    }
    
    [HttpPut("product/{productId}")] 
    public async Task<ActionResult<Product>> UpdateProduct(int productId, [FromBody] ProductDto productDto)
    {
        if (productDto == null)
        {
            return BadRequest("Dữ liệu không được để trống");
        }
        try
        {
            var updatedProduct = await _productService.UpdateProductAsync(productId, productDto);
            
            return Ok(updatedProduct);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = ex.Message });
        }
    }
    [HttpDelete("product/{productId}")] 
    public async Task<IActionResult> DeleteProduct(int productId)
    {
        var result = await _productService.DeleteProductAsync(productId);
        if (!result)
        {
            return NotFound();
        }
        
        return NoContent(); 
    }
}
// IActionResult
// Là một interface
// Trả về nhiều loại kết quả khác từ một hành động trong controller.