using backend.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace backend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsersController : ControllerBase
{
   private readonly MyDbContext _context;

   public UsersController(MyDbContext context)
   {
      _context = context;
   }
   
   // GET: api/Users
   [HttpGet]
   public async Task<ActionResult<IEnumerable<User>>> GetUsers()
   {
      return await _context.Users.ToListAsync();
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