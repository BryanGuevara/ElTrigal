using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using ElTrigal.Models;

namespace ElTrigal.Controllers
{
    public class HomeController : Controller
    {
        private readonly ElTrigalContext _context;
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger, ElTrigalContext context)
        {
            _logger = logger;
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            ViewBag.Marcas = await _context.Marcas.ToListAsync() ?? new List<Marca>();
            ViewBag.Categorias = await _context.Categoria.ToListAsync() ?? new List<Categoria>();
            ViewBag.Proveedores = await _context.Proveedors.ToListAsync() ?? new List<Proveedor>();

            var productos = await _context.Productos
                .Include(p => p.Categoria)
                .Include(p => p.Marca)
                    .ThenInclude(m => m.Proveedor)
                .ToListAsync();

            return View(productos);
        }
        public async Task<IActionResult> Filtro(string codigo, string nombre, Guid? marca, Guid? categoria, Guid? proveedor)
        {
            ViewBag.Nombre = nombre;
            ViewBag.Marcas = await _context.Marcas.ToListAsync() ?? new List<Marca>();
            ViewBag.Categorias = await _context.Categoria.ToListAsync() ?? new List<Categoria>();
            ViewBag.Proveedores = await _context.Proveedors.ToListAsync() ?? new List<Proveedor>();

            var productosQuery = _context.Productos
                .Include(p => p.Categoria)
                .Include(p => p.Marca)
                    .ThenInclude(m => m.Proveedor)
                .AsQueryable();

            if (!string.IsNullOrEmpty(codigo))
            {
                productosQuery = productosQuery.Where(p => p.Codigo.Contains(codigo));
            }

            if (!string.IsNullOrEmpty(nombre))
            {
                productosQuery = productosQuery.Where(p => p.Nombre.Contains(nombre));
            }

            if (marca.HasValue)
            {
                productosQuery = productosQuery.Where(p => p.MarcaId == marca);
            }

            if (categoria.HasValue)
            {
                productosQuery = productosQuery.Where(p => p.CategoriaId == categoria);
            }

            if (proveedor.HasValue)
            {
                productosQuery = productosQuery.Where(p => p.Marca != null && p.Marca.ProveedorId == proveedor);
            }

            var productos = await productosQuery.ToListAsync();

            return View("Index", productos);
        }

        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null || _context.Productos == null)
            {
                return NotFound();
            }

            var producto = await _context.Productos
                .Include(p => p.Categoria)
                .Include(p => p.Marca)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (producto == null)
            {
                return NotFound();
            }

            return View(producto);
        }
        public async Task<IActionResult> DetailsCat(Guid? id)
        {
            if (id == null || _context.Productos == null)
            {
                return NotFound();
            }

            var producto = await _context.Productos
                .Include(p => p.Categoria)
                .Include(p => p.Marca)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (producto == null)
            {
                return NotFound();
            }

            return View(producto);
        }
        public IActionResult Catalogo()
        {
            var proveedores = _context.Proveedors.ToList() ?? new List<Proveedor>();

            ViewBag.Proveedores = proveedores;

            return View();
        }
        public IActionResult VerCatalogo(Guid id)
        {
            var proveedor = _context.Proveedors
                .Include(p => p.Marca)
                .ThenInclude(m => m.Productos)
                .ThenInclude(p => p.Categoria)
                .FirstOrDefault(p => p.Id == id);


            if (proveedor == null)
            {
                return NotFound();
            }

            var productos = proveedor.Marca.SelectMany(m => m.Productos).ToList();
            ViewBag.Productos = productos;

            var marcas = productos.Select(p => p.Marca).Distinct().ToList();
            ViewBag.Marcas = marcas;

            return View();
        }

        public IActionResult Ventas()
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
        public IActionResult CambiarPermisosDefault()
        {
            RoleContext.DefaultR = new Rol
            {
                Nombre = "Baneado",
                VerTodo = false,
                VerUsuario = false,
                EditUsuario = false,
                EliUsuario = false,
                CrearUsuario = false,
                EditPass = false,
                VerCliente = false,
                EditCliente = false,
                EliCliente = false,
                CrearCliente = false,
                VerProveedor = false,
                EditProveedor = false,
                EliProveedor = false,
                CrearProveedor = false,
                VerMarca = false,
                EditMarca = false,
                EliMarca = false,
                CrearMarca = false,
                VerCategoria = false,
                EditCategoria = false,
                EliCategoria = false,
                CrearCategoria = false,
                VerProductos = false,
                EditProductos = false,
                EliProductos = false,
                CrearProductos = false,
                VerCotizaciones = false,
                EditCotizaciones = false,
                EliCotizaciones = false,
                CrearCotizaciones = false,
                VerVentas = false,
                EditVentas = false,
                EliVentas = false,
                CrearVentas = false,
                VerRol = false,
                EditRol = false,
                EliRol = false,
                CrearRol = false,
                VerAnalisis = false
            };

            return RedirectToAction("Index", "Home");
        }
    }

    
}