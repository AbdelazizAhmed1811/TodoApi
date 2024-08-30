using System.ComponentModel.DataAnnotations;

namespace TodoApi.DTOs
{
    public class UpdateTodoItemDto
    {
        [Required]
        [StringLength(100, MinimumLength = 3)]
        public string Name { get; set; }
        public bool IsCompleted { get; set; }
    }
}
