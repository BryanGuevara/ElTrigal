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
    public class DetallesController : Controller
    {
        private readonly ElTrigalContext _context;

        public DetallesController(ElTrigalContext context)
        {
            _context = context;
        }

        // GET: Detalles
        public async Task<IActionResult> Index(Guid? cotizacionId)
        {
            if (cotizacionId == null)
            {
                return NotFound();
            }

            var detalles = await _context.Detalle
                .Include(d => d.Cotizacion)
                .Include(d => d.Producto)
                .Where(d => d.CotizacionId == cotizacionId)
                .ToListAsync();

            if (detalles == null)
            {
                return NotFound();
            }

            return View(detalles);
        }




        // GET: Detalles/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null || _context.Detalle == null)
            {
                return NotFound();
            }

            var detalle = await _context.Detalle
                .Include(d => d.Cotizacion)
                .Include(d => d.Producto)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (detalle == null)
            {
                return NotFound();
            }

            return View(detalle);
        }

        public IActionResult Create(Guid? cotizacionId)
        {
            if (cotizacionId == null || !_context.Cotizaciones.Any(c => c.Id == cotizacionId))
            {
                return NotFound();
            }

            ViewData["ProductoId"] = new SelectList(_context.Productos, "Id", "Nombre");
            ViewBag.CotizacionId = cotizacionId;

            return View();
        }

        // POST: Detalles/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,CotizacionId,ProductoId,Cantidad")] Detalle detalle)
        {
            try
            {
                    detalle.Id = Guid.NewGuid();
                    _context.Add(detalle);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index), new { cotizacionId = detalle.CotizacionId });

            }
            catch (DbUpdateException)
            {
                // Log the error (uncomment ex variable name and write a log.)
                ModelState.AddModelError("", "No se pudo guardar los cambios. Intente nuevamente y si el problema persiste, consulte con el administrador del sistema.");
            }

            ViewData["ProductoId"] = new SelectList(_context.Productos, "Id", "Nombre", detalle.ProductoId);
            return View(detalle);
        }



        // GET: Detalles/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null || _context.Detalle == null)
            {
                return NotFound();
            }

            var detalle = await _context.Detalle.FindAsync(id);
            if (detalle == null)
            {
                return NotFound();
            }
            ViewData["CotizacionId"] = new SelectList(_context.Cotizaciones, "Id", "Id", detalle.CotizacionId);
            ViewData["ProductoId"] = new SelectList(_context.Productos, "Id", "Nombre", detalle.ProductoId);
            return View(detalle);
        }

        // POST: Detalles/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,CotizacionId,ProductoId,Cantidad")] Detalle detalle)
        {
            if (id != detalle.Id)
            {
                return NotFound();
            }
            try
                {
                    _context.Update(detalle);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DetalleExists(detalle.Id))
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

        // GET: Detalles/Delete/{id}
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var detalle = await _context.Detalle
                .Include(d => d.Producto)
                .Include(d => d.Cotizacion)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (detalle == null)
            {
                return NotFound();
            }

            return View(detalle);
        }

        // POST: Detalles/Delete/{id}
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var detalle = await _context.Detalle.FindAsync(id);
            if (detalle == null)
            {
                return NotFound();
            }

            _context.Detalle.Remove(detalle);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index", new { cotizacionId = detalle.CotizacionId });
        }

        private bool DetalleExists(Guid id)
        {
          return (_context.Detalle?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
