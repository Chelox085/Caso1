using System.ComponentModel.DataAnnotations;

namespace Caso1.Models;

public class Reservacion
{
    [Key]
    public int Id { get; set; }

    [Required(ErrorMessage = "El nombre es obligatorio.")]
    [MaxLength(150)]
    public string NombreDeLaPersona { get; set; } = string.Empty;

    [Required(ErrorMessage = "La identificación es obligatoria.")]
    [MaxLength(30)]
    public string Identificacion { get; set; } = string.Empty;

    [Required(ErrorMessage = "El teléfono es obligatorio.")]
    [MaxLength(10)]
    public string Telefono { get; set; } = string.Empty;

    [Required(ErrorMessage = "El correo es obligatorio.")]
    [MaxLength(50)]
    [EmailAddress(ErrorMessage = "Formato de correo inválido.")]
    public string Correo { get; set; } = string.Empty;

    [Required]
    public DateTime FechaNacimiento { get; set; }

    [Required(ErrorMessage = "La dirección es obligatoria.")]
    [MaxLength(200)]
    public string Direccion { get; set; } = string.Empty;

    [Required]
    public decimal MontoTotal { get; set; }

    [Required]
    public DateTime FechaInicioReserva { get; set; }

    [Required]
    public DateTime FechaFinReserva { get; set; }

    [Required]
    public DateTime FechaDeRegistro { get; set; }

    [Required]
    public int IdHabitacion { get; set; }

    [Required(ErrorMessage = "La cantidad de personas es obligatoria.")]
    [Range(1, int.MaxValue, ErrorMessage = "Debe ser al menos 1 persona.")]
    public int CantidadDePersonas { get; set; }

    public string? UserId { get; set; }
}