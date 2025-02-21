using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend.Models.Entities;

[Table("Product_Subscriptions")]
public class ProductSubscriptions
{
    [Key] public int Id { get; set; }
    
    public int ProductId { get; set; }
    
    [ForeignKey("ProductId")] public Product? Product { get; set; }
    
    [Required] public int DurationMonths { get; set; }
    
    public bool IsPerpetual { get; set; }

    [Required] public decimal Price { get; set; }

    [DataType(DataType.DateTime)] public DateTime CreatedAt { get; set; }

    [DataType(DataType.DateTime)] public DateTime UpdatedAt { get; set; }
}