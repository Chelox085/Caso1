using System.Security.Claims;
using Caso1.Data;
using Caso1.Models;
using Caso1.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Caso1.Controllers;

[Authorize]
public class ReservacionController : Controller
{
    private readonly Caso1Context _context;

    public ReservacionController(Caso1Context context)
    {
        _context = context;
    }

    [Authorize(Roles = "Cliente")]
    [HttpGet]
    public IActionResult Index()
    {
        return View(new BusquedaReservaViewModel());
    }

    [Authorize(Roles = "Cliente")]
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Index(BusquedaReservaViewModel model)
    {
        if (model.FechaInicio >= model.FechaFin)
        {
            ModelState.AddModelError("", "La fecha de inicio debe ser anterior a la fecha de fin.");
            return View(model);
        }

        model.HabitacionesDisponibles = await _context.Habitaciones
            .Where(h => h.Estado == true && !_context.Reservaciones
                .Any(r => r.IdHabitacion == h.Id &&
                          ((model.FechaInicio >= r.FechaInicioReserva && model.FechaInicio < r.FechaFinReserva) ||
                           (model.FechaFin > r.FechaInicioReserva && model.FechaFin <= r.FechaFinReserva) ||
                           (model.FechaInicio <= r.FechaInicioReserva && model.FechaFin >= r.FechaFinReserva))))
            .ToListAsync();

        return View(model);
    }

    [Authorize(Roles = "Cliente")]
    [HttpGet]
    public async Task<IActionResult> BuscarReserva(int idReservacion)
    {
        var reservacion = await _context.Reservaciones.FindAsync(idReservacion);
        if (reservacion == null)
        {
            return NotFound();
        }

        var habitacion = await _context.Habitaciones.FindAsync(reservacion.IdHabitacion);
        if (habitacion == null) return NotFound();

        string tipoProsa = habitacion.TipoDeHabitacion switch
        {
            1 => "Junior",
            2 => "Superior",
            3 => "Suite",
            _ => "Desconocido"
        };

        var viewModel = new DetallesReservaViewModel
        {
            Reservacion = reservacion,
            Habitacion = habitacion,
            TipoDeHabitacionProsa = tipoProsa
        };

        return PartialView("DetailsPartial", viewModel);
    }

    [Authorize(Roles = "Cliente")]
    [HttpGet]
    public async Task<IActionResult> Create(int idHabitacion, DateTime fechaInicio, DateTime fechaFin)
    {
        var habitacion = await _context.Habitaciones.FindAsync(idHabitacion);
        if (habitacion == null) return NotFound();

        var niches = (fechaFin - fechaInicio).Days;

        var viewModel = new FormularioReservaViewModel
        {
            HabitacionSeleccionada = habitacion,
            CantidadNoches = niches,
            NuevaReserva = new Reservacion
            {
                IdHabitacion = idHabitacion,
                FechaInicioReserva = fechaInicio,
                FechaFinReserva = fechaFin,
                MontoTotal = (niches * habitacion.CostoDeReserva) + habitacion.CostoDeLimpieza
            }
        };

        return View(viewModel);
    }

    [Authorize(Roles = "Cliente")]
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(FormularioReservaViewModel viewModel)
    {
        ModelState.Remove("HabitacionSeleccionada");

        if (ModelState.IsValid)
        {
            viewModel.NuevaReserva.FechaDeRegistro = DateTime.Now;
            viewModel.NuevaReserva.UserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            _context.Add(viewModel.NuevaReserva);
            await _context.SaveChangesAsync();

            TempData["SuccessMessage"] = "Reserva completada con éxito. ¡Gracias por preferir Los Patitos!";
            return RedirectToAction(nameof(Details), new { idReservacion = viewModel.NuevaReserva.Id });
        }

        var habitacionDb = await _context.Habitaciones.FindAsync(viewModel.NuevaReserva.IdHabitacion);
        if (habitacionDb == null) return NotFound("La habitación seleccionada ya no existe.");

        viewModel.HabitacionSeleccionada = habitacionDb;
        return View(viewModel);
    }

    [Authorize(Roles = "Cliente")]
    [HttpGet]
    public async Task<IActionResult> Details(int idReservacion)
    {
        var reservacion = await _context.Reservaciones.FindAsync(idReservacion);
        if (reservacion == null)
        {
            TempData["ErrorMessage"] = "Estimado usuario, no se ha encontrado la reservación, favor realice una.";
            return RedirectToAction(nameof(Index));
        }

        var habitacion = await _context.Habitaciones.FindAsync(reservacion.IdHabitacion);
        if (habitacion == null) return NotFound();

        string tipoProsa = habitacion.TipoDeHabitacion switch
        {
            1 => "Junior",
            2 => "Superior",
            3 => "Suite",
            _ => "Desconocido"
        };

        var viewModel = new DetallesReservaViewModel
        {
            Reservacion = reservacion,
            Habitacion = habitacion,
            TipoDeHabitacionProsa = tipoProsa
        };

        return View(viewModel);
    }

    [Authorize(Roles = "Administrador")]
    [HttpGet]
    public async Task<IActionResult> History(int? idHabitacion)
    {
        IQueryable<Reservacion> query = _context.Reservaciones;

        if (idHabitacion.HasValue)
        {
            query = query.Where(r => r.IdHabitacion == idHabitacion.Value);
            var hab = await _context.Habitaciones.FindAsync(idHabitacion.Value);
            ViewData["FiltroActivo"] = $"Habitación: {hab?.NombreDeHabitacion ?? ""}";
        }
        else
        {
            ViewData["FiltroActivo"] = "Histórico General de Reservas";
        }

        var listaReservas = await query.OrderByDescending(r => r.FechaDeRegistro).ToListAsync();
        return View(listaReservas);
    }
}