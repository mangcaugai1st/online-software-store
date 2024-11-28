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
    
    [MaxLength(1000000)]
    public string? Description { get; set; } // Product's description
   
    [Required]
    public int StockQuantity { get; set; } // stock quantity 
    
    public bool IsActive { get; set; }  // Product active

    [DataType(DataType.DateTime)]
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    
    [DataType(DataType.DateTime)]
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    
    public ICollection<ProductImage>? ProductImages { get; set; }
    public ICollection<OrderDetail>? OrderDetails { get; set; }
}