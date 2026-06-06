using System.ComponentModel.DataAnnotations;

public class CreatedTaskDto
{
    [Required]
    public string Title {get; set;} = string.Empty;
}