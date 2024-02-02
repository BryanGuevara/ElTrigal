using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ElTrigal.Models;

namespace ElTrigal.Controllers
{
    public class RolsController : Controller
    {
        private readonly ElTrigalContext _context;

        public RolsController(ElTrigalContext context)
        {
            _context = context;
        }

        // GET: Rols
        public async Task<IActionResult> Index()
        {
            return _context.Roles != null ?
                          View(await _context.Roles.ToListAsync()) :
                          Problem("Entity set 'ElTrigalContext.Roles'  is null.");
        }

        // GET: Rols/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null || _context.Roles == null)
            {
                return NotFound();
            }

            var rol = await _context.Roles
                .FirstOrDefaultAsync(m => m.Id == id);
            if (rol == null)
            {
                return NotFound();
            }

            return View(rol);
        }

        // GET: Rols/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Rols/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nombre,VerTodo,VerUsuario,EditUsuario,EliUsuario,CrearUsuario,EditPass,VerCliente,EditCliente,EliCliente,CrearCliente,VerProveedor,EditProveedor,EliProveedor,CrearProveedor,VerMarca,EditMarca,EliMarca,CrearMarca,VerCategoria,EditCategoria,EliCategoria,CrearCategoria,VerProductos,EditProductos,EliProductos,CrearProductos,VerCotizaciones,EditCotizaciones,EliCotizaciones,CrearCotizaciones,VerVentas,EditVentas,EliVentas,CrearVentas,VerRol,EditRol,EliRol,CrearRol,VerAnalisis")] Rol rol)
        {
            // Asignar permisos de "Ver" automáticamente basados en los permisos de edición
            if (rol.EditUsuario || rol.EliUsuario || rol.CrearUsuario || rol.EditPass)
                rol.VerUsuario = true;

            if (rol.EditCliente || rol.EliCliente || rol.CrearCliente)
                rol.VerCliente = true;

            if (rol.EditProveedor || rol.EliProveedor || rol.CrearProveedor)
                rol.VerProveedor = true;

            if (rol.EditMarca || rol.EliMarca || rol.CrearMarca)
                rol.VerMarca = true;

            if (rol.EditCategoria || rol.EliCategoria || rol.CrearCategoria)
                rol.VerCategoria = true;

            if (rol.EditProductos || rol.EliProductos || rol.CrearProductos || rol.Pedido)
                rol.VerProductos = true;

            if (rol.EditCotizaciones || rol.EliCotizaciones || rol.CrearCotizaciones)
                rol.VerCotizaciones = true;

            if (rol.EditVentas || rol.EliVentas || rol.CrearVentas)
                rol.VerVentas = true;

            if (rol.EditRol || rol.EliRol || rol.CrearRol)
                rol.VerRol = true;

            rol.Id = Guid.NewGuid();
            _context.Add(rol);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // GET: Rols/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null || _context.Roles == null)
            {
                return NotFound();
            }

            var rol = await _context.Roles.FindAsync(id);
            if (rol == null)
            {
                return NotFound();
            }

            if (rol.EditUsuario || rol.EliUsuario || rol.CrearUsuario || rol.EditPass)
                rol.VerUsuario = true;

            if (rol.EditCliente || rol.EliCliente || rol.CrearCliente)
                rol.VerCliente = true;

            if (rol.EditProveedor || rol.EliProveedor || rol.CrearProveedor)
                rol.VerProveedor = true;

            if (rol.EditMarca || rol.EliMarca || rol.CrearMarca)
                rol.VerMarca = true;

            if (rol.EditCategoria || rol.EliCategoria || rol.CrearCategoria)
                rol.VerCategoria = true;

            if (rol.EditProductos || rol.EliProductos || rol.CrearProductos || rol.Pedido)
                rol.VerProductos = true;

            if (rol.EditCotizaciones || rol.EliCotizaciones || rol.CrearCotizaciones)
                rol.VerCotizaciones = true;

            if (rol.EditVentas || rol.EliVentas || rol.CrearVentas)
                rol.VerVentas = true;

            if (rol.EditRol || rol.EliRol || rol.CrearRol)
                rol.VerRol = true;

            return View(rol);
        }

        // POST: Rols/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,Nombre,VerTodo,VerUsuario,EditUsuario,EliUsuario,CrearUsuario,EditPass,VerCliente,EditCliente,EliCliente,CrearCliente,VerProveedor,EditProveedor,EliProveedor,CrearProveedor,VerMarca,EditMarca,EliMarca,CrearMarca,VerCategoria,EditCategoria,EliCategoria,CrearCategoria,VerProductos,EditProductos,EliProductos,CrearProductos,VerCotizaciones,EditCotizaciones,EliCotizaciones,CrearCotizaciones,VerVentas,EditVentas,EliVentas,CrearVentas,VerRol,EditRol,EliRol,CrearRol,VerAnalisis")] Rol rol)
        {
            if (id != rol.Id)
            {
                return NotFound();
            }

            // Asignar permisos de "Ver" automáticamente basados en los permisos de edición
            if (rol.EditCliente)
                rol.VerCliente = true;
            if (rol.EditProveedor)
                rol.VerProveedor = true;
            // Continuar para los demás permisos de edición

            try
            {
                _context.Update(rol);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RolExists(rol.Id))
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

        // GET: Rols/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null || _context.Roles == null)
            {
                return NotFound();
            }

            var rol = await _context.Roles
                .FirstOrDefaultAsync(m => m.Id == id);
            if (rol == null)
            {
                return NotFound();
            }

            return View(rol);
        }

        // POST: Rols/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            if (_context.Roles == null)
            {
                return Problem("Entity set 'ElTrigalContext.Roles'  is null.");
            }
            var rol = await _context.Roles.FindAsync(id);
            if (rol != null)
            {
                _context.Roles.Remove(rol);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RolExists(Guid id)
        {
            return (_context.Roles?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
