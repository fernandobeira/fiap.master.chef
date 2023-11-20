using fiap.master.chef.core.Context;
using fiap.master.chef.core.Models;
using Microsoft.AspNetCore.Mvc;

namespace fiap.master.chef.web.ViewComponents
{
    public class ReceitaViewComponent : ViewComponent
    {
        private readonly MasterChefDBContext _context;

        public ReceitaViewComponent(MasterChefDBContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync(int total, string categoria)
        {
            var receitas = Load(total, categoria);
            return View("Receita", receitas);
            
        }

        private IEnumerable<Receita> Load(int total, string categoria)
        {
            var receitas = _context.Receitas;
            return receitas;
        }
    }
}
