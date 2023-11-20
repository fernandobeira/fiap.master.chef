using fiap.master.chef.core.Models;
using fiap.master.chef.core.Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace fiap.master.chef.api.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/[controller]")]
    public class ReceitaController : ControllerBase
    {
        private readonly MasterChefDBContext _context;

        public ReceitaController(MasterChefDBContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<Receita>>> Get()
        {
            var result = await _context.Receitas
                .Include(i => i.Ingredientes)
                .Include(t => t.Tags)
                .ToListAsync();

            if (result != null)
            {
                return Ok(result);
            }

            return NotFound();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Receita>> Get(Guid id)
        {
            var result = await _context.Receitas
                .Include(i => i.Ingredientes)
                .Include(t => t.Tags)
                .FirstOrDefaultAsync(r => r.Id.Equals(id));

            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<Receita>> Create(Receita model)
        {
            _context.Receitas.Add(model);
            await _context.SaveChangesAsync();

            return Created($"/api/receita/{model.Id}", model);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Receita>> Update(int id, Receita model)
        {
            _context.Attach(model);
            await _context.SaveChangesAsync();
            return model;
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(Guid id)
        {
            var receita = await _context.Receitas.FirstOrDefaultAsync(r => r.Id.Equals(id));
            if (receita == null)
            {
                return NotFound();
            }
            _context.Receitas.Remove(receita);
            await _context.SaveChangesAsync();

            return NoContent();

        }
    }
}
