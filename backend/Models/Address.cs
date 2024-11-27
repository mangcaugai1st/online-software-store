using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend.Models;

[Table("Addresses")]
public class Address
{
   [Key]
   public int Id { get; set; } 
   
   public int UserId { get; set; }
   [ForeignKey("UserId")]
   public User? User { get; set; }
   
   [Required]
   [MaxLength(1000)]
   public string? AddressLine { get; set; }

   public string? PostalCode { get; set; }
   
   public bool IsDefault { get; set; }
}