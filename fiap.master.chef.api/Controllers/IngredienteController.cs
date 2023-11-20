using fiap.master.chef.core.Context;
using fiap.master.chef.core.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace fiap.master.chef.api.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/[controller]")]
    public class IngredienteController : ControllerBase
    {
        private readonly MasterChefDBContext _context;

        public IngredienteController(MasterChefDBContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<Ingrediente>>> Get()
        {
            var result = await _context.Ingredientes
                .ToListAsync();

            if (result != null)
            {
                return Ok(result);
            }

            return NotFound();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Ingrediente>> Get(Guid id)
        {
            var result = await _context.Ingredientes
                .FirstOrDefaultAsync(r => r.Id.Equals(id));

            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<Ingrediente>> Create(Ingrediente model)
        {
            _context.Ingredientes.Add(model);
            await _context.SaveChangesAsync();

            return Created($"/api/ingrediente/{model.Id}", model);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Ingrediente>> Update(int id, Ingrediente model)
        {
            _context.Attach(model);
            await _context.SaveChangesAsync();
            return model;
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(Guid id)
        {
            var ingrediente = await _context.Ingredientes.FirstOrDefaultAsync(r => r.Id.Equals(id));
            if (ingrediente == null)
            {
                return NotFound();
            }
            _context.Ingredientes.Remove(ingrediente);
            await _context.SaveChangesAsync();

            return NoContent();

        }

    }
}
