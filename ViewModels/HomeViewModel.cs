namespace Caso1.ViewModels;

public class HomeViewModel
{
    public int TotalHabitaciones { get; set; }
    public int TotalReservaciones { get; set; }
    public string ConnectionString { get; set; } = string.Empty;
    public List<Caso1.Models.Habitacion> HabitacionesDestacadas { get; set; } = new();
}