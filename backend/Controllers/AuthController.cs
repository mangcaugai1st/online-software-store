using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using backend.Models;
using backend.Models.DTOs;
using backend.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace backend.Controllers;

[ApiController] 
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly SymmetricSecurityKey _key;
    // private readonly IAuthService _authService;
    private readonly ApplicationDbContext _context;

    public AuthController(ApplicationDbContext context, IConfiguration config)
    {
        _context = context;
        _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["Jwt:Key"]));
    }

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
                Email = "",
                Phone = "",
                IsAdmin = false,
                IsActive = true
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
    public string CreateToken(User user)
    {
        var claims = new List<Claim>
        {
            new Claim(JwtRegisteredClaimNames.Name,user.Username),
            new Claim("role",user.IsAdmin.ToString()),
        };

        var creds = new SigningCredentials(_key,SecurityAlgorithms.HmacSha512Signature);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.Now.AddDays(1),
            SigningCredentials = creds
        };

        var tokenHandler = new JwtSecurityTokenHandler();
            
        var token = tokenHandler.CreateToken(tokenDescriptor);

        return tokenHandler.WriteToken(token);

    }
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState); 
            }
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Username == loginDto.Username);

            if (user == null)
            {
                return BadRequest(new {Message = "Username doesn't exists"});
            }

            var token = CreateToken(user);
            
            return Ok(new { Token = token });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { Message = "Đã có lỗi xảy ra:" + ex.Message }); 
        }
    }
}