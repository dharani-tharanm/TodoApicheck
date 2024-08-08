using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using TodoApi.Models;

namespace TodoApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TasksController : ControllerBase
    {
        private readonly List<TodoItem> _tasks;

        public TasksController(List<TodoItem> tasks)
        {
            _tasks = tasks;
        }

        [HttpGet]
        public ActionResult<IEnumerable<TodoItem>> GetTasks()
        {
            return Ok(_tasks);
        }

        [HttpPost]
        public ActionResult<TodoItem> AddTask([FromBody] TodoItem newTask)
        {
            var task = new TodoItem
            {
                Id = _tasks.Count > 0 ? _tasks.Max(t => t.Id) + 1 : 1,
                Description = newTask.Description
            };
            _tasks.Add(task);
            return CreatedAtAction(nameof(GetTasks), new { id = task.Id }, task);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteTask(int id)
        {
            var task = _tasks.FirstOrDefault(t => t.Id == id);
            if (task == null)
            {
                return NotFound();
            }

            _tasks.Remove(task);
            return NoContent();
        }
    }
}
