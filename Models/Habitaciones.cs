using System.ComponentModel.DataAnnotations;

namespace Caso1.Models;

public class Habitacion
{
    [Key]
    public int Id { get; set; }

    [Required(ErrorMessage = "El código es obligatorio.")]
    [MaxLength(7)]
    public string CodigoDeHabitacion { get; set; } = string.Empty;

    [Required(ErrorMessage = "El nombre es obligatorio.")]
    [MaxLength(30)]
    public string NombreDeHabitacion { get; set; } = string.Empty;

    [Required]
    [Range(1, int.MaxValue, ErrorMessage = "La cantidad de huéspedes debe ser mayor a 0.")]
    public int CantidadDeHuespedesPermitidos { get; set; }

    [Required]
    public int CantidadDeCamas { get; set; }

    [Required]
    public int CantidadDeBanos { get; set; }

    [Required]
    [MaxLength(10)]
    public string Ubicacion { get; set; } = string.Empty;

    [Required]
    [MaxLength(100)]
    public string EncargadoDeLimpieza { get; set; } = string.Empty;

    [Required]
    public int TipoDeHabitacion { get; set; }

    [Required]
    [Range(0.01, double.MaxValue, ErrorMessage = "El costo de limpieza debe ser mayor a 0.")]
    public decimal CostoDeLimpieza { get; set; }

    [Required]
    [Range(0.01, double.MaxValue, ErrorMessage = "El costo de reserva debe ser mayor a 0.")]
    public decimal CostoDeReserva { get; set; }

    [Required]
    public DateTime FechaDeRegistro { get; set; }

    public DateTime? FechaDeModificacion { get; set; }

    public bool Estado { get; set; }
}