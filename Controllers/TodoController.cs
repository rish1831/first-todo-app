using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using first_api_project.Models;

namespace first_api_project.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TodoController : ControllerBase
    {
        private readonly TodoContext _context;

        public TodoController(TodoContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<string>>> GetTodoItems()
        {
            var todoInfos = await _context.TodoInfo.ToListAsync();
            return Ok(todoInfos);
        }

        [HttpPost]
        public async Task<ActionResult<TodoItem>> AddTodoItem(TodoItem todoItem)
        {
            todoItem.Id = Guid.NewGuid(); 

            var todoInfos = await _context.TodoInfo.ToListAsync();
              var existingItem = await _context.TodoInfo.FindAsync(todoItem.Id);
    if (existingItem != null)
    {
        // If the item already exists, throw an exception
        throw new InvalidOperationException("A TodoItem with the same ID already exists.");
    }
 todoItem.TodoTimeAdded = DateTime.Now;

            _context.TodoInfo.Add(todoItem);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetTodoItems), new { id = todoItem.Id }, todoItem);
        }
            [HttpPut("{id}")]
public async Task<IActionResult> UpdateTodoItem(Guid id, [FromBody] TodoItem updatedTodoItem)
{
    var existingTodoItem = await _context.TodoInfo.FindAsync(id);

    if (existingTodoItem == null)
    {
        return NotFound();
    }

    existingTodoItem.Title = updatedTodoItem.Title;
existingTodoItem.TodoModifiedTime=DateTime.Now;

    _context.Entry(existingTodoItem).State = EntityState.Modified;

    try
    {
        await _context.SaveChangesAsync();
        return NoContent(); // Successful update
    }
    catch (DbUpdateConcurrencyException)
    {
        // Handle concurrency exceptions if needed
        return StatusCode(500, "An error occurred while updating the record.");
    }
}
    }


}

