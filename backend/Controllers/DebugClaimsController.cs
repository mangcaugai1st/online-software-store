using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers;

[Route("api/[controller]")]
[ApiController]
public class DebugClaimsController : ControllerBase 
{
   // Lấy tất cả các claims từ JWT 
   [Authorize]
   [HttpGet("getclaims")]
   public IActionResult GetClaims()
   {
      // Truy xuất các claims từ HttpContext.User
      var userClaims = HttpContext.User.Claims.ToList();
      
      // Trả về các claims dưới dạng JSON
      return Ok(userClaims);
   }
   
   // Lấy một claim cụ thể theo tên ClaimType
   [HttpGet("getclaim/{claimType}")]
   public IActionResult GetClaim(Claim claimType)
   {
      var claimValue = HttpContext.User.Claims.FirstOrDefault(c => c.Type == claimType.Type).Value;
      
      if (string.IsNullOrEmpty(claimValue))
      {
         return NotFound($"Claim {claimType.Type} not found");
      }
      
      return Ok( new {ClaimType = claimType.Type, ClaimValue = claimValue});
   }

   [HttpGet("getJwtParam")]
   public IActionResult GetJwtParam(string jwtToken)
   {
      if (string.IsNullOrEmpty(jwtToken))
      {  
         return BadRequest("JWT token is empty");
      }

      try
      {
         var handler = new JwtSecurityTokenHandler(); // JwtSecurityTokenHandler được thiết kế cho việc tạo và xác thực JSON Web Tokens.
         var jsonToken = handler.ReadToken(jwtToken) as JwtSecurityToken; // method ReadToken() dùng để chuyển đổi một chuỗi thành 1 instance của JwtSecurityToken 

         if (jsonToken == null)
         {
            return BadRequest(jwtToken);
         }

         // Lấy tất cả claim từ token
         var claims = jsonToken?.Claims.Select(c => new { c.Type, c.Value }).ToList();

         // Hoặc lấy một claim cụ thể
         var specificClaim = jsonToken?.Claims.FirstOrDefault(c => c.Type == "your_claim_name")?.Value;

         return Ok(new
         {
            Claims = claims,
            SpecificClaim = specificClaim
         });
      }
      catch (Exception e)
      {
         return BadRequest();
      }
   }
}
/*
 * Chú thích:
 * - HttpContext.User.Claims: chứa danh sách tất cả các claims có trong JWT token của người dùng hiện tại.
 */