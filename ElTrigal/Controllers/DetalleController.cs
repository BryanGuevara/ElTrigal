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
    public class DetalleController : Controller
    {
        private readonly ElTrigalContext _context;

        public DetalleController(ElTrigalContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index(Guid? perteneceId)
        {
            if (perteneceId == null)
            {
                return NotFound();
            }

            var detalles = await _context.Detalle
                .Include(d => d.Venta)
                .Include(d => d.Producto)
                .Where(d => d.PerteneceId == perteneceId)
                .ToListAsync();

            if (detalles == null)
            {
                return NotFound();
            }

            ViewData["PerteneceId"] = perteneceId;

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
                .Include(d => d.Venta)
                .Include(d => d.Producto)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (detalle == null)
            {
                return NotFound();
            }

            return View(detalle);
        }

        public IActionResult Create(Guid? perteneceId)
        {
            if (perteneceId == null || !_context.Ventas.Any(c => c.Id == perteneceId))
            {
                return NotFound();
            }

            ViewData["ProductoId"] = new SelectList(_context.Productos, "Id", "Nombre");
            ViewBag.PerteneceId = perteneceId;

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,PerteneceId,ProductoId,Descuento, Cantidad")] Detalle detalle)
        {
                detalle.Id = Guid.NewGuid();
                _context.Add(detalle);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index), new { perteneceId = detalle.PerteneceId });
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

            ViewData["ProductoId"] = new SelectList(_context.Productos, "Id", "Nombre", detalle.ProductoId);
            return View(detalle);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,PerteneceId,ProductoId,Cantidad")] Detalle detalle)
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
                return RedirectToAction(nameof(Index), new { perteneceId = detalle.PerteneceId });
            }

        // GET: Detalles/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null || _context.Detalle == null)
            {
                return NotFound();
            }

            var detalle = await _context.Detalle
                .Include(d => d.Producto)
                .Include(d => d.Venta)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (detalle == null)
            {
                return NotFound();
            }

            return View(detalle);
        }

        // POST: Detalles/Delete/5
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

            return RedirectToAction(nameof(Index), new { perteneceId = detalle.PerteneceId });
        }

        private bool DetalleExists(Guid id)
        {
            return (_context.Detalle?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
