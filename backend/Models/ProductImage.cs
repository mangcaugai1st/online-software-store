using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend.Models;

[Table("ProductImages")]
public class ProductImage
{
    [Key]
    public int Id { get; set; }
   
    public int ProductId { get; set; }
    [ForeignKey("ProductId")]
    public Product? Product { get; set; }
   
    [Required]
    [MaxLength(100)]
    public string? ImagePath { get; set; }
    
    public bool IsActive { get; set; }
    
    [DataType(DataType.DateTime)]
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}