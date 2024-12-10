using System.ComponentModel.DataAnnotations;

namespace Dto.Commands;

public class CreateTodoItemCommand
{
    [Required]
    [StringLength(150)]
    public string Title { get; set; }
    [StringLength(300)]
    public string Description { get; set; }
}
