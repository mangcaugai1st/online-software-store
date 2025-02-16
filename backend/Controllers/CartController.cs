using System.Diagnostics;
using System.Security.Claims;
using backend.Models;
using backend.Models.DTOs;
using Microsoft.AspNetCore.Mvc;
using backend.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;

namespace backend.Controllers;

[ApiController]
[Route("api/[controller]")]
// [Authorize]
public class CartController : ControllerBase
{
    private readonly IShoppingCartService _shoppingCartService;
    private readonly ILogger<CartController> _logger; // Định nghĩa _logger 
    private readonly IJwtClaimsService _jwtClaimsService;
    public CartController(IShoppingCartService shoppingCartService, ILogger<CartController> logger, IJwtClaimsService jwtClaimsService)
    {
        _shoppingCartService = shoppingCartService;
        _logger = logger;
        _jwtClaimsService = jwtClaimsService;
    }
    
    [HttpGet("user{userId}")]
    public async Task<ActionResult<IEnumerable<Cart>>> GetCart()
    {
        // token được lấy từ header của http request dưới dạng: Authorization: Bearer <jwt_token> 
        var jwtToken = Request.Headers["Authorization"].ToString().Replace("Bearer ", "");       
        
        if (string.IsNullOrEmpty(jwtToken))
        { 
            return Unauthorized("Jwt Token không tồn tại");
        }       
        
        var userIdFromClaim = _jwtClaimsService.GetUserIdByJwt(jwtToken);
        
        // Kiểm tra nếu không tìm thấy userId
        if (string.IsNullOrEmpty(userIdFromClaim))
        {
            return Unauthorized();
        }

        try
        {
            int userId = int.Parse(userIdFromClaim);
            var cartItem = await _shoppingCartService.GetCartItemsAsync(userId);

            return Ok(cartItem);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
    
    [HttpPost("add")]
    public async Task<IActionResult> AddToCart([FromBody] AddToCartRequest request)
    {
        if (request == null)
        {
            return BadRequest("Vui lòng nhập đúng dữ liệu.");
        }

        // token được lấy từ header của http request dưới dạng: Authorization: Bearer <jwt_token> 
        var jwtToken = Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
        
        if (string.IsNullOrEmpty(jwtToken))
        {
            return Unauthorized("Jwt Token không tồn tại");
        }
        
        try
        {
            var userIdFromClaim = _jwtClaimsService.GetUserIdByJwt(jwtToken);
            int userId = int.Parse(userIdFromClaim);
            var cartItem = await _shoppingCartService.AddToCartAsync(userId, request);

            if (cartItem == null)
            {
                return NotFound();
            }
            
            return Ok(cartItem);
        }
        
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }

    [HttpPut("items/{productId}/increase")]
    // PUT /api/cart/items/{productId}/increase?quantity=1
    public async Task<IActionResult> IncreaseQuantity(int productId, [FromQuery] int quantity)
    {
        if (productId <= 0 || quantity <= 0)
        {
            return BadRequest("Dữ liệu yêu cầu không hợp lệ");
        }
        
        // Lấy userId từ JWT token
        var jwtToken = Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
         
        if (string.IsNullOrEmpty(jwtToken))
        {
            return Unauthorized("Jwt Token không tồn tại");
        }       
         
        var userIdFromClaim = _jwtClaimsService.GetUserIdByJwt(jwtToken);
        int userId = int.Parse(userIdFromClaim);
         
        try 
        {
            // Gọi service để tăng số lượng sản phẩm
            var increaseQuantity = await _shoppingCartService.IncreaseQuantityAsync(userId, productId, quantity);

            return Ok(increaseQuantity);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(ex.Message); 
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (Exception)
        {
            return StatusCode(500, "Đã xảy ra lỗi khi cập nhật giỏ hàng");
        }
    }
    
    [HttpPut("items/{productId}/decrease")]
    public async Task<IActionResult> DecreaseQuantity(int productId, [FromQuery] int quantity)
    {
        if (productId <= 0 || quantity <= 0)
        {
            return BadRequest("Dữ liệu yêu cầu không hợp lệ");
        }

        // Lấy userId từ JWT token
        var jwtToken = Request.Headers["Authorization"].ToString().Replace("Bearer ", "");

        if (string.IsNullOrEmpty(jwtToken))
        {
            return Unauthorized("Jwt Token không tồn tại");
        }

        var userIdFromClaim = _jwtClaimsService.GetUserIdByJwt(jwtToken);
        int userId = int.Parse(userIdFromClaim);
        
        try
        {
            var decreaseQuantity = await _shoppingCartService.DecreaseQuantityAsync(userId, productId, quantity);
            
            return Ok(decreaseQuantity);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (Exception)
        {
            return StatusCode(500, "Đã xảy ra lỗi khi cập nhật giỏ hàng");
        }
    }

    [HttpGet("{userId}/total-quantity")]
    public async Task<ActionResult<int>> GetTotalQuantity(int userId)
    {
        var jwtToken = Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
        
        if (string.IsNullOrEmpty(jwtToken))
        {
            return Unauthorized("Token is missing"); 
        }
        
        var userIdFromClaim = _jwtClaimsService.GetUserIdByJwt(jwtToken);

        if (string.IsNullOrEmpty(userIdFromClaim))
        {
            return Unauthorized("Invalid token.");
        }
        
        // Kiểm tra xem user có quyền được truy cập giỏ hàng này không
        if (int.Parse(userIdFromClaim) != userId)
        {
            return Forbid();
        }

        try
        {
            var totalQuantity = await _shoppingCartService.GetTotalQuantityAsync(userId);
            
            return Ok(totalQuantity);
        }
        catch (Exception e)
        {
            return StatusCode(500, e.Message);
        }
    }

    private int GetUserIdFromClaims()
    {
        var claims = User.Claims;
        
        foreach (var claim in claims)
        {
            Console.WriteLine($"Claim Type: {claim.Type}, Value: {claim.Value}");
        }
        
        var userIdClaim = User.Claims.FirstOrDefault(claim => claim.Type == /* ClaimTypes.NameIdentifier */ "user_id")?.Value;
        // var userIdClaim = User.FindFirst("nameid")?.Value;
        if (string.IsNullOrEmpty(userIdClaim))
        {
            throw new UnauthorizedAccessException("User not authenticated " + userIdClaim);
        }
        
        return int.Parse(userIdClaim);
    }
    
    // Debug các claims trong jwt token
    private int DebugUserIdFromClaims()
    {
        var claims = User.Claims;

        foreach (var claim in claims)
        {
            Console.WriteLine($"Claim Type: {claim.Type}, Value: {claim.Value}");
        }
        
        var userIdClaim = User.Claims.FirstOrDefault(claim => claim.Type == ClaimTypes.NameIdentifier)?.Value;

        if (userIdClaim == null)
        {
            throw new UnauthorizedAccessException("User not authenticated " + userIdClaim);
        }
        
        return int.Parse(userIdClaim);
    }
}