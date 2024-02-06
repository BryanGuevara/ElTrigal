using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ElTrigal.Models;

namespace ElTrigal.Controllers
{
    public class InformesController : Controller
    {
        private readonly ElTrigalContext _context;

        public InformesController(ElTrigalContext context)
        {
            _context = context;
        }

        // GET: Informes
        public async Task<IActionResult> Index(DateTime? fecha, string tipoInforme)
        {
            IQueryable<Informe> informes = _context.InformesAnalises;

            if (fecha.HasValue)
            {
                informes = informes.Where(i => i.FechaGeneracion.Date == fecha.Value.Date);
            }

            if (!string.IsNullOrEmpty(tipoInforme))
            {
                informes = informes.Where(i => i.TipoInforme == tipoInforme);
            }

            return View(await informes.ToListAsync());
        }

        public async Task<IActionResult> FiltroPorFecha(DateTime fecha)
        {
            return RedirectToAction(nameof(Index), new { fecha = fecha.Date });
        }

        public async Task<IActionResult> FiltroPorTipo(string tipoInforme)
        {
            return RedirectToAction(nameof(Index), new { tipoInforme = tipoInforme });
        }

        private bool InformeExists(Guid id)
        {
          return (_context.InformesAnalises?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
