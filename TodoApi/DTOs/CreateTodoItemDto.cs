using System.ComponentModel.DataAnnotations;

namespace TodoApi.DTOs
{
    public class CreateTodoItemDto    {

        [Required, MaxLength(50), MinLength(3)]
        public string Name { get; set; }
        public bool IsCompleted { get; set; }
    }
}
