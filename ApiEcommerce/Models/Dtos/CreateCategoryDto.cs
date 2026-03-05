using System.ComponentModel.DataAnnotations;

public class CreateCategoryDto
{
    [Required(ErrorMessage = "Name is required")]
    [MaxLength(50, ErrorMessage = "Name can not be more than 50 characters length")]
    [MinLength(3, ErrorMessage = "Name can not be less than 3 characters length")]
    public string Name { get; set; } = string.Empty;
}