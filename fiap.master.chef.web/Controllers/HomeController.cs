using fiap.master.chef.core.Context;
using fiap.master.chef.core.Models;
using fiap.master.chef.web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace fiap.master.chef.web.Controllers
{
    public class HomeController : Controller
    {
        private readonly MasterChefDBContext _context;

        public HomeController(ILogger<HomeController> logger, MasterChefDBContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Doces()
        {
            var receitas = await _context.Receitas
                .Where(r => r.Categoria == "doces").ToListAsync();

            return View(receitas);
        }

        public async Task<IActionResult> Salgados()
        {
            var receitas = await _context.Receitas
                .Where(r => r.Categoria == "salgados").ToListAsync();

            return View(receitas);
        }

        public async Task<ActionResult<Receita>> Detalhe(Guid id)
        {
            var receita = await _context.Receitas
                .Include(i => i.Ingredientes)
                .Include(t => t.Tags)
                .FirstOrDefaultAsync(r => r.Id.Equals(id));

            return View(receita);
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { 
                RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier 
            });
        }
    }
}