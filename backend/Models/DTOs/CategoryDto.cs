namespace backend.Models.DTOs;

public class CategoryDto
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Slug{ get; set; }
    public string? Description { get; set; }
}

public class CategoryCreateDto
{
    public string? Name { get; set; }
    public string? Description { get; set; }
}

public class CategoryUpdateDto
{
    public string? Name { get; set; }
    public string? Description { get; set; }    
}