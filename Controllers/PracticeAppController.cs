using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using practice_backend.Data;
using react_practice_backend.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace practice_backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PracticeAppController : ControllerBase
    {
        private readonly AppDbContext _context;

        public PracticeAppController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<DBTask>>> GetTasks()
        {
            var tasks = await _context.Tasks.ToListAsync();
            return Ok(tasks);
        }

        [HttpPost("createTask")]
        public async Task<ActionResult<DBTask>> AddTask([FromBody] DBTask taskModel)
        {
            if (taskModel == null || string.IsNullOrWhiteSpace(taskModel.Task))
            {
                return BadRequest("Invalid task data.");
            }

            try
            {
                var dbTask = new DBTask
                {
                    Task = taskModel.Task.Trim(), 
                    Completed = false 
                };

                _context.Tasks.Add(dbTask);
                await _context.SaveChangesAsync();

                return CreatedAtAction(nameof(GetTasks), new { id = dbTask.Id }, dbTask);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error adding task: {ex.Message}");
            }
        }

        [HttpDelete("deleteTask/{id}")]
        public async Task<IActionResult> deleteTask(int id)
        {
            var task = await _context.Tasks.FindAsync(id);
            if (task == null)
            {
                return NotFound();
            }

            _context.Tasks.Remove(task);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpPut("updateTask/{id}")]
        public async Task<IActionResult> updateTask(int id, [FromBody] DBTask updatedTask)
        {
            if (updatedTask == null || id != updatedTask.Id)
            {
                return BadRequest("Invalid task data.");
            }

            var task = await _context.Tasks.FindAsync(id);
            if (task == null)
            {
                return NotFound();
            }

            task.Task = updatedTask.Task;
            task.Completed = updatedTask.Completed;

            try { await _context.SaveChangesAsync(); }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error updating task: {ex.Message}");
            }
            return NoContent();
        }

    }
}
