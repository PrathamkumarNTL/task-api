using System.ComponentModel.DataAnnotations;

public class CreateTaskDto
{
    [Required]
    [MaxLength(100)]
    public string Title{get;set;} = string.Empty;
}