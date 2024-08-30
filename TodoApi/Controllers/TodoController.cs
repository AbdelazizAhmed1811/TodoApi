using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TodoApi.Data;
using TodoApi.DTOs;
using TodoApi.Models;

namespace TodoApi.Controllers
{
    [Route("api/v1/todo")]
    [ApiController]
    public class TodoController : ControllerBase
    {
        private readonly AppDbContext _dbContext;

        public TodoController(AppDbContext context)
        {
            _dbContext = context;
        }

        [HttpGet(Name = "GetAllTodos")]
        public async Task<IActionResult> Get()
        {
            try
            {
                var todos = await _dbContext.TodoItems.ToListAsync();
                return Ok(todos);
            }
            catch (Exception ex)
            {
                // Log the exception here (if needed)
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var todo = await _dbContext.TodoItems.FirstOrDefaultAsync(x => x.Id == id);

            if (todo == null) { 
                return NotFound();
            }

            return Ok(todo);

        }


        [HttpPost(Name = "CreateTodo")]
        public async Task<IActionResult> Post([FromBody] CreateTodoItemDto todoDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var todo = new TodoItem
                {
                    Name = todoDto.Name,
                    IsCompleted = todoDto.IsCompleted
                };
                
                await _dbContext.TodoItems.AddAsync(todo);
                await _dbContext.SaveChangesAsync(); // Use the async version
                return CreatedAtRoute("GetAllTodos", new { id = todo.Id }, todo); // Return 201 Created
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}"); // More secure and detailed error
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] UpdateTodoItemDto todoDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var todo = await _dbContext.TodoItems.FirstOrDefaultAsync(x => x.Id ==id);
                if (todo == null) { return NotFound(); }

                todo.Name = todoDto.Name;
                todo.IsCompleted = todoDto.IsCompleted;
                await _dbContext.SaveChangesAsync();
                return Ok(todo);

            }
            catch (Exception e)
            {

                return StatusCode(500, e.Message);
            }
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id) {

            try
            {
                var todo = await _dbContext.TodoItems.FirstOrDefaultAsync(x => x.Id == id);
                if (todo == null)
                {
                    return NotFound();                    
                }
                _dbContext.TodoItems.Remove(todo);
                await _dbContext.SaveChangesAsync();
                return Ok();
            }
            catch (Exception e)
            {

                return StatusCode(500, e.Message);
            }
        }
    }
}
