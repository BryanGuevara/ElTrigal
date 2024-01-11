using Microsoft.AspNetCore.Mvc;
using ElTrigal.Models;
using System.Linq;

namespace ElTrigal.Controllers
{
    public class AccesoController : Controller
    {
        private readonly ElTrigalContext _context;

        public AccesoController(ElTrigalContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(Usuario usuario)
        {
            var usuarioEncontrado = _context.Usuarios.SingleOrDefault(u => u.NameTag == usuario.NameTag);

            if (usuarioEncontrado != null && usuarioEncontrado.Pass == usuario.Pass)
            {
                var rolUsuario = _context.Roles.SingleOrDefault(r => r.Id == usuarioEncontrado.RolId);

                if (rolUsuario != null)
                {
                    if (rolUsuario.Nombre == "Administrador")
                    {
                        return RedirectToAction("Index", "Home");
                    }
                    else if (rolUsuario.Nombre == "Usuario")
                    {
                        return RedirectToAction("Index", "HomeUser");
                    }
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
            return View();
        }

    }
}
