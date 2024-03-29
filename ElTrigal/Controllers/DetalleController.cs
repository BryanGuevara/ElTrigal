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

            if (detalle.Descuento == null)
            {
                detalle.Descuento = 0;
            }

            var producto = await _context.Productos
                        .FirstOrDefaultAsync(c => c.Id == detalle.ProductoId);

            if (producto != null)
            {
                if (producto.Cantidad > 0)
                {
                    if (detalle.Cantidad >= producto.Cantidad)
                    {
                        producto.Cantidad = 0;
                    }
                    else
                    {
                        producto.Cantidad -= detalle.Cantidad;
                    }

                    _context.Update(producto);
                }
            }

            var cotizacion = await _context.Cotizaciones
                        .Include(c => c.Cliente)
                        .FirstOrDefaultAsync(c => c.Id == detalle.PerteneceId);

            var Producto = producto?.Nombre ?? "Producto Desconocido";

            var clienteNombre = cotizacion?.Cliente?.Nombre ?? "Cliente Desconocido";

            var nuevoInforme = new Informe
            {
                Id = Guid.NewGuid(),
                TipoInforme = "Detalle",
                DetallesInforme = $"Se añadió a la cotización del cliente {clienteNombre} {detalle.Cantidad} {Producto} con el {detalle.Descuento}% de descuento",
                FechaGeneracion = DateTime.Now
            };

            _context.Add(nuevoInforme);
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
                if (detalle.Descuento == null)
                {
                    detalle.Descuento = 0;
                }
                if (detalle.Cantidad != null || detalle.Cantidad != 0)
                {
                    detalle.Descuento = 0;
                    _context.Update(detalle);

                    var venta = await _context.Ventas
                                              .Include(v => v.Cliente)
                                              .FirstOrDefaultAsync(v => v.Id == detalle.PerteneceId);


                    var clienteNombre = venta.Cliente?.Nombre ?? "Cliente Desconocido";

                    var nuevoInforme = new Informe

                    {
                        Id = Guid.NewGuid(),
                        TipoInforme = "Detalle",
                        DetallesInforme = $"Se le edito a la factura de el cliente {detalle.Venta.Cliente?.Nombre} {detalle.Cantidad} {detalle.Producto?.Nombre} con el {detalle.Descuento}% de descuento",
                        FechaGeneracion = DateTime.Now
                    };

                    _context.Add(nuevoInforme);
                    await _context.SaveChangesAsync();
                }
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

            var venta = await _context.Ventas
                                      .Include(v => v.Cliente)
                                      .FirstOrDefaultAsync(v => v.Id == detalle.PerteneceId);


            var clienteNombre = venta.Cliente?.Nombre ?? "Cliente Desconocido";

            var nuevoInforme = new Informe

            {
                Id = Guid.NewGuid(),
                TipoInforme = "Detalle",
                DetallesInforme = $"Se elimino de la factura de el cliente {detalle.Venta.Cliente?.Nombre} {detalle.Cantidad} {detalle.Producto?.Nombre} con el {detalle.Descuento}% de descuento",
                FechaGeneracion = DateTime.Now
            };

            _context.Add(nuevoInforme);
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
