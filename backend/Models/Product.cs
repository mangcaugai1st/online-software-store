using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend.Models;

[Table("Products")]
public class Product
{
    [Key]
    public int Id { get; set; } // Product id (PK)
    
    public int CategoryId { get; set; } // CategoryId (FK)
    [ForeignKey("CategoryId")]
    public Category? Category { get; set; } 
    
    [Required]
    [MaxLength(100)]
    public string? Name { get; set; }  // Product's name 
    
    [Required]
    public decimal Price { get; set; } // Product's price
    
    [Required]
    [MaxLength(500)]
    public string? ImagePath { get; set; } // Product's image
        
    [MaxLength(1000000)]
    public string? Description { get; set; } // Product's description
   
    [Required]
    public int StockQuantity { get; set; } // stock quantity 
    
    [Required]
    [MaxLength(250)] 
    public string? Slug { get; set; }
    
    public bool IsActive { get; set; }  // Product active

    [DataType(DataType.DateTime)]
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    
    [DataType(DataType.DateTime)]
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    
    public ICollection<OrderDetail>? OrderDetails { get; set; }
}