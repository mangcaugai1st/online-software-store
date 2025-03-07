namespace backend.Models.DTOs;

public class ProductDto
{
    public int Id { get; set; }
    public string? Name { get; set; } 
    public decimal? Price { get; set; } 
    public SubscriptionType SubscriptionType { get; set; }
    // public decimal? MonthlyRentalPrice { get; set; }
    public decimal? YearlyRentalPrice { get; set; }
    public decimal Discount { get; set; }
    // public IFormFile? ImagePath { get; set; }
    public IFormFile? ImagePath { get; set; }
    public string? Description { get; set; } 
    public int StockQuantity { get; set; } 
    public string? Slug { get; set; }
    public bool IsActive { get; set; }  
    public int CategoryId { get; set; }
}