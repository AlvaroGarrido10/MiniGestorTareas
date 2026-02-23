using Microsoft.EntityFrameworkCore;
using MiniGestorTareas.Models;

namespace MiniGestorTareas.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public DbSet<Tarea> Tareas { get; set; }
}