using fiap.master.chef.core.Context;
using fiap.master.chef.core.Models;
using Microsoft.AspNetCore.Mvc;

namespace fiap.master.chef.web.Controllers
{
    public class AdminController : Controller
    {
        private readonly MasterChefDBContext _context;

        public AdminController(MasterChefDBContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Receita model)
        {
            if (ModelState.IsValid)
            {
                _context.Receitas.Add(model);
                await _context.SaveChangesAsync();
            }

            return Json(true);

        }

    }
}
