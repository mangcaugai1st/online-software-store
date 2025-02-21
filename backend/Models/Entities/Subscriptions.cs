using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend.Models.Entities;

[Table("Subscriptions")]
public class Subscriptions
{
    [Key] public int Id { get; set; }
   
    public int OrderId { get; set; }
    
    [ForeignKey("OrderId")] 
    public Order? Order { get; set; }
   
    [DataType(DataType.Date)]
    public DateTime StartDate { get; set; }
    
    [DataType(DataType.Date)]
    public DateTime EndDate { get; set; }
    
    public bool IsRenewal { get; set; }
    
    // public decimal Price { get; set; }
}