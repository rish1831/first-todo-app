using Microsoft.AspNetCore.Mvc;
using first_api_project.Data;
using first_api_project.Models;

namespace first_api_project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoController : ControllerBase
    {
        private readonly TodoRepository _todoRepository;

        public TodoController()
        {
            _todoRepository = new TodoRepository(); // Initialize with the default constructor or pass connection string
        }

        [HttpGet]
        public IActionResult GetTodoItems()
        {
            var todoItems = _todoRepository.GetAllTodoItems();
            return Ok(todoItems);
        }

        [HttpPost]
        public IActionResult AddTodoItem(TodoItem todoItem)
        {
            _todoRepository.AddTodoItem(todoItem);
            return Ok();
        }

        [HttpPut("{id}")]
        public IActionResult UpdateTodoItem(Guid id, TodoItem todoItem)
        {
            todoItem.Id = id;
            _todoRepository.UpdateTodoItem(todoItem);
            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteTodoItem(Guid id)
        {
            _todoRepository.DeleteTodoItem(id);
            return Ok();
        }
    }
}
