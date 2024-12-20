using backend.Models;
using backend.Models.DTOs;
using backend.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace backend.Controllers;

[ApiController] 
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    // private readonly IAuthService _authService;
    private readonly ApplicationDbContext _context;
    public AuthController(ApplicationDbContext context) { _context = context; }

    [HttpPost("Register")]
    public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var userExists = await _context.Users.FirstOrDefaultAsync(u => u.Username == registerDto.Username);
            
            if (userExists != null) { return BadRequest(new {Message = "Username already exists"}); }

            var newUser = new User
            {
                Username = registerDto.Username,
                Password = registerDto.Password,
            };
            
            var result = await _context.Users.AddAsync(newUser);
           
            await _context.SaveChangesAsync();

            return Ok();
        }
        catch (Exception ex)
        {
           return BadRequest(ex.Message); 
        }
    }
    // [HttpPost("Login")]
}