using backend.Models;
using Microsoft.EntityFrameworkCore;
using backend.Models.DTOs;

namespace backend.Services;

public interface IProductService
{
    Task<IEnumerable<Product>> GetAllProductsAsync(); 
    Task<Product?> GetProductByIdAsync(int id); 
    Task<Product?> GetProductDetailsByIdAsync(int id);
    Task<Product?> GetProductDetailsBySlugNameAsync(string slugName);
    Task<Product> AddProductAsync(ProductDto productDto);
    Task<Product> UpdateProductAsync(int productId, ProductDto productDto);
    Task<bool> DeleteProductAsync(int productId); 
}
public class ProductService : IProductService
{
    private readonly ApplicationDbContext _context;

    public ProductService(ApplicationDbContext context)
    {
        _context = context; 
    }

    public async Task<IEnumerable<Product>> GetAllProductsAsync()
    {
        return await _context.Products.ToListAsync();
    }

    public async Task<Product?> GetProductByIdAsync(int id)
    {
        return await _context.Products.FindAsync(id);
    }

    public async Task<Product?> GetProductDetailsByIdAsync(int id)
    {
        return await _context.Products.FirstOrDefaultAsync(product => product.Id == id);
    }

    public async Task<Product?> GetProductDetailsBySlugNameAsync(string slugName)
    {
        return await _context.Products.Where(product => product.Slug == slugName).FirstOrDefaultAsync();
    }

    public async Task<Product> AddProductAsync(ProductDto productDto)
    {
        var product = new Product
        {
            Name = productDto.Name,
            Price = productDto.Price,
            ImagePath = productDto.ImagePath,
            Description = productDto.Description,
            StockQuantity = productDto.StockQuantity,
            Slug = productDto.Slug,
            IsActive = productDto.IsActive,
            CategoryId = productDto.CategoryId
        };
        await _context.Products.AddAsync(product);
        await _context.SaveChangesAsync(); 
        
        return product;
    }

    public async Task<Product> UpdateProductAsync(int productId, ProductDto productDto)
    { 
        var product = await _context.Products.FindAsync(productId);

        if (product == null)
        {
            throw new KeyNotFoundException($"Không tìm thấy sản phẩm");
            
        }

        productDto.Name = productDto.Name;
        productDto.Price = productDto.Price;
        productDto.ImagePath = productDto.ImagePath;
        productDto.Description = productDto.Description;
        productDto.StockQuantity = productDto.StockQuantity;
        productDto.Slug = productDto.Slug;
        productDto.IsActive = productDto.IsActive;
        productDto.CategoryId = productDto.CategoryId;
        
        _context.Products.Update(product);
        await _context.SaveChangesAsync();
        
        return product;
    }

    public async Task<bool> DeleteProductAsync(int productId)
    {
        var product = await _context.Products.FindAsync(productId);

        if (product == null)
        {
            return false;
        }
        
        _context.Products.Remove(product);
        await _context.SaveChangesAsync();

        return true;
    }
}
// IActionResult
// Là một interface
// Trả về nhiều loại kết quả khác từ một hành động trong controller.
