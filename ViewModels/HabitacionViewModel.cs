using Caso1.Models;

namespace Caso1.ViewModels;

public class HabitacionListItemViewModel
{
    public Habitacion Habitacion { get; set; } = null!;
    public string TipoDeHabitacionProsa { get; set; } = string.Empty;
    public string EstadoProsa { get; set; } = string.Empty;
}