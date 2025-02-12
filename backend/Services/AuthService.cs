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
    Task<User> RegisterAsync(RegisterDto registerDto);
}
public class AuthService : IAuthService
{
    private readonly SymmetricSecurityKey _key;
    private readonly ApplicationDbContext _context;
    private readonly IConfiguration _config;

    public AuthService(ApplicationDbContext context, IConfiguration config)
    {
        _context = context;
        _config = config;
        _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["Jwt:Key"]));
    }

    /*
     * Method GenerateToken nhận vào một đối tượng User
     * và trả về token dưới dạng string
     */
    private string GenerateToken(User user)
    {
        // Tạo danh sách các claims
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()), // Lấy id của người dùng
            new Claim("user_id", user.Id.ToString()), // Lấy id của người dùng
            new Claim(JwtRegisteredClaimNames.Name, user.Username!), // username vào token
            new Claim("role", user.IsAdmin.ToString()), // Thêm role của user vào token
        };
    
        // Lấy giá trị của Jwt:Key, Jwt:Issuer, Jwt:Audience từ Configuration
        var issuer = _config["Jwt:Issuer"];
        var audience = _config["Jwt:Audience"];
        var secret_key = _config["Jwt:Key"];
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret_key));

        // Tạo chữ ký cho token sử dụng HMAC SHA256
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
    
        // Tạo các descriptor cho token
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims), // Claims (Thông tin về người dùng)
            Expires = DateTime.Now.AddDays(1), // Token hết hạn sau 1 ngày
            SigningCredentials = creds, // Chữ ký xác thực
            Issuer = issuer, // Thêm thông tin về Issuer (ứng dụng phát hành)
            Audience = audience, // Thông tin về Audience (người dùng sẽ nhận token)
            IssuedAt = DateTime.UtcNow, // Thời gian token được cấp
        };
    
        // Tạo token từ tokenDescriptor
        var token = new JwtSecurityTokenHandler().CreateToken(tokenDescriptor);
    
        // Trả về token dưới dạng chuỗi JWT
        return new JwtSecurityTokenHandler().WriteToken(token);
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
    public async Task<User> RegisterAsync(RegisterDto registerDto)
    {
        try
        {
            var userExists = await _context.Users.FirstOrDefaultAsync(user => user.Username == registerDto.Username);

            if (userExists != null)
            {
                throw new ApplicationException($"Username {registerDto.Username} already exist");
            }
            var newUser = new User
            {
                Username = registerDto.Username,
                Password = registerDto.Password,
                Email = "",
                Phone = "",
                IsAdmin = false,
                IsActive = true
            };
            
            await _context.Users.AddAsync(newUser);
            await _context.SaveChangesAsync();

            return newUser;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}