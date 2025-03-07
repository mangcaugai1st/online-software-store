using backend.Models;
using backend.Models.Entities;
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
    public async Task<ActionResult<IEnumerable<Product>>> GetProducts([FromQuery] string sortOrder = "asc")
    {
        // Gọi service để lấy sản phẩm đã sắp xếp 
        var products = await _productService.GetAllProductsAsync(sortOrder);

        //  Trả kết quả cho client 
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

    [HttpGet("detail/{productSlug}")]
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
    public async Task<ActionResult<Product>> AddNewProduct([FromForm] ProductDto productDto)
    {
        // if (productDto == null)
        // {
        //     return BadRequest("Yêu cầu dữ liệu trong productDto.");
        // }

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
            return StatusCode(500, new { message = "Có lỗi xảy ra khi thêm sản phẩm mới" });
        }
    }

    [HttpPut("update_product/{productId}")] // CẬP NHẬT THÔNG TIN CỦA MỘT SẢN PHẨM
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

    [HttpDelete("delete_product/{productId}")]
    public async Task<IActionResult> DeleteProduct(int productId)
    {
        var result = await _productService.DeleteProductAsync(productId);
        
        if (!result)
        {
            return NotFound();
        }

        return NoContent();
    }

    [HttpGet("search")]
    public async Task<ActionResult<IEnumerable<Product>>> SearchProducts([FromQuery] string query)
    {
        if (string.IsNullOrWhiteSpace(query))
        {
            return BadRequest("Query parameter is required.");
        }

        var products = await _productService.SearchProductAsync(query);

        if (products == null || !products.Any())
        {
            return NotFound("No products found matching the search query.");
        }
        
        return Ok(products);
    }
}
// IActionResult
// Là một interface
// Trả về nhiều loại kết quả khác từ một hành động trong controller.