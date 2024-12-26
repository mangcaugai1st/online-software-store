using backend.Models;
using backend.Models.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace backend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsersController : ControllerBase
{
   private readonly ApplicationDbContext _context;

   public UsersController(ApplicationDbContext context)
   {
      _context = context;
   }
   
   // GET: api/Users
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
  
   // GET: api/Users/{id}
   [HttpGet("{id}")]
   public async Task<ActionResult<User>> GetUser(int id)
   {
      var user = await _context.Users.FindAsync(id);

      if (user == null)
      {
         return NotFound();
      }
      
      return user;
   }
   
   // Post: api/Users
   [HttpPost]
   public async Task<ActionResult<User>> CreateUser(User user)
   {
      _context.Users.Add(user);
      await _context.SaveChangesAsync();
      
      return CreatedAtAction(nameof(GetUser), new { id = user.Id }, user);
   }
}