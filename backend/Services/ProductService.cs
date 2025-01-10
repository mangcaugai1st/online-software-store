using backend.Models;
using Microsoft.EntityFrameworkCore;
using backend.Models.DTOs;
using Microsoft.AspNetCore.Http.HttpResults;

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
    private readonly IWebHostEnvironment _environment;
    public ProductService(ApplicationDbContext context, IWebHostEnvironment environment)
    {
        _context = context; 
        _environment = environment;
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
        // return await _context.Products.FirstOrDefaultAsync(product => product.Id == id);
        return await _context.Products.FindAsync(id);
    }

    public async Task<Product?> GetProductDetailsBySlugNameAsync(string slugName)
    {
        return await _context.Products.Where(product => product.Slug == slugName).FirstOrDefaultAsync();
    }

    public async Task<Product> AddProductAsync(ProductDto productDto)
    {
        try
        {
             var product = new Product
             { 
                 Name = productDto.Name,
                 Price = productDto.Price,
                 // ImagePath = uniqueFileName,
                 // ImagePath = productDto.ImagePath,
                 Description = productDto.Description,
                 StockQuantity = productDto.StockQuantity,
                 Slug = productDto.Slug,
                 IsActive = productDto.IsActive,
                 CategoryId = productDto.CategoryId
             };
     
             if (productDto.ImagePath != null)
             {
                 // Khai báo folder nào sẽ là folder lưu trữ hình ảnh. 
                 string uploadsFolder = Path.Combine(_environment.WebRootPath, "images");
                 
                 // Tạo tên file duy nhất để tránh trùng lặp. 
                 string uniqueFileName = Guid.NewGuid().ToString() + "_" + productDto.ImagePath.FileName; 
                 
                 // Đường dẫn hình ảnh = uploadsFoler + uniqueFileName 
                 string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                 
                 // Tự động tạo ra folder để lưu trữ hình ảnh nếu nó không tồn tại 
                 if (!Directory.Exists(uploadsFolder))
                 {
                     Directory.CreateDirectory(uploadsFolder);
                 }
                 
                 // Lưu file
                 using (var fileStream = new FileStream(filePath, FileMode.Create))
                 {
                     await productDto.ImagePath.CopyToAsync(fileStream);
                 }
                 
                 // Lưu đường dẫn tương đối vào database
                 product.ImagePath = "/images/" + uniqueFileName;
                 
             }
             _context.Products.Add(product); // Thêm mới sản phẩm vào database 
             await _context.SaveChangesAsync(); 
             
             // trả về thông tin sản phẩm đã được tạo thành công
             return product;       
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        } 
    }

    public async Task<Product> UpdateProductAsync(int productId, ProductDto productDto)
    { 
        var product = await _context.Products.FindAsync(productId);

        if (product == null)
        {
            throw new KeyNotFoundException($"Không tìm thấy sản phẩm");
            
        }
        /*
         * Cập nhật đối tượng product:
         * - Gán giá trị từ productDto cho đối tượng product.
         * - Cập nhật các thuộc tính của product với giá trị từ productDto
         */
        product.Name = productDto.Name;
        product.Price = productDto.Price;
        // product.ImagePath = productDto.ImagePath;
        product.Description = productDto.Description;
        product.StockQuantity = productDto.StockQuantity;
        product.Slug = productDto.Slug;
        product.IsActive = productDto.IsActive;
        product.CategoryId = productDto.CategoryId;
        
        /*
         * Cập nhật dữ liệu vào cơ sở dữ liệu
         */
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

/*
 * FirstOrDefaultAsync
 * - Lấy phần tử đầu tiên thỏa mãn điều kiện hoặc trả về null nếu không tìm thấy phần tử nào.
 * Ví dụ: tìm một người dùng dựa trên tên hoặc email.
 * - var user = await dbContext.Users.FirstOrDefaultAsync(u => u.Email == "example@example.com");
 *
 * FindAsync
 * - Tìm kiếm một phần tử trong csdl dựa trên khóa chính.
 * - FindAsync sẽ truy cập csdl và sử dụng khóa chính để tìm phần tử.
 * Ví dụ:
 * var user = await dbContext.Users.FindAsync(userId)
 *
 * So sánh:
 * - Mục đích sử dụng:
 *  + FirstOrDefaultAsync: Tìm kiếm phần tử đầu tiên thỏa mãn điều kiện cho trước.
 *  + FindAsync: Tìm phần tử theo khóa chính (ID)
 * - Điều kiện:
 *  + FirstOrDefaultAsync: có thể sử dụng điều kiện tùy chỉnh (vd tìm theo email)
 *  + FindAsync: Tìm theo khóa chính của đối tượng
 * - Hiệu suất
 *  + FirstOrDefaultAsync: chậm hơn nếu điều kiện phức tạp hoặc không có chỉ mục
 *  + FindAsync: nhanh hơn
 * - Tham số
 *  + FirstOrDefaultAsync: chấp nhận biểu thức điều kiện LINQ
 *  + FindAsync: chỉ chấp nhận khóa chính 
 */
 
 
 
 
 
         // if (productDto.ImagePath != null)
         // {
         //     // Khai báo folder nào sẽ lưu hình ảnh
         //     string uploadsFoler = Path.Combine(_environment.WebRootPath, "images");
         //     // Tên hình ảnh
         //     string uniqueFileName = Guid.NewGuid().ToString() + "_" + productDto.ImagePath.FileName; 
         //     // Đường dẫn hình ảnh = uploadsFoler + uniqueFileName 
         //     string filePath = Path.Combine(uploadsFoler, uniqueFileName);
         //
         //     if (!Directory.Exists(uploadsFoler))
         //     {
         //         Directory.CreateDirectory(uploadsFoler);
         //     }
         //
         //     using (var fileStream = new FileStream(filePath, FileMode.Create))
         //     {
         //        await productDto.ImagePath.CopyToAsync(fileStream); 
         //     }
         //     
         //     var product = new Product
         //     { 
         //         Name = productDto.Name,
         //         Price = productDto.Price,
         //         // ImagePath = uniqueFileName,
         //         ImagePath = productDto.ImagePath,
         //         Description = productDto.Description,
         //         StockQuantity = productDto.StockQuantity,
         //         Slug = productDto.Slug,
         //         IsActive = productDto.IsActive,
         //         CategoryId = productDto.CategoryId
         //     };
         //     
         //     await _context.Products.AddAsync(product);
         //     await _context.SaveChangesAsync();            
         //     
         //     return product;
         // }
         //
         // return null;
   