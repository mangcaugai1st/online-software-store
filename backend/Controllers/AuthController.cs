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
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
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

            var user = await _authService.RegisterAsync(registerDto);
            return Ok(user);
        }
        catch (ApplicationException ex)
        {
            return BadRequest(new { Message = ex.Message });     
        }
        catch (Exception ex)
        {
           return BadRequest(ex.Message); 
        }
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
    {
        try
        { 
            var token = await _authService.LoginAsync(loginDto);
            
            return Ok(new { Token = token });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { Message = "Đã có lỗi xảy ra:" + ex.Message }); 
        }
    }
}