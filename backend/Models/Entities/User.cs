using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend.Models;

[Table("Users")]
public class User
{
    [Key]
    public int Id { get; set; } // User's id 
    
    [Required]
    [MaxLength(20)]
    public string? Username { get; set; } // username 
  
    [Required]
    [MaxLength(20)]
    public string? Password { get; set; }
   
    [Required] 
    [MaxLength(50)]
    public string? Email { get; set; }
    
    [Required]
    [MaxLength(15)]
    public string? Phone { get; set; }  
    
    public bool IsAdmin { get; set; } 
    
    public bool IsActive { get; set; }   
    
    [DataType(DataType.DateTime)] 
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    
    [DataType(DataType.DateTime)]
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    
    public ICollection<Order>? Orders { get; set; }
}