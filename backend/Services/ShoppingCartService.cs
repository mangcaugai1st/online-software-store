using backend.Models;
using Microsoft.EntityFrameworkCore;
using backend.Models.DTOs;
namespace backend.Services;

public interface IShoppingCartService
{ 
    Task<Cart> AddToCartAsync(int userId, AddToCartRequest request); // Thêm sản phẩm vào giỏ hàng 
    Task<IEnumerable<Cart>> GetCartItemsAsync(int userId);  // Lấy danh sách sản phẩm trong giỏ hàng
}

public class ShoppingCartService : IShoppingCartService
{
    private readonly ApplicationDbContext _context;
    // private readonly IProductService _productService;

    // public ShoppingCartService(ApplicationDbContext context, IProductService productService)
    // {
    //     _context = context;
    //     _productService = productService;
    // }
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
}