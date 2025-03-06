using backend.Models;
using backend.Models.DTOs;
using backend.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace backend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsersController : ControllerBase
{
   private readonly ApplicationDbContext _context;
   private readonly IJwtClaimsService _jwtClaimsService;

   public UsersController(ApplicationDbContext context, IJwtClaimsService jwtClaimsService)
   {
      _context = context;
      _jwtClaimsService = jwtClaimsService;
   }
   
   // API trả về toàn bộ thông tin của tất cả người dùng có trong hệ thống
   [HttpGet]
   public async Task<ActionResult<IEnumerable<UserDto>>> GetUsers()
   {
      return await _context.Users.Select(u => new UserDto
      {
         Id = u.Id, 
         Username = u.Username,
         Password = u.Password,
         Email = u.Email,
         Phone = u.Phone,
      }).ToListAsync();
   }
   
   [HttpGet("{userId}")]
   public async Task<ActionResult<User>> GetUser(int userId)
   {
      var user = await _context.Users.FindAsync(userId);

      if (user == null)
      {
         return NotFound();
      }
      
      return user;
   }
   
   [HttpPost]
   public async Task<ActionResult<User>> CreateUser(User user)
   {
      _context.Users.Add(user);
      await _context.SaveChangesAsync();
      
      return CreatedAtAction(nameof(GetUser), new { id = user.Id }, user);
   }
   
   [HttpPut("update")]
   public async Task<IActionResult> UpdateUserProfile([FromBody] UpdateUserProfileDto? updateUserProfileDTO)
   {
      // Token được lấy từ header của http request dưới dạng: Authorization: Bearer <jwt_token>
      var jwtToken = Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
     
      if (string.IsNullOrEmpty(jwtToken))
      {
         return Unauthorized("JWT Token is required"); 
      }

      try
      {
         // Trích xuất UserId từ Claims của Token
         var userIdFromClaims = _jwtClaimsService.GetUserIdByJwt(jwtToken);
         int userId = int.Parse(userIdFromClaims);
         
         if (userId <= 0 || updateUserProfileDTO == null)
         { 
            return BadRequest(); // Trả về lỗi 404
         }
         else
         {
            // Tìm kiếm người dùng theo userId
            var user = await _context.Users.FindAsync(userId);
            
            // Cập nhật thông tin người dùng
            if (user != null)
            {
               user.FullName = updateUserProfileDTO.FullName;
               user.Email = updateUserProfileDTO.Email;
               user.Phone = updateUserProfileDTO.Phone;
               user.Address = updateUserProfileDTO.Address;

               // Lưu thay đổi vào cơ sở dữ liệu
               _context.Users.Update(user);
               await _context.SaveChangesAsync();

               return Ok(user);
            }
         }
      }
      catch (Exception ex)
      { 
         return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
      }
      // _context.Entry(user).State = EntityState.Modified;
      return BadRequest();
   }
}