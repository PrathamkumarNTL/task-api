using System.ComponentModel.DataAnnotations;

public class TaskItem
{
    [Key]
    public int Id{get;set;}

    [Required]
    [MaxLength(100)]
    public string Title{get;set;} = string.Empty;
    public bool IsCompleted{get;set;}
}