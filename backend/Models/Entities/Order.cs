using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend.Models.Entities;

[Table("Orders")]
public class Order
{
    [Key]
    public int Id { get; set; }
    
    public int UserId { get; set; }
    [ForeignKey("UserId")]
    public User? User { get; set; }
    [DataType(DataType.DateTime)]
    public DateTime OrderDate { get; set; } = DateTime.UtcNow;
    
    [Required]
    [DataType(DataType.Currency)]
    [Column(TypeName = "decimal(18,2)")]
    public decimal TotalAmount { get; set; }
    
    [MaxLength(100)]
    public string? Status { get; set; }
    
    [MaxLength(100)]
    public string? PaymentStatus { get; set; }
   
    [DataType(DataType.DateTime)]
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    
    [DataType(DataType.DateTime)]
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    public ICollection<OrderDetail>? OrderDetails { get; } = new List<OrderDetail>();
}