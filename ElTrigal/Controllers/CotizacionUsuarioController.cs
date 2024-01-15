using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ElTrigal.Models;

namespace ElTrigal.Controllers
{
    public class CotizacionUsuarioController : Controller
    {
        private readonly ElTrigalContext _context;

        public CotizacionUsuarioController(ElTrigalContext context)
        {
            _context = context;
        }

        // GET: Cotizacion
        public async Task<IActionResult> Index()
        {
            var elTrigalContext = _context.Cotizaciones.Include(c => c.Cliente);
            var cotizacionesOrdenadas = await elTrigalContext.OrderByDescending(c => c.Fecha).ToListAsync();
            return View(cotizacionesOrdenadas);
        }

        // GET: Cotizacion/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cotizacion = await _context.Cotizaciones
                .Include(c => c.Cliente)
                .Include(c => c.Detalles)
                    .ThenInclude(d => d.Producto)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (cotizacion == null)
            {
                return NotFound();
            }

            return View(cotizacion);
        }

        // GET: Cotizacion/Create
        public IActionResult Create()
        {
            ViewData["ClienteId"] = new SelectList(_context.Clientes, "Id", "Nombre");
            return View();
        }

        // POST: Cotizacion/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ClienteId,Fecha")] Cotizacion cotizacion)
        {
                cotizacion.Id = Guid.NewGuid();
                _context.Add(cotizacion);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
        }


        // GET: Cotizacion/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null || _context.Cotizaciones == null)
            {
                return NotFound();
            }

            var cotizacion = await _context.Cotizaciones.FindAsync(id);
            if (cotizacion == null)
            {
                return NotFound();
            }

            // Guardar la fecha original antes de mostrar el formulario de edición
            TempData["FechaOriginal"] = cotizacion.Fecha;

            ViewData["ClienteId"] = new SelectList(_context.Clientes, "Id", "Nombre", cotizacion.ClienteId);
            return View(cotizacion);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,ClienteId,Fecha")] Cotizacion cotizacion)
        {
            if (id != cotizacion.Id)
            {
                return NotFound();
            }

            try
            {
                var cotizacionExistente = await _context.Cotizaciones.FindAsync(id);

                cotizacion.Fecha = (DateTime)TempData["FechaOriginal"];

                _context.Entry(cotizacionExistente).CurrentValues.SetValues(cotizacion);

                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CotizacionExists(cotizacion.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToAction(nameof(Index));
        }

        // GET: Cotizacion/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null || _context.Cotizaciones == null)
            {
                return NotFound();
            }

            var cotizacion = await _context.Cotizaciones
                .Include(c => c.Cliente)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (cotizacion == null)
            {
                return NotFound();
            }

            return View(cotizacion);
        }

        // POST: Cotizacion/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var cotizacion = await _context.Cotizaciones
                .Include(c => c.Detalles)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (cotizacion == null)
            {
                return NotFound();
            }

            _context.Cotizaciones.Remove(cotizacion);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        private bool CotizacionExists(Guid id)
        {
          return (_context.Cotizaciones?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
