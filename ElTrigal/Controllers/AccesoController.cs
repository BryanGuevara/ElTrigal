using Microsoft.AspNetCore.Mvc;
using ElTrigal.Models;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace ElTrigal.Controllers
{
    public class AccesoController : Controller
    {
        private readonly ElTrigalContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AccesoController(ElTrigalContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
            RoleContext.Configure(_httpContextAccessor);
        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Cotizacion()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Cotizacion(string codigoCotizacion)
        {
            var cotizacion = _context.Cotizaciones.FirstOrDefault(c => c.CodigoFactura == codigoCotizacion);

            if (cotizacion == null)
            {
                ModelState.AddModelError("", "La cotización no fue encontrada.");
                return View();
            }

            return RedirectToAction("Details", new { id = cotizacion.Id });
        }

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


        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(Usuario usuario)
        {
            var usuarioEncontrado = _context.Usuarios.SingleOrDefault(u => u.NameTag == usuario.NameTag);

            if (usuarioEncontrado != null && usuarioEncontrado.Pass == usuario.Pass)
            {
                var rolUsuario = _context.Roles.SingleOrDefault(r => r.Id == usuarioEncontrado.RolId);

                if (rolUsuario != null)
                {
                    RoleContext.CurrentRole = rolUsuario;
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "Error al obtener el rol del usuario");
                    return View();
                }
            }
            else
            {
                ModelState.AddModelError("", "Usuario o contraseña incorrectos");
                return View();
            }
        }
        private bool CotizacionExists(Guid id)
        {
            return _context.Cotizaciones.Any(e => e.Id == id);
        }
    }
}
