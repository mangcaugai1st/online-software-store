using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend.Models;

[Table("OrderDetails")]
public class OrderDetail
{
    [Key]
    public int Id { get; set; }  
    
    public int OrderId { get; set; }
    [ForeignKey("OrderId")]
    public Order? Order { get; set; }
    
    public int ProductId { get; set; }
    [ForeignKey("ProductId")]
    public Product? Product { get; set; }
    
    public int Quantity { get; set; }
    
    public decimal UnitPrice { get; set; }
    
    public decimal Subtotal { get; set; }
}