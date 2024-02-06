using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ElTrigal.Models;

namespace ElTrigal.Controllers
{
    public class MarcasController : Controller
    {
        private readonly ElTrigalContext _context;

        public MarcasController(ElTrigalContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var marcas = await _context.Marcas.Include(m => m.Proveedor).ToListAsync();
            return View(marcas);
        }

        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var marca = await _context.Marcas
                .Include(m => m.Proveedor)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (marca == null)
            {
                return NotFound();
            }

            return View(marca);
        }

        public IActionResult Create()
        {
            ViewData["ProveedorId"] = new SelectList(_context.Proveedors, "Id", "Nombre");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Nombre,ProveedorId,Descripcion,Especialidad")] Marca marca)
        {
            marca.Id = Guid.NewGuid();
            _context.Add(marca);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var marca = await _context.Marcas.FindAsync(id);
            if (marca == null)
            {
                return NotFound();
            }
            ViewData["ProveedorId"] = new SelectList(_context.Proveedors, "Id", "Nombre", marca.ProveedorId);
            return View(marca);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,Nombre,ProveedorId,Descripcion,Especialidad")] Marca marca)
        {
            if (id != marca.Id)
            {
                return NotFound();
            }

            try
            {
                _context.Update(marca);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MarcaExists(marca.Id))
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
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var marca = await _context.Marcas
                .Include(m => m.Proveedor)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (marca == null)
            {
                return NotFound();
            }

            return View(marca);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var marca = await _context.Marcas.FindAsync(id);
            if (marca != null)
            {
                _context.Marcas.Remove(marca);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

        private bool MarcaExists(Guid id)
        {
            return _context.Marcas.Any(e => e.Id == id);
        }
    }
}
