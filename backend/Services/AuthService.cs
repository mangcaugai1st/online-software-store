using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using backend.Models;
using backend.Models.Entities;
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
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),   // Lấy id (nameid) của người dùng
            new Claim("user_id", user.Id.ToString()),                   // Lấy id của người dùng
            new Claim(JwtRegisteredClaimNames.Name, user.Username!),    // lấy username (name) từ token
            new Claim("role", user.IsAdmin.ToString()),                 // Lấy role của user từ token
        };
        
        /*
         * Lấy giá trị của
         * Jwt:Key, Jwt:Issuer, Jwt:Audience từ Configuration 
         */ 
        var issuer = _config["Jwt:Issuer"];
        var audience = _config["Jwt:Audience"];
        var secret_key = _config["Jwt:Key"];
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret_key));

        /*
         * Tạo chữ ký kỹ thuật số (digital signature)
         * để biểu thị cryptographic key và security algorithms (HMAC SHA256)
         */
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        
        /*
         * SecurityTokenDescriptor
         * là chỗ giữ tất cả các thuộc tính liên quan đến mã token đã phát hành
         */
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),   // Claims (Thông tin về người dùng)
            Expires = DateTime.Now.AddDays(1),      // Token hết hạn sau 1 ngày
            SigningCredentials = creds,             // Chữ ký kỹ thuật số cho việc xác thực
            Issuer = issuer,                        // Thêm thông tin về Issuer (ứng dụng phát hành)
            Audience = audience,                    // Thông tin về Audience (người dùng sẽ nhận token)
            IssuedAt = DateTime.UtcNow,             // Thời gian token được phát hành
        };
    
        // Tạo token từ tokenDescriptor
        var token = new JwtSecurityTokenHandler().CreateToken(tokenDescriptor);
    
        // Trả về token dưới dạng chuỗi JWT
        return new JwtSecurityTokenHandler().WriteToken(token);
    }
    
    // Service thực hiện chức năng đăng nhập
    public async Task<string> LoginAsync(LoginDto loginDto)
    {
        // Tìm người dùng trong csdl dựa trên tên đăng nhập
        var user = await _context.Users.FirstOrDefaultAsync(user => user.Username == loginDto.Username);

        // Kiểm tra nếu không tìm thấy người dùng nào trùng với tên đăng nhập đã cho
        if (user == null)
        {
            // Ném ra ngoại lệ nếu người dùng không tồn tại
            throw new Exception("Người dùng không xác định");
        }
        
        // Nếu người dùng tồn tại thì sẽ tạo ra mã token cho  người
        var token = GenerateToken(user);
        return token;
    }
    
    // Service dùng để thực hiện các logic của chức năng đăng ký tài khoản mới.
    public async Task<User> RegisterAsync(RegisterDto registerDto)
    {
        try
        {
            // Kiểm tra người dùng với tên đăng nhập đã tồn tại trong cơ sở dữ liệu chưa
            var userExists = await _context.Users.FirstOrDefaultAsync(user => user.Username == registerDto.Username);

            // Nếu tên đăng nhập tồn tại thì ném ra một ngoại lệ với thông báo lỗi
            if (userExists != null)
            {
                throw new ApplicationException($"Username {registerDto.Username} already exist");
            }
            // Nếu tên đăng nhập chưa tồn tại, tạo một đối tượng User mới.
            var newUser = new User
            {
                Username = registerDto.Username,    // gán tên đăng nhập từ RegisterDto
                Password = registerDto.Password,    // gán password từ RegisterDto
                Email = "",                         // Điền email mặc định
                Phone = "",                         // Điền số điện thoại mặc định 
                IsAdmin = false,                    // cài đặt quyền quản trị mặc định là false
                IsActive = true                     // cài đặt trạng thái người dùng là active
            };
            
            // Thêm đối tượng newUser vào csdl
            await _context.Users.AddAsync(newUser);
            
            // Lưu thay đổi vào cơ sở dữ liệu
            await _context.SaveChangesAsync();

            // Trả về đối tượng newUser đã được tạo và lưu vào csdl.
            return newUser;
        }
        catch (Exception e)
        {
            // Ghi thông tin lỗi vào console nếu có bất kỳ ngoại lệ nào xảy ra 
            Console.WriteLine(e);
            // Ném ra ngoại lệ các phần khác của hệ thống xử lý (nếu cần)
            throw;
        }
    }
}