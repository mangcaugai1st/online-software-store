using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using backend.Models;
using backend.Models.DTOs;
using backend.Services;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

public interface IAuthService
{
    Task<string> LoginAsync(LoginDto loginDto);
    Task<string> RegisterAsync(RegisterDto registerDto);
}
public class AuthService : IAuthService
{
    private readonly SymmetricSecurityKey _key;
    private readonly ApplicationDbContext _context;

    public AuthService(ApplicationDbContext context, IConfiguration config)
    {
        _context = context;
        _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["Jwt:Key"]));
    }

    /*
     * Method GenerateToken nhận vào một đối tượng User
     * và trả về token dưới dạng string
     */
    public string GenerateToken(User user)
    {
        /*
         * Tạo danh sách các claims
         * - là những thông tin mã hóa trong token
         */
        var claims = new List<Claim>
        {
            new Claim(JwtRegisteredClaimNames.Name, user.Username), // Thêm username vào token
            new Claim("role", user.IsAdmin.ToString()), // Thêm role của user vào token
        };
        
        /*
         * Tạo chữ ký cho token sử dụng sử dụng thuật toán HMAC SHA256
         */
        var creds = new SigningCredentials(_key, SecurityAlgorithms.HmacSha256);
        // var creds = new SigningCredentials(_key,SecurityAlgorithms.HmacSha512Signature);
        
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
    
    public async Task<string> LoginAsync(LoginDto loginDto)
    {
        var user = await _context.Users.FirstOrDefaultAsync(user => user.Username == loginDto.Username);

        if (user == null)
        {
            throw new Exception("Người dùng không xác định");
        }
        
        var token = GenerateToken(user);
        return token;
    }
    
    // Đây là service dùng để thực hiện các logic của chức năng đăng ký tài khoản mới.
    public async Task<string> RegisterAsync(RegisterDto registerDto)
    {
        var userExits = await _context.Users.FirstOrDefaultAsync(user => user.Username == registerDto.Username);
        
    }
}