using System.ComponentModel.DataAnnotations;

namespace MiniGestorTareas.Models;

public class Tarea : IValidatableObject
{
    public int Id { get; set; }

    [Display(Name = "Título")]
    [Required(ErrorMessage = "El título es obligatorio.")]
    [StringLength(80, MinimumLength = 3, ErrorMessage = "El título debe tener entre 3 y 80 caracteres.")]
    public string Title { get; set; } = string.Empty;

    [Display(Name = "Descripción")]
    [StringLength(200, ErrorMessage = "La descripción no puede superar 200 caracteres.")]
    public string? Description { get; set; }

    [Display(Name = "Prioridad")]
    [Required(ErrorMessage = "La prioridad es obligatoria.")]
    public Priority Priority { get; set; }

    // Siempre se crea en Pending
    [Display(Name = "Estado")]
    public Status Status { get; set; } = Status.Pending;

    [Display(Name = "Creada el")]
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    [Display(Name = "Fecha objetivo")]
    [DataType(DataType.Date)]
    public DateTime? DueDate { get; set; }

    // Validación personalizada
    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if (DueDate.HasValue && DueDate.Value.Date < DateTime.Today)
        {
            yield return new ValidationResult(
                "La fecha objetivo no puede ser anterior a hoy.",
                new[] { nameof(DueDate) }
            );
        }
    }
}

public enum Priority
{
    [Display(Name = "Baja")]
    Low = 0,

    [Display(Name = "Media")]
    Medium = 1,

    [Display(Name = "Alta")]
    High = 2
}

public enum Status
{
    [Display(Name = "Pendiente")]
    Pending = 0,

    [Display(Name = "En progreso")]
    InProgress = 1,

    [Display(Name = "Completada")]
    Done = 2
}