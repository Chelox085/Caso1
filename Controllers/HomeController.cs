using Caso1.Data;
using Caso1.Models;
using Caso1.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Caso1.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly Caso1Context _context;

    public HomeController(ILogger<HomeController> logger, Caso1Context context)
    {
        _logger = logger;
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        var model = new HomeViewModel
        {
            TotalHabitaciones = await _context.Habitaciones.CountAsync(),
            TotalReservaciones = await _context.Reservaciones.CountAsync(),
            HabitacionesDestacadas = await _context.Habitaciones
                .Where(h => h.Estado)
                .OrderBy(h => h.TipoDeHabitacion)
                .ToListAsync()
        };

        return View(model);
    }
}