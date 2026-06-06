using System.ComponentModel.DataAnnotations;

public class UpdatedTaskDto
{
    [Required]
    public string Title {get; set;} = string.Empty;
    [Required]
    public bool IsCompleted {get; set;}
}