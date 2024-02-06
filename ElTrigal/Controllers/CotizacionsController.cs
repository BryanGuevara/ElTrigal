using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ElTrigal.Models;

namespace ElTrigal.Controllers
{
    public class CotizacionsController : Controller
    {
        private readonly ElTrigalContext _context;

        public CotizacionsController(ElTrigalContext context)
        {
            _context = context;
        }

        // GET: Cotizacions
        public async Task<IActionResult> Index(string codigoFactura, DateTime? fechaDespuesDe, DateTime? fechaAntesDe)
        {
            var cotizaciones = await Filtro(codigoFactura, fechaDespuesDe, fechaAntesDe);

            ViewData["CodigoFactura"] = codigoFactura;
            ViewData["FechaDespuesDe"] = fechaDespuesDe;
            ViewData["FechaAntesDe"] = fechaAntesDe;

            return View(cotizaciones);
        }

        private async Task<List<Cotizacion>> Filtro(string codigoFactura, DateTime? fechaDespuesDe, DateTime? fechaAntesDe)
        {
            var cotizaciones = from c in _context.Cotizaciones.Include(c => c.Cliente)
                               select c;

            if (!String.IsNullOrEmpty(codigoFactura))
            {
                cotizaciones = cotizaciones.Where(c => c.CodigoFactura.Contains(codigoFactura));
            }

            if (fechaDespuesDe != null)
            {
                cotizaciones = cotizaciones.Where(c => c.Fecha >= fechaDespuesDe);
            }

            if (fechaAntesDe != null)
            {
                cotizaciones = cotizaciones.Where(c => c.Fecha <= fechaAntesDe);
            }

            return await cotizaciones.ToListAsync();
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

            decimal subTotal = cotizacion.Detalles.Sum(d => (d.Cantidad.HasValue ? d.Cantidad.Value : 0) * (d.Producto?.Precio ?? 0));
            decimal iva = cotizacion.Iva ? subTotal * 0.13m : 0;
            decimal total = subTotal + iva;

            ViewBag.SubTotal = subTotal;
            ViewBag.IVA = iva;
            ViewBag.Total = total;

            return View(cotizacion);
        }

        // GET: Cotizacions/Create
        public IActionResult Create()
        {
            ViewData["ClienteId"] = new SelectList(_context.Clientes, "Id", "Nombre");
            return View();
        }

        // POST: Cotizacions/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ClienteId,Iva,Dias,Entregado,Comentario,Pagado,Abono")] Cotizacion cotizacion)
        {
            cotizacion.CodigoPedido = GenerateUniqueRandomNumericString(7);
            cotizacion.CodigoFactura = GenerateUniqueRandomNumericString(8);
            cotizacion.Id = Guid.NewGuid();
            cotizacion.Fecha = DateTime.Now;
            cotizacion.Vencimiento = cotizacion.Fecha.AddDays(cotizacion.Dias);
            cotizacion.Entregado = false;
            cotizacion.Pagado = false;
            cotizacion.Abono ??= 0.00m;

            var nuevoInforme = new Informe

            {
                Id = Guid.NewGuid(),
                TipoInforme = "Cotización",
                DetallesInforme = $"Se creo la cotizacion con el codigo de factura {cotizacion.CodigoFactura} para el cliente {cotizacion.Cliente?.Nombre}",
                FechaGeneracion = DateTime.Now
            };

            _context.Add(nuevoInforme);
            _context.Add(cotizacion);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index", "Detalles", new { perteneceId = cotizacion.Id });
        }

        private string GenerateUniqueRandomNumericString(int length)
        {
            Random random = new Random();
            const string chars = "0123456789";
            string randomString;

            do
            {
                randomString = new string(Enumerable.Repeat(chars, length)
                    .Select(s => s[random.Next(s.Length)]).ToArray());
            }
            while (IsCodeExists(randomString));

            return randomString;
        }

        private bool IsCodeExists(string code)
        {
            return _context.Cotizaciones.Any(c => c.CodigoPedido == code || c.CodigoFactura == code);
        }


        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cotizacion = await _context.Cotizaciones.FindAsync(id);
            if (cotizacion == null)
            {
                return NotFound();
            }

            ViewData["ClienteId"] = new SelectList(_context.Clientes, "Id", "Nombre", cotizacion.ClienteId);

            return View(cotizacion);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,ClienteId,Iva,Dias,Entregado,Comentario,Pagado,Abono")] Cotizacion cotizacion)
        {
            try
            {
                if (id != cotizacion.Id)
                {
                    return NotFound();
                }

                var cotizacionOriginal = await _context.Cotizaciones.FindAsync(id);
                if (cotizacionOriginal == null)
                {
                    return NotFound();
                }

                cotizacionOriginal.ClienteId = cotizacion.ClienteId;
                cotizacionOriginal.Iva = cotizacion.Iva;
                cotizacionOriginal.Entregado = cotizacion.Entregado;
                cotizacionOriginal.Comentario = cotizacion.Comentario;
                cotizacionOriginal.Pagado = cotizacion.Pagado;
                cotizacionOriginal.Abono = cotizacion.Abono;

                cotizacionOriginal.Vencimiento = cotizacionOriginal.Fecha.AddDays(cotizacion.Dias);

                _context.Entry(cotizacionOriginal).State = EntityState.Modified;

                var nuevoInforme = new Informe
                {
                    Id = Guid.NewGuid(),
                    TipoInforme = "Cotización",
                    DetallesInforme = $"Se actualizó la cotización con el código de factura {cotizacion.CodigoFactura} para el cliente {cotizacion.Cliente?.Nombre}",
                    FechaGeneracion = DateTime.Now
                };

                _context.Add(nuevoInforme);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", $"Error al editar la cotización: {ex.Message}");
                return View(cotizacion);
            }
        }

        // GET: Cotizacions/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
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

        // POST: Cotizacions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var cotizacion = await _context.Cotizaciones.FindAsync(id);
            if (cotizacion == null)
            {
                return NotFound();
            }

            _context.Cotizaciones.Remove(cotizacion);

            var nuevoInforme = new Informe
            {
                Id = Guid.NewGuid(),
                TipoInforme = "Cotización",
                DetallesInforme = $"Se eliminó la cotización con el código de factura {cotizacion.CodigoFactura} para el cliente {cotizacion.Cliente?.Nombre}",
                FechaGeneracion = DateTime.Now
            };

            _context.Add(nuevoInforme);

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CotizacionExists(Guid id)
        {
            return _context.Cotizaciones.Any(e => e.Id == id);
        }
    }
}
