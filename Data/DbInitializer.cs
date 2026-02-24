using MiniGestorTareas.Models;

namespace MiniGestorTareas.Data;

public static class DbInitializer
{
    public static void Seed(AppDbContext context)
    {
        // Si ya hay datos, no metemos nada (evita duplicados cada vez que arranca)
        if (context.Tareas.Any())
            return;

        var hoy = DateTime.Today;

        var tareas = new List<Tarea>
        {
            new()
            {
                Title = "Preparar README y entrega",
                Description = "Completar README, revisar .gitignore y limpieza final",
                Priority = Priority.High,
                Status = Status.InProgress,
                CreatedAt = DateTime.UtcNow,
                DueDate = hoy.AddDays(2)
            },
            new()
            {
                Title = "Revisar validaciones y errores",
                Description = "Comprobar DataAnnotations + fecha objetivo no anterior a hoy",
                Priority = Priority.Medium,
                Status = Status.Pending,
                CreatedAt = DateTime.UtcNow,
                DueDate = hoy.AddDays(3)
            },
            new()
            {
                Title = "Hacer repaso para entrevista",
                Description = "Preparar explicación de EF Core, migraciones, MVC y decisiones",
                Priority = Priority.High,
                Status = Status.Pending,
                CreatedAt = DateTime.UtcNow,
                DueDate = hoy.AddDays(5)
            },
            new()
            {
                Title = "Ordenar el listado",
                Description = "Añadir ordenación por prioridad o fecha objetivo (extra opcional)",
                Priority = Priority.Low,
                Status = Status.Pending,
                CreatedAt = DateTime.UtcNow,
                DueDate = hoy.AddDays(7)
            },
            new()
            {
                Title = "Limpiar archivos de DB del repo",
                Description = "Asegurar que *.db, *.db-wal, *.db-shm están en .gitignore",
                Priority = Priority.Medium,
                Status = Status.Done,
                CreatedAt = DateTime.UtcNow,
                DueDate = null
            }
        };

        context.Tareas.AddRange(tareas);
        context.SaveChanges();
    }
}