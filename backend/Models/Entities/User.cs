using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend.Models.Entities;

[Table("Users")]
public class User
{
    [Key]
    public int Id { get; set; } // User's id 
    
    [Required]
    [MaxLength(20)]
    public string? Username { get; set; } // username 
    [StringLength(100)]
    public string? FullName { get; set; }
    [Required]
    [MaxLength(20)]
    public string? Password { get; set; }
    [Required] 
    [EmailAddress]
    [MaxLength(100)]
    public string? Email { get; set; }
    [Required]
    [MaxLength(15)]
    public string? Phone { get; set; }  
    [MaxLength(500)]
    public string? Address { get; set; }
    public bool IsAdmin { get; set; } 
    public bool IsActive { get; set; }   
    [DataType(DataType.DateTime)] 
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    [DataType(DataType.DateTime)]
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    public ICollection<Order>? Orders { get; set; }
}