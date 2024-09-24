using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PPManager.Data;
using PPManager.DTOs;
using PPManager.Models;

namespace PPManager.Controllers
{
    public class TasksController : Controller
    {
        private readonly AppDbContext _appDbContext;
        public TasksController(AppDbContext appDbContext)
        {

            _appDbContext = appDbContext;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var tasks = await _appDbContext.Task.ToListAsync();
            return View(tasks);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody]Tasks task)
        {
            await _appDbContext.Task.AddAsync(task);
            await _appDbContext.SaveChangesAsync();
            return Ok();
        }

        [HttpPatch("/Tasks/Edit/{taskId}")]
        public async Task<IActionResult> Edit([FromRoute] int taskId, [FromBody] EditDTO editDTO)
        {
            var task = await _appDbContext.Task.FindAsync(taskId);

            if (task == null)
            {
                Console.WriteLine("task not found");
                return NotFound();
            }

            if (!string.IsNullOrEmpty(editDTO.Title))
            {
                task.Title = editDTO.Title;
            }
            if (!string.IsNullOrEmpty(editDTO.Desc))
            {
                task.Desc = editDTO.Desc;
            }
            if (editDTO.DateLimit != null)
            {
                task.DateLimit = editDTO.DateLimit;
            }
            if (editDTO.PriorityLevel != null)
            {
                task.PriorityLevel = (int) editDTO.PriorityLevel;
            }


            await _appDbContext.SaveChangesAsync();
            return Ok();

        }

        [HttpDelete("/Tasks/Delete/{taskId}")]
        public async Task<IActionResult> Delete([FromRoute] int taskId)
        {
            var task = await _appDbContext.Task.FindAsync(taskId);

            if (task != null)
            {
                _appDbContext.Task.Remove(task);
                await _appDbContext.SaveChangesAsync();
                return Ok();
            }

            Console.WriteLine("task not found");
            return NotFound();
        }
    }
}
