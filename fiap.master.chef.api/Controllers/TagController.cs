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
    public class TagController : ControllerBase
    {
        private readonly MasterChefDBContext _context;

        public TagController(MasterChefDBContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<Tag>>> Get()
        {
            var result = await _context.Tags
                .ToListAsync();

            if (result != null)
            {
                return Ok(result);
            }

            return NotFound();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Tag>> Get(Guid id)
        {
            var result = await _context.Tags
                .FirstOrDefaultAsync(r => r.Id.Equals(id));

            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<Tag>> Create(Tag model)
        {
            _context.Tags.Add(model);
            await _context.SaveChangesAsync();

            return Created($"/api/tag/{model.Id}", model);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Tag>> Update(int id, Tag model)
        {
            _context.Attach(model);
            await _context.SaveChangesAsync();
            return model;
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(Guid id)
        {
            var tags = await _context.Tags.FirstOrDefaultAsync(r => r.Id.Equals(id));
            if (tags == null)
            {
                return NotFound();
            }
            _context.Tags.Remove(tags);
            await _context.SaveChangesAsync();

            return NoContent();

        }

    }
}
