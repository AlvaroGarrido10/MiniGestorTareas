using System.ComponentModel.DataAnnotations;

namespace MiniGestorTareas.Models;

public class Tarea
{
    public int Id { get; set; }

    [Required]
    [StringLength(80, MinimumLength = 3)]
    public string Title { get; set; } = string.Empty;

    [StringLength(200)]
    public string? Description { get; set; }

    [Required]
    public Priority Priority { get; set; }

    public Status Status { get; set; } = Status.Pending;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    [DataType(DataType.Date)]
    public DateTime? DueDate { get; set; }
}

public enum Priority
{
    Low,
    Medium,
    High
}

public enum Status
{
    Pending,
    InProgress,
    Done
}