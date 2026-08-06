using Caso1.Models;

namespace Caso1.ViewModels;

public class FormularioReservaViewModel
{
    public Reservacion NuevaReserva { get; set; } = new Reservacion();

    public Habitacion HabitacionSeleccionada { get; set; } = null!;
    public int CantidadNoches { get; set; }
}