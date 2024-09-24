using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PPManager.Data;
using PPManager.DTOs;
using PPManager.Models;

namespace PPManager.Controllers
{
   [Authorize]
    public class ProjectController : Controller
    {
        private readonly AppDbContext _appDbContext;
        private readonly UserManager<MyUser> _userManager;
        private readonly ILogger<ProjectController> _logger;

        public ProjectController(AppDbContext appDbContext, UserManager<MyUser> userManager, ILogger<ProjectController> logger) {
        
            _appDbContext = appDbContext;
            _userManager = userManager;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
            {
           
                if (!User.Identity.IsAuthenticated)
                {
                    return Unauthorized(); 
                }
                string? userId = _userManager.GetUserId(User);

            if (userId != null)
                {

                    var projects = await _appDbContext.Project
                        .Where(p => p.UserId == userId)
                        .ToListAsync();

                    return Ok(projects);
                }  
            return NotFound();

            }

        [HttpGet("/Project/Detail/{id}")]
        public async Task<IActionResult> Detail([FromRoute]int id)
            {

            var project = await _appDbContext.Project
                .Include(p => p.Tasks) 
                .Include(p => p.Notes)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (project != null)
                {
               
                return Ok(project);
            }

            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody]Project project)
            {
                
                
                string? userId = _userManager.GetUserId(User);

                if (userId == null)
                {
                    return Unauthorized();
                }

                project.UserId = userId;

                    await _appDbContext.Project.AddAsync(project);
                    await _appDbContext.SaveChangesAsync();
                    return Ok(project.UserId);
                

                
            }

        [HttpPatch("/Project/Edit/{projectId}")]
        public async Task<IActionResult> Edit([FromRoute] int projectId, [FromBody] EditDTO editDTO)
        {
            var project = await _appDbContext.Project.FindAsync(projectId);

            if (project == null)
            {
                Console.WriteLine("project not found");
                return NotFound();
            }

            if (!string.IsNullOrEmpty(editDTO.Title))
                {
                    project.Title = editDTO.Title;
                }
            if (!string.IsNullOrEmpty(editDTO.Desc))
            {
                project.Desc = editDTO.Desc;
            }

            await _appDbContext.SaveChangesAsync();
            return Ok();
            
        }

        [HttpDelete("/Project/Delete/{projectId}")]
        public async Task<IActionResult> Delete([FromRoute]int projectId)
            {
                var project = await _appDbContext.Project.FindAsync(projectId);

                if (project != null)
                {
                    _appDbContext.Project.Remove(project);
                    await _appDbContext.SaveChangesAsync();
                    return Ok();
                }

                Console.WriteLine("project not found");
                return NotFound();
        }
    }
}
