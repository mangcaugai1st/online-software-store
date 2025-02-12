using System.Text;
using Microsoft.EntityFrameworkCore;
using backend.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.IdentityModel.Tokens;
using backend.Services;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

// IConfiguration configuration = builder.Configuration;

builder.Services.AddControllers(); // Add services to the container.

/* Register the database context
 * DB context must be registered with the dependency injection (DI) container. The container provides the service to controllers.
 * */
builder.Services.AddDbContext<ApplicationDbContext>(opt=>
    opt.UseSqlServer(builder.Configuration.GetConnectionString("MyDB")));

/*
 * Đăng ký service
 */
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IShoppingCartService, ShoppingCartService>();
builder.Services.AddScoped<IJwtClaimsService, GetUserIdByJwtClaims>();

// builder.Services.AddAuthentication(options =>
// {
//     options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
//     options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
//     options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
// })
//     .AddJwtBearer(options =>
//     {
//         options.TokenValidationParameters = new TokenValidationParameters
//         {
//             ValidateIssuer = true,
//             ValidateAudience = true,
//             ValidateLifetime = true,
//             ValidateIssuerSigningKey = true,
//             ValidIssuer = builder.Configuration["Jwt:Issuer"],
//             // ValidAudience = builder.Configuration["Jwt:Issuer"],
//             IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
//         };
//     });

builder.Services.AddAuthentication(options => 
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme; 
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(options =>
    { 
        options.SaveToken = true;
        options.RequireHttpsMetadata = false;
        options.TokenValidationParameters = new TokenValidationParameters 
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidIssuer = "https://localhost:5252", // Đảm bảo khớp với "iss" trong token
            ValidAudience = "myapp_api", // Đảm bảo khớp với "aud" trong token
            ClockSkew = TimeSpan.Zero, // Để token hết hạn chính xác
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("your_random_secure_key_min_32_chars_here")) // Đảm bảo "IssuerSigningKey" trùng với key đã dùng khi tạo token
        };
    });

// Cấu hình Authorization 
// builder.Services.AddAuthorization(Options =>
// {
//     Options.AddPolicy("AdminPolicy", policy => policy.RequireRole("Admin"));
//     Options.AddPolicy("UserPolicy", policy => policy.RequireRole("User"));
// });

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseStaticFiles(); // Cho phép truy xuất các file trong wwwroot

// Enable CORS
app.UseCors(policy => policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
// app.UseCors("AllowAll");

app.UseHttpsRedirection();

app.UseRouting();
/* 
 * Authentication -> Authorization
 */
app.UseAuthentication(); // Xác thực token

app.UseAuthorization();

app.MapControllers();

app.Run();