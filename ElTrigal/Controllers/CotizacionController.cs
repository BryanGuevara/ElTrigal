using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ElTrigal.Models;
using Microsoft.Extensions.Logging;

namespace ElTrigal.Controllers
{
    public class CotizacionController : Controller
    {
        private readonly ElTrigalContext _context;
        private readonly ILogger<CotizacionController> _logger;

        public CotizacionController(ElTrigalContext context, ILogger<CotizacionController> logger)
        {
            _logger = logger;
            _context = context;
        }

        // GET: Cotizacions
        public async Task<IActionResult> Index()
        {
            try
            {
                var cotizaciones = await _context.Cotizaciones
                    .Include(c => c.Cliente)
                    .ToListAsync();

                return View(cotizaciones);
            }
            catch (Exception ex)
            {
                return Problem($"Error al cargar las cotizaciones: {ex.Message}");
            }
        }

        // GET: Cotizacions/Details/5
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

        // GET: Cotizacions/Create
        public IActionResult Create()
        {
            ViewData["ClienteId"] = new SelectList(_context.Clientes, "Id", "Nombre");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ClienteId,Fecha")] Cotizacion cotizacion)
        {
            try
            {
                _logger.LogInformation($"Modelo recibido: {cotizacion.Id}, {cotizacion.ClienteId}, {cotizacion.Fecha}");

                cotizacion.Fecha = DateTime.Now;

                cotizacion.Id = Guid.NewGuid();
                _context.Add(cotizacion);
                await _context.SaveChangesAsync();

                ViewData["ClienteId"] = new SelectList(_context.Clientes, "Id", "Id", cotizacion.ClienteId);

                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateException ex)
            {
                return View(cotizacion);
            }
        }


        // GET: Cotizacions/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cotizacion = await _context.Cotizaciones
                .Include(c => c.Cliente) 
                .Include(c => c.Detalles)
                    .ThenInclude(d => d.Producto)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (cotizacion == null)
            {
                return NotFound();
            }

            ViewBag.Clientes = new SelectList(_context.Clientes, "Id", "Nombre", cotizacion.ClienteId);

            return View(cotizacion);
        }


        // POST: Cotizacions/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,ClienteId,Fecha,Detalles")] Cotizacion cotizacion)
        {
            if (id != cotizacion.Id)
            {
                return NotFound();
            }

                try
                {
                    _context.Update(cotizacion);
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

        // GET: Cotizacions/Delete/5
        public async Task<IActionResult> Delete(Guid id)
        {
            var cotizacion = await _context.Cotizaciones
                .Include(c => c.Detalles)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (cotizacion == null)
            {
                return NotFound();
            }

            _context.Cotizaciones.Remove(cotizacion);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var cotizacion = await _context.Cotizaciones
                .Include(c => c.Detalles)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (cotizacion == null)
            {
                return NotFound();
            }

            _context.Cotizaciones.Remove(cotizacion);

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Detalle(Guid id)
        {
            var cotizacion = await _context.Cotizaciones
                .Include(c => c.Detalles)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (cotizacion == null)
            {
                return NotFound();
            }

            var productos = await _context.Productos.ToListAsync();

            ViewBag.CotizacionId = cotizacion.Id;
            ViewBag.Productos = new SelectList(productos, "Id", "Nombre");
            ViewBag.Detalles = cotizacion.Detalles;

            return View("Detalle");
        }

        [HttpPost]
        public async Task<IActionResult> AgregarProducto()
        {
            var cotizacionId = Guid.Parse(Request.Form["CotizacionId"]);
            var productoId = Guid.Parse(Request.Form["ProductoId"]);
            var cantidad = int.Parse(Request.Form["Cantidad"]);

            if (ModelState.IsValid)
            {
                var detalle = new Detalle
                {
                    CotizacionId = cotizacionId,
                    ProductoId = productoId,
                    Cantidad = cantidad
                };

                _context.Detalle.Add(detalle);
                await _context.SaveChangesAsync();

                return RedirectToAction("Detalle", new { id = cotizacionId });
            }

            return RedirectToAction("Detalle", new { id = cotizacionId });
        }
        public async Task<IActionResult> EditarProducto(Guid id)
        {
            var detalle = await _context.Detalle
                .Include(d => d.Producto) 
                .FirstOrDefaultAsync(d => d.Id == id);

            if (detalle == null)
            {
                return NotFound();
            }

            return View(detalle);
        }


        [HttpPost]
        public async Task<IActionResult> EliminarProducto(Guid id)
        {
            var detalle = await _context.Detalle.FindAsync(id);

            if (detalle == null)
            {
                return NotFound();
            }

            _context.Detalle.Remove(detalle);
            await _context.SaveChangesAsync();

            return RedirectToAction("Edit", new { id = detalle.CotizacionId });
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteProductoConfirmed(Guid id)
        {
            var detalle = await _context.Detalle.FindAsync(id);

            if (detalle == null)
            {
                return NotFound();
            }

            _context.Detalle.Remove(detalle);
            await _context.SaveChangesAsync();

            // Redirigir a la acción deseada después de la eliminación
            return RedirectToAction("Edit", new { id = detalle.CotizacionId });
        }

        [HttpPost]
        public async Task<IActionResult> GuardarEdicionProducto(Detalle detalle)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var detalleExistente = await _context.Detalle.FindAsync(detalle.Id);

                    if (detalleExistente == null)
                    {
                        return NotFound();
                    }

                    detalleExistente.Cantidad = detalle.Cantidad;

                    await _context.SaveChangesAsync();

                    return RedirectToAction("Edit", new { id = detalle.CotizacionId });
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, $"Error al guardar los cambios: {ex.Message}");
                    return View("Edit", detalle);
                }
            }

            return View("EditarProducto", detalle);
        }

        public async Task<IActionResult> EditarCliente(Guid clienteId)
        {
            try
            {
                var cliente = await _context.Clientes.FindAsync(clienteId);

                if (cliente == null)
                {
                    return NotFound();
                }

                return View("EditarCliente", cliente);
            }
            catch (Exception ex)
            {
                return Problem($"Error al cargar los detalles del cliente: {ex.Message}");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditarCliente(Guid id, [Bind("Id,Nombre,Telefono,Direccion")] Cliente cliente)
        {
            if (id != cliente.Id)
            {
                return NotFound();
            }

            try
            {
                _context.Update(cliente);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ClienteExists(cliente.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return RedirectToAction(nameof(Edit));
        }


        private bool CotizacionExists(Guid id)
        {
          return (_context.Cotizaciones?.Any(e => e.Id == id)).GetValueOrDefault();
        }
        private bool ClienteExists(Guid id)
        {
            return _context.Clientes.Any(e => e.Id == id);
        }
    }
}
