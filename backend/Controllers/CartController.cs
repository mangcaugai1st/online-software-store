using System.Security.Claims;
using backend.Models;
using backend.Models.DTOs;
using Microsoft.AspNetCore.Mvc;
using backend.Services;
using Microsoft.AspNetCore.Authorization;

namespace backend.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class CartController : ControllerBase
{
    private readonly IShoppingCartService _shoppingCartService;

    public CartController(IShoppingCartService shoppingCartService)
    {
        _shoppingCartService = shoppingCartService;
    }
    
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Cart>>> GetCart()
    {
        // Tìm claim có type là NameIdentifier 
        var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        
        // Kiểm tra nếu không tìm thấy userId
        if (string.IsNullOrEmpty(userIdClaim))
        {
            return Unauthorized();
        }

        try
        {
            int userId = int.Parse(userIdClaim);
            var cartItem = await _shoppingCartService.GetCartItemsAsync(userId);

            return Ok(cartItem);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
    
    [HttpPost("addToCart")]
    // [Authorize]
    public async Task<ActionResult<Cart>> AddToCart([FromBody] AddToCartRequest request )
    {
        // var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        // var userIdClaim = User.FindFirst("user_id")?.Value;
        // var claims = User.Claims.ToList();
        // var userIdClaim = User.Claims.FirstOrDefault(claim => claim.Type == ClaimTypes.NameIdentifier)?.Value;
        //
        // if (string.IsNullOrEmpty(userIdClaim))
        // {
        //     return Unauthorized("Không tìm thấy thông tin người dùng");
        // }
        //
        // try
        // {
        //     int userId = int.Parse(userIdClaim);
        //     var cartItem = await _shoppingCartService.AddToCartAsync(userId, request);
        //
        //     return Ok(new
        //     {
        //         message = "Thêm vào giỏ hàng thành công", 
        //         data = cartItem
        //     });
        // }
        // catch (Exception ex)
        // {
        //     return StatusCode(500, new
        //     {
        //         message = "Có lỗi xảy ra khi thêm sản phẩm vào giỏ hàng",
        //         error = ex.Message
        //     });
        // }
        try
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            int userId = int.Parse(userIdClaim!);
            var cartItem = await _shoppingCartService.AddToCartAsync(userId, request);
            return Ok(cartItem);
        }
        // catch (NotFoundException ex)
        // {
        //     return NotFound(ex.Message);
        // }
        catch (Exception ex)
        {
            return StatusCode(500, "An error occurred while processing your request");
        }
    }
}
