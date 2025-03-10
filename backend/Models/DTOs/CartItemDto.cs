namespace backend.Models.DTOs;

/*
 * DTO này dùng để truyền tải thông tin của mặt hàng trong giỏ hàng,
 */
public class CartItemDto
{
    public int Id { get; set; }
    public string? Name { get; set; } 
    public decimal Price { get; set; } 
    public string? ImagePath { get; set; }
    public int StockQuantity { get; set; } 
    public string? Slug { get; set; }
    public int CategoryId { get; set; }
}