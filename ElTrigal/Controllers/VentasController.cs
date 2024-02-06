﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ElTrigal.Models;

namespace ElTrigal.Controllers
{
    public class VentasController : Controller
    {
        private readonly ElTrigalContext _context;

        public VentasController(ElTrigalContext context)
        {
            _context = context;
        }

        // GET: Ventas
        public async Task<IActionResult> Index()
        {
              return _context.Ventas != null ? 
                          View(await _context.Ventas.Include(v => v.Cliente).ToListAsync()) :
                          Problem("Entity set 'ElTrigalContext.Ventas'  is null.");
        }

        // GET: Ventas/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var venta = await _context.Ventas
                .Include(c => c.Cliente)
                .Include(c => c.Detalles)
                .ThenInclude(d => d.Producto)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (venta == null)
            {
                return NotFound();
            }

            decimal total = venta.Detalles.Sum(d => (d.Cantidad ?? 0) * (d.Producto?.Precio ?? 0));

            ViewBag.Total = total;

            return View(venta);
        }

        // GET: Ventas/Create
        public IActionResult Create()
        {
            ViewData["ClienteId"] = new SelectList(_context.Clientes, "Id", "Nombre");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ClienteId")] Venta venta)
        {
            if (ModelState.IsValid)
            {
                venta.Id = Guid.NewGuid();
                venta.FechaVenta = DateTime.Now;

                var cliente = await _context.Clientes.FindAsync(venta.ClienteId);

                var nuevoInforme = new Informe
                {
                    Id = Guid.NewGuid(),
                    TipoInforme = "Venta",
                    DetallesInforme = $"Se hizo una venta para el cliente {(cliente != null ? cliente.Nombre : "Cliente Desconocido")}",
                    FechaGeneracion = DateTime.Now
                };

                _context.Add(nuevoInforme);
                _context.Add(venta);
                await _context.SaveChangesAsync();

                return RedirectToAction("Index", "Detalle", new { perteneceId = venta.Id });
            }
            ViewData["ClienteId"] = new SelectList(_context.Clientes, "Id", "Nombre", venta.ClienteId);
            return View(venta);
        }

        // GET: Ventas/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            ViewData["ClienteId"] = new SelectList(_context.Clientes, "Id", "Nombre");

            if (id == null || _context.Ventas == null)
            {
                return NotFound();
            }

            var venta = await _context.Ventas.FindAsync(id);
            if (venta == null)
            {
                return NotFound();
            }
            return View(venta);
        }

        // POST: Ventas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,ClienteId,FechaVenta,TotalVenta")] Venta venta)
        {
            if (id != venta.Id)
            {
                return NotFound();
            }

                try
                {
                    _context.Update(venta);
                var cliente = await _context.Clientes.FindAsync(venta.ClienteId);
                var nuevoInforme = new Informe

                {
                    Id = Guid.NewGuid(),
                    TipoInforme = "Cotización",
                    DetallesInforme = $"Se actualizo la venta para el cliente {venta.Cliente?.Nombre}",
                    FechaGeneracion = DateTime.Now
                };

                _context.Add(nuevoInforme);
                await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VentaExists(venta.Id))
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

        // GET: Ventas/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null || _context.Ventas == null)
            {
                return NotFound();
            }

            var venta = await _context.Ventas
                .FirstOrDefaultAsync(m => m.Id == id);
            if (venta == null)
            {
                return NotFound();
            }

            return View(venta);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var venta = await _context.Ventas
                                        .Include(v => v.Cliente)
                                        .FirstOrDefaultAsync(v => v.Id == id);
            if (venta == null)
            {
                return NotFound();
            }

            _context.Ventas.Remove(venta);

            var nuevoInforme = new Informe
            {
                Id = Guid.NewGuid(),
                TipoInforme = "Venta",
                DetallesInforme = $"Se eliminó la venta para el cliente {(venta.Cliente != null ? venta.Cliente.Nombre : "Cliente Desconocido")}",
                FechaGeneracion = DateTime.Now
            };

            _context.Add(nuevoInforme);

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VentaExists(Guid id)
        {
          return (_context.Ventas?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
