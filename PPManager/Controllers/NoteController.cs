using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PPManager.Data;
using PPManager.DTOs;
using PPManager.Models;

namespace PPManager.Controllers
{
    public class NoteController : Controller
    {
        private readonly AppDbContext _appDbContext;
        public NoteController(AppDbContext appDbContext)
        {

            _appDbContext = appDbContext;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var notes = await _appDbContext.Note.ToListAsync();
            return View(notes);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody]Note note)
        {
            await _appDbContext.Note.AddAsync(note);
            await _appDbContext.SaveChangesAsync();
            return Ok();
        }

        [HttpPatch("/Note/Edit/{noteId}")]
        public async Task<IActionResult> Edit([FromRoute] int noteId, [FromBody] EditDTO editDTO)
        {
            var note = await _appDbContext.Note.FindAsync(noteId);

            if (note == null)
            {
                Console.WriteLine("note not found");
                return NotFound();
            }

            if (!string.IsNullOrEmpty(editDTO.Title))
            {
                note.Title = editDTO.Title;
            }
            if (!string.IsNullOrEmpty(editDTO.Desc))
            {
                note.Desc = editDTO.Desc;
            }

            await _appDbContext.SaveChangesAsync();
            return Ok();

        }

        [HttpDelete("/Note/Delete/{noteId}")]
        public async Task<IActionResult> Delete([FromRoute] int noteId)
        {
            var note = await _appDbContext.Note.FindAsync(noteId);

            if (note != null)
            {
                _appDbContext.Note.Remove(note);
                await _appDbContext.SaveChangesAsync();
                return Ok();
            }

            Console.WriteLine("note not found");
            return NotFound();
        }
    }
}
