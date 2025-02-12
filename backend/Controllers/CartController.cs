using System.Diagnostics;
using System.Security.Claims;
using backend.Models;
using backend.Models.DTOs;
using Microsoft.AspNetCore.Mvc;
using backend.Services;
using Microsoft.AspNetCore.Authorization;

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