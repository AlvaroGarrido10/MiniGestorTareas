using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MiniGestorTareas.Data;
using MiniGestorTareas.Models;

namespace MiniGestorTareas.Controllers
{
    public class TareasController : Controller
    {
        private readonly AppDbContext _context;

        public TareasController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Tareas
        public async Task<IActionResult> Index()
        {
            var tareas = await _context.Tareas.ToListAsync();
            return View(tareas);
        }

        // GET: Tareas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
                return NotFound();

            var tarea = await _context.Tareas.FirstOrDefaultAsync(t => t.Id == id);

            if (tarea == null)
                return NotFound();

            return View(tarea);
        }

        // GET: Tareas/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Tareas/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Title,Description,Priority,DueDate")] Tarea tarea)
        {
            if (!ModelState.IsValid)
                return View(tarea);

            tarea.CreatedAt = DateTime.UtcNow;
            tarea.Status = Status.Pending;

            _context.Add(tarea);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        // GET: Tareas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                return NotFound();

            var tarea = await _context.Tareas.FindAsync(id);

            if (tarea == null)
                return NotFound();

            return View(tarea);
        }

        // POST: Tareas/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Description,Priority,Status,DueDate")] Tarea tarea)
        {
            if (id != tarea.Id)
                return NotFound();

            if (!ModelState.IsValid)
                return View(tarea);

            var tareaDb = await _context.Tareas.FindAsync(id);

            if (tareaDb == null)
                return NotFound();

            // Actualizamos solo campos editables
            tareaDb.Title = tarea.Title;
            tareaDb.Description = tarea.Description;
            tareaDb.Priority = tarea.Priority;
            tareaDb.Status = tarea.Status;
            tareaDb.DueDate = tarea.DueDate;

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        // GET: Tareas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return NotFound();

            var tarea = await _context.Tareas.FirstOrDefaultAsync(t => t.Id == id);

            if (tarea == null)
                return NotFound();

            return View(tarea);
        }

        // POST: Tareas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tarea = await _context.Tareas.FindAsync(id);

            if (tarea != null)
            {
                _context.Tareas.Remove(tarea);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }
    }
}