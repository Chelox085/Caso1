using Caso1.Models;

namespace Caso1.ViewModels;

public class DetallesReservaViewModel
{
    public Reservacion Reservacion { get; set; } = null!;
    public Habitacion Habitacion { get; set; } = null!;
    public string TipoDeHabitacionProsa { get; set; } = string.Empty;
}