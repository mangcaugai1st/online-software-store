using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend.Models.Entities;

[Table("Categories")]
public class Category
{
   [Key] 
   public int Id { get; set; }
   [Required]
   [MaxLength(50)]
   public string? Name { get; set; }
   
   [Required]
   [MaxLength(250)] 
   public string? Slug { get; set; }
   
   [MaxLength(100)]
   public string? Description { get; set; }
}