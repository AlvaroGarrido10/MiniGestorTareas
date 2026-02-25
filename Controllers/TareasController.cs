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

        // LISTADO + BÚSQUEDA + FILTROS + ORDENACIÓN + PAGINACIÓN
        public async Task<IActionResult> Index(string? q, Status? status, Priority? priority, string? sort, int page = 1)
        {
            const int pageSize = 10;

            if (page < 1) page = 1;

            var query = _context.Tareas.AsQueryable();

            // Filtros
            if (!string.IsNullOrWhiteSpace(q))
                query = query.Where(t => t.Title.Contains(q));

            if (status.HasValue)
                query = query.Where(t => t.Status == status.Value);

            if (priority.HasValue)
                query = query.Where(t => t.Priority == priority.Value);

            // Ordenación
            sort = string.IsNullOrWhiteSpace(sort) ? "created_desc" : sort;

            query = sort switch
            {
                "priority_desc" => query.OrderByDescending(t => t.Priority),
                "priority_asc" => query.OrderBy(t => t.Priority),

                "duedate_asc" => query
                    .OrderBy(t => t.DueDate == null)
                    .ThenBy(t => t.DueDate),

                "duedate_desc" => query
                    .OrderBy(t => t.DueDate == null)
                    .ThenByDescending(t => t.DueDate),

                _ => query.OrderByDescending(t => t.CreatedAt)
            };

            // Paginación (antes del ToListAsync)
            var totalItems = await query.CountAsync();
            var totalPages = (int)Math.Ceiling(totalItems / (double)pageSize);

            if (totalPages == 0) totalPages = 1;
            if (page > totalPages) page = totalPages;

            var tareas = await query
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            // Mantener valores para la vista
            ViewData["q"] = q;
            ViewData["status"] = status;
            ViewData["priority"] = priority;
            ViewData["sort"] = sort;

            ViewData["page"] = page;
            ViewData["totalPages"] = totalPages;
            ViewData["totalItems"] = totalItems;

            return View(tareas);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var tarea = await _context.Tareas.FirstOrDefaultAsync(m => m.Id == id);
            if (tarea == null) return NotFound();

            return View(tarea);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Tarea tarea)
        {
            if (ModelState.IsValid)
            {
                tarea.CreatedAt = DateTime.UtcNow;
                _context.Add(tarea);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(tarea);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var tarea = await _context.Tareas.FindAsync(id);
            if (tarea == null) return NotFound();

            return View(tarea);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Tarea tarea)
        {
            if (id != tarea.Id) return NotFound();

            if (!ModelState.IsValid)
                return View(tarea);

            var tareaDb = await _context.Tareas.FindAsync(id);
            if (tareaDb == null) return NotFound();

            tareaDb.Title = tarea.Title;
            tareaDb.Description = tarea.Description;
            tareaDb.Priority = tarea.Priority;
            tareaDb.Status = tarea.Status;
            tareaDb.DueDate = tarea.DueDate;

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var tarea = await _context.Tareas.FirstOrDefaultAsync(m => m.Id == id);
            if (tarea == null) return NotFound();

            return View(tarea);
        }

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