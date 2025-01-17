using backend.Models;
using Microsoft.EntityFrameworkCore;
using backend.Models.DTOs;
namespace backend.Services;

public interface IShoppingCartService
{ 
    // Thêm sản phẩm vào giỏ hàng
    Task<Cart> AddToCartAsync(int userId, AddToCartRequest request);
    
    // Lấy danh sách sản phẩm trong giỏ hàng
    Task<IEnumerable<Cart>> GetCartItemsAsync(int userId);
}

public class ShoppingCartService : IShoppingCartService
{
    private readonly ApplicationDbContext _context;
    private readonly IProductService _productService;

    public ShoppingCartService(ApplicationDbContext context, IProductService productService)
    {
        _context = context;
        _productService = productService;
    }
    
    // Thêm sản phẩm vào giỏ hàng
    public async Task<Cart> AddToCartAsync(int userId, AddToCartRequest request)
    {
        // Kiểm tra sản phẩm có tồn tại;
        var product = await _productService.GetProductByIdAsync(request.ProductId);
        
        // if (product == null)
        // {
        //     throw new NotFoundException("Product not found");
        // }
        
        var existingItem = await _context.Carts.FirstOrDefaultAsync(x => x.UserId == userId && x.ProductId == product.Id);

        if (existingItem != null)
        {
            existingItem.Quantity += request.Quantity; // Cập nhật số lượng nếu sản phẩm đã tồn tại
            await _context.SaveChangesAsync();
            return existingItem;
        }
        
        // Thêm sản phẩm mới vào giỏ hàng
        var cartItem = new Cart
        {
           UserId = userId,
           ProductId = request.ProductId, 
           Quantity = request.Quantity
        };
        
        await _context.Carts.AddAsync(cartItem);
        await _context.SaveChangesAsync();
        
        return cartItem;
    }
    
    // Lấy giỏ hàng hiện tại
    // public ShoppingCart GetCart()
    // {
    // }
    public async Task<IEnumerable<Cart>> GetCartItemsAsync(int userId)
    {
        return await _context.Carts.Where(x => x.UserId == userId).ToListAsync();
    }
    
}

