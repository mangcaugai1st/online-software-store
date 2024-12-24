using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend.Models;

[Table("PaymentDetails")]
public class PaymentDetail
{
   [Key]
   public int Id { get; set; } 
   
   public int  OrderId { get; set; }
   [ForeignKey("OrderId")]
   public Order? Order { get; set; }
   
   [MaxLength(50)]
   public string? PaymentMethod { get; set; }
   
   [MaxLength(50)]
   public string? TransactionId { get; set; }
   
   public decimal Amount { get; set; }
   
   [MaxLength(50)]
   public string? Status { get; set; }
   
   [DataType(DataType.DateTime)]
   public DateTime PaymentDate{ get; set; } = DateTime.UtcNow;
}