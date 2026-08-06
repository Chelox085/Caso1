using System.ComponentModel.DataAnnotations;
using Caso1.Models;

namespace Caso1.ViewModels;

public class BusquedaReservaViewModel
{
    [Required(ErrorMessage = "La fecha de inicio es requerida.")]
    [DataType(DataType.Date)]
    public DateTime FechaInicio { get; set; } = DateTime.Today;

    [Required(ErrorMessage = "La fecha de fin es requerida.")]
    [DataType(DataType.Date)]
    public DateTime FechaFin { get; set; } = DateTime.Today.AddDays(1);

    public List<Habitacion> HabitacionesDisponibles { get; set; } = new List<Habitacion>();
}