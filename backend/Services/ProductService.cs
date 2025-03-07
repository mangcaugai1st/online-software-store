using backend.Models;
using backend.Models.Entities;
using Microsoft.EntityFrameworkCore;
using backend.Models.DTOs;
using Microsoft.AspNetCore.Http.HttpResults;

namespace backend.Services;

public interface IProductService
{
    Task<IEnumerable<Product>> GetAllProductsAsync(string sortOrder = "asc"); 
    Task<Product?> GetProductByIdAsync(int id); 
    Task<Product?> GetProductDetailsByIdAsync(int id);
    Task<Product?> GetProductDetailsBySlugNameAsync(string slugName);
    Task<Product> AddProductAsync(ProductDto productDto);
    Task<Product> UpdateProductAsync(int productId, ProductDto productDto);
    Task<bool> DeleteProductAsync(int productId);
    Task<IEnumerable<Product>> SearchProductAsync(string query);
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

    // Lấy toàn bộ danh sách sản phẩm
    public async Task<IEnumerable<Product>> GetAllProductsAsync(string sortOrder)
    {
        
         var products = await _context.Products.ToListAsync();
         
         // Sắp xếp theo giá
         switch (sortOrder.ToLower())
         {
             case "desc":
                 products = products.OrderByDescending(x => x.Price).ToList();
                 break;
             case "asc":
                 products = products.OrderBy(x => x.Price).ToList();
                 break;
             default: 
                 products = products.ToList();
                 break;
         }

         return products;
    }

    // Lấy chi tiết sản phẩm theo id
    public async Task<Product?> GetProductByIdAsync(int id)
    {
        return await _context.Products.FindAsync(id);
    }

    // Lấy chi tiết sản phẩm theo id
    public async Task<Product?> GetProductDetailsByIdAsync(int id)
    {
        return await _context.Products.FirstOrDefaultAsync(product => product.Id == id);
    }

    // Lấy chi tiết sản phẩm theo slug
    public async Task<Product?> GetProductDetailsBySlugNameAsync(string slugName)
    {
        return await _context.Products.Where(product => product.Slug == slugName).Include(x => x.Category).FirstOrDefaultAsync();
    }

    // Thêm một sản phẩm mới.
    public async Task<Product> AddProductAsync(ProductDto productDto)
    {
        // Ánh xạ kiểu SubscriptionType từ ProductDto sang Product entity
        SubscriptionType subscriptionType = productDto.SubscriptionType;

        // Kiểm tra loại bản quyền phần mềm
        switch (subscriptionType)
        {
            case SubscriptionType.Perpetual: //  Vĩnh viễn
                if (productDto.YearlyRentalPrice.HasValue)
                {
                    throw new ArgumentException("Không được nhập giá thuê theo năm.");
                }
                break;
            
            case SubscriptionType.Rental:
                if (!productDto.YearlyRentalPrice.HasValue)
                {
                    throw new ArgumentException("Không được để trống giá thuê theo năm");
                }
                break;
            
            default:
                throw new ArgumentOutOfRangeException();
        }
            
        try
        {
             var product = new Product
             { 
                 Name = productDto.Name,
                 Price = productDto.Price,
                 SubscriptionType = subscriptionType,
                 // MonthlyRentalPrice = productDto.MonthlyRentalPrice,
                 YearlyRentalPrice = productDto.YearlyRentalPrice,
                 Discount = productDto.Discount,
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
                 
                 product.ImagePath = "/images/" + uniqueFileName; // Lưu đường dẫn tương đối vào database
                 
             }
             
             _context.Products.Add(product); // Thêm mới sản phẩm vào database 
             await _context.SaveChangesAsync(); 
             
             return product; // trả về thông tin sản phẩm đã được tạo thành công   
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        } 
    }

    public async Task<Product> UpdateProductAsync(int productId, ProductDto productDto)
    { 
        // Tìm kiếm sản phẩm theo id
        var product = await _context.Products.FindAsync(productId);

        if (product == null)
        {
            throw new KeyNotFoundException($"Không tìm thấy sản phẩm");
        }

        if (productDto.ImagePath != null)
        {
            //var imagePath = Path.Combine(_environment.WebRootPath, "images");

            // Khai báo folder nào sẽ là folder lưu trữ hình ảnh. 
            string uploadsFolder = Path.Combine(_environment.WebRootPath, "images");

            // Tạo tên file duy nhất để tránh trùng lặp. 
            string uniqueFileName = Guid.NewGuid().ToString() + "_" + productDto.ImagePath.FileName;

            // Đảm bảo folder lưu trữ hình ảnh tồn tại, nếu không sẽ tạo mới.
            if (!Directory.Exists(uploadsFolder))
            {
                Directory.CreateDirectory(uploadsFolder);
            }

            // Đường dẫn đầy đủ của file 
            string filePath = Path.Combine(uploadsFolder, uniqueFileName);

            // Lưu tệp hình ảnh vào thư mục
            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await productDto.ImagePath.CopyToAsync(fileStream);
            }
            // Sau khi file hình ảnh được lưu thì
            // Gán đường dẫn hình ảnh vào thuộc tính ImagePath của productDto

            // productDto.ImagePath = Path.Combine("images", uniqueFileName);
            productDto.ImagePath = new FormFile(null, 0, 0, "imagePath", uniqueFileName);
        }
        /*
         * Cập nhật đối tượng product:
         * - Gán giá trị từ productDto cho đối tượng product.
         * - Cập nhật các thuộc tính của product với giá trị từ productDto
         */
        product.Name = productDto.Name;
        product.Price = productDto.Price;
        product.SubscriptionType = productDto.SubscriptionType;
        // product.MonthlyRentalPrice = productDto.MonthlyRentalPrice;
        product.YearlyRentalPrice = productDto.YearlyRentalPrice;
        product.Discount = productDto.Discount; 
        // product.ImagePath = productDto.ImagePath;
        product.Description = productDto.Description;
        product.StockQuantity = productDto.StockQuantity;
        product.Slug = productDto.Slug;
        product.IsActive = productDto.IsActive;
        product.CategoryId = productDto.CategoryId;


        _context.Products.Update(product); // Cập nhật dữ liệu vào cơ sở dữ liệu
        
        await _context.SaveChangesAsync(); // Lưu những thay dooidr
        
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

    // Tìm kiếm sản phẩm theo tên hoặc mô tả
    public async Task<IEnumerable<Product>> SearchProductAsync(string query)
    {
        return await _context.Products
            .Where(x => x.Name.ToLower().Contains(query.ToLower()) 
                        || x.Description.ToLower().Contains(query.ToLower()))
            .ToListAsync();  // Lấy kết quả từ cơ sở dữ liệu bất đồng bộ
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
   