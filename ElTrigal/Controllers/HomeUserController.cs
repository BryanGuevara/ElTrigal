using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using ElTrigal.Models;

namespace ElTrigal.Controllers
{
    public class HomeUserController : Controller
    {

        private readonly ElTrigalContext _context;
        private readonly ILogger<HomeController> _logger;

        public HomeUserController(ILogger<HomeController> logger, ElTrigalContext context)
        {
            _logger = logger;
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var productos = await _context.Productos
                .Include(p => p.Marca)
                .Include(p => p.Categoria)
                .ToListAsync();

            return View(productos);
        }
        public IActionResult Cotizacion()
        {
            return View();
        }
        public IActionResult Personas()
        {
            return View();
        }
        public IActionResult Productos()
        {
            return View();
        }
        public IActionResult Privacy()
        {
            return View();
        }
        public IActionResult VerUsuario()
        {
            var usuarios = _context.Usuarios
                .Include(u => u.Rol)
                .ToList();

            return View(usuarios);
        }

        public IActionResult VerCliente()
        {
            var clientes = _context.Clientes.ToList();

            return View(clientes);
        }
    }
}
