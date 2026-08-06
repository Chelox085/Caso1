using Caso1.Data;
using Caso1.Models;
using Caso1.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Caso1.Controllers;

[Authorize(Roles = "Administrador")]
public class HabitacionesController : Controller
{
    private readonly Caso1Context _context;

    public HabitacionesController(Caso1Context context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var habitaciones = await _context.Habitaciones.ToListAsync();
        var viewModelList = new List<HabitacionListItemViewModel>();

        foreach (var hab in habitaciones)
        {
            string tipoProsa = hab.TipoDeHabitacion switch
            {
                1 => "Junior",
                2 => "Superior",
                3 => "Suite",
                _ => "Desconocido"
            };

            viewModelList.Add(new HabitacionListItemViewModel
            {
                Habitacion = hab,
                TipoDeHabitacionProsa = tipoProsa,
                EstadoProsa = hab.Estado ? "Activo" : "Inactivo"
            });
        }

        return View(viewModelList);
    }

    [HttpGet]
    public IActionResult Create()
    {
        return View(new Habitacion { Estado = true });
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Habitacion habitacion)
    {
        if (ModelState.IsValid)
        {
            habitacion.FechaDeRegistro = DateTime.Now;
            habitacion.FechaDeModificacion = null;

            _context.Add(habitacion);
            await _context.SaveChangesAsync();

            TempData["SuccessMessage"] = "Habitación registrada correctamente.";
            return RedirectToAction(nameof(Index));
        }
        return View(habitacion);
    }

    [HttpGet]
    public async Task<IActionResult> Edit(int id)
    {
        var habitacion = await _context.Habitaciones.FindAsync(id);
        if (habitacion == null)
        {
            return NotFound();
        }
        return View(habitacion);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, Habitacion habitacionModificada)
    {
        if (id != habitacionModificada.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            var habitacionDb = await _context.Habitaciones.FindAsync(id);

            if (habitacionDb == null)
            {
                return NotFound();
            }

            habitacionDb.CantidadDeHuespedesPermitidos = habitacionModificada.CantidadDeHuespedesPermitidos;
            habitacionDb.CantidadDeCamas = habitacionModificada.CantidadDeCamas;
            habitacionDb.EncargadoDeLimpieza = habitacionModificada.EncargadoDeLimpieza;
            habitacionDb.TipoDeHabitacion = habitacionModificada.TipoDeHabitacion;
            habitacionDb.CostoDeLimpieza = habitacionModificada.CostoDeLimpieza;
            habitacionDb.CostoDeReserva = habitacionModificada.CostoDeReserva;
            habitacionDb.Estado = habitacionModificada.Estado;

            habitacionDb.FechaDeModificacion = DateTime.Now;

            try
            {
                _context.Update(habitacionDb);
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "Habitación actualizada exitosamente.";
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!HabitacionExists(habitacionDb.Id)) return NotFound();
                else throw;
            }
            return RedirectToAction(nameof(Index));
        }
        return View(habitacionModificada);
    }

    private bool HabitacionExists(int id)
    {
        return _context.Habitaciones.Any(e => e.Id == id);
    }
}