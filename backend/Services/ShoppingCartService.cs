using System.Globalization;
using backend.Models;
using Microsoft.EntityFrameworkCore;
using backend.Models.DTOs;
namespace backend.Services;

public interface IShoppingCartService
{ 
    Task<Cart> AddToCartAsync(int userId, AddToCartRequest request); // Thêm sản phẩm vào giỏ hàng 
    Task<IEnumerable<Cart>> GetCartItemsAsync(int userId);  // Lấy danh sách sản phẩm trong giỏ hàng
    Task<Cart> IncreaseQuantityAsync(int userId, int productId, int quantity);
    Task<Cart> DecreaseQuantityAsync(int userId, int productId, int quantity);
    Task<int> GetTotalQuantityAsync(int userId);
}

public class ShoppingCartService : IShoppingCartService
{
    private readonly ApplicationDbContext _context;
    // private readonly IProductService _productService;

    public ShoppingCartService(ApplicationDbContext context)
    {
        _context = context;
    }
    
    public ShoppingCartService(ApplicationDbContext context, IProductService productService)
    {
        _context = context;
    }
    
    // Thêm sản phẩm vào giỏ hàng
    public async Task<Cart> AddToCartAsync(int userId, AddToCartRequest request)
    {
        // var product = await _productService.GetProductByIdAsync(request.ProductId); // Kiểm tra sản phẩm có tồn tại hay không
        var product = await _context.Products.FirstOrDefaultAsync(x => x.Id == request.ProductId);  // Kiểm tra sản phẩm có tồn tại hay không

        if (product == null)
        {
            throw new Exception("Sản phẩm không tồn tại.");
        }
        var existingItem = await _context.Carts.FirstOrDefaultAsync(x => x.UserId == userId && x.ProductId == request.ProductId);

        if (existingItem != null)
        {
            existingItem.Quantity += request.Quantity; // Cập nhật số lượng nếu sản phẩm đã tồn tại
            await _context.SaveChangesAsync();
            return existingItem;
        }
        
        var cartItem = new Cart // Thêm sản phẩm mới vào giỏ hàng
        {
           UserId = userId,
           ProductId = request.ProductId, 
           Quantity = request.Quantity
        };
        
        await _context.Carts.AddAsync(cartItem);
        await _context.SaveChangesAsync();
        
        return cartItem; // return existingItem ?? cartItem;
    }
    
    // Lấy danh sách giỏ hàng
    public async Task<IEnumerable<Cart>> GetCartItemsAsync(int userId)
    {
        return await _context.Carts
                .Where(x => x.UserId == userId)
                .Include(x => x.Product)
                .ToListAsync();
    }

    // Tăng số lượng của một sản phẩm trong giỏ hàng
    public async Task<Cart> IncreaseQuantityAsync(int userId, int productId, int quantity)
    {
        if (quantity <= 0)
        {
            throw new ArgumentException("Số lượng phải lớn hơn 0");
        }
        
        // Kiểm tra sản phẩm có tồn tại hay không
        var product = await _context.Products.FirstOrDefaultAsync(x => x.Id == productId);

        if (product == null)
        {
            throw new CultureNotFoundException($"Không tìm thấy sản phẩm với id {productId}");
        }
        
        // Tìm giỏ hàng của người dùng 
        var cartItem = await _context.Carts
          .Include(x => x.Product)
          .FirstOrDefaultAsync(x => x.UserId == userId && x.ProductId == productId);
      
      // var cart = await _context.Carts
      //     .Include(x => x.Product)
      //     .FirstOrDefaultAsync(x => x.UserId == userId);

        if (cartItem == null)
        {
            cartItem = new Cart
            {
                UserId = userId,
                ProductId = productId,
                Quantity = quantity,
            };
            _context.Carts.Add(cartItem);
        }
        else
        {
            // Kiểm tra số lượng sau khi tăng có vượt quá tồn kho không
            // if (cartItem.Quantity + quantity > product.StockQuantity)
            // {
            //     throw new InvalidOperationException(
            //         $"Số lượng yêu cầu vượt quá số lượng tồn kho. Tồn kho hiện tại: {product.StockQuantity}");
            // }
            
            // Nếu sản phẩm đã tồn tại trong giỏ hàng, tăng số lượng
            cartItem.Quantity += quantity; 
        }

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            throw new Exception("Lỗi khi cập nhật giỏ hàng ", ex);
        }
        // Lưu thay đổi vào cơ sở dữ liệu
        
        return cartItem; // Trả về giỏ hàng đã được cập nhật
    }
    
    // Giảm số lượng của một sản phẩm trong giỏ hàng
    public async Task<Cart> DecreaseQuantityAsync(int userId, int productId, int quantity)
    {
         if (quantity <= 0)
         {
             throw new ArgumentException("Số lượng phải lớn hơn 0");
         }
         // Kiểm tra sản phẩm có tồn tại hay không
         var product = await _context.Products.FirstOrDefaultAsync(x => x.Id == productId);
 
         if (product == null)
         {
             throw new CultureNotFoundException($"Không tìm thấy sản phẩm với id {productId}");
         }
         
         // Tìm giỏ hàng của người dùng 
         var cartItem = await _context.Carts
           .Include(x => x.Product)
           .FirstOrDefaultAsync(x => x.UserId == userId && x.ProductId == productId);
         
         if (cartItem == null)
         {
             cartItem = new Cart
             {
                 UserId = userId,
                 ProductId = productId,
                 Quantity = quantity
             };
             _context.Carts.Add(cartItem);
         }
         else
         {
             cartItem.Quantity -= quantity; 
         }
 
         try
         {
             await _context.SaveChangesAsync();
         }
         catch (Exception ex)
         {
             throw new Exception("Lỗi khi cập nhật giỏ hàng ", ex);
         }
         // Lưu thay đổi vào cơ sở dữ liệu
         
         return cartItem; // Trả về giỏ hàng đã được cập nhật            
    }

    public async Task<int> GetTotalQuantityAsync(int userId)
    {
        // Tính tổng số lượng sản phẩm trong giỏ hàng
        var totalQuantity = _context.Carts.Sum(x => x.Quantity);

        // Trả về tổng số lượng
        return await Task.FromResult(totalQuantity);
    }
}