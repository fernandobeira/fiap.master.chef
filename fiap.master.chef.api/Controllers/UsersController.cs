using fiap.master.chef.core.Models;
using fiap.master.chef.core.Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Text;

namespace fiap.master.chef.api.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly MasterChefDBContext _context;

        public UsersController(MasterChefDBContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task <ActionResult<ApplicationUser>> Create(ApplicationUser model)
        {
            if (string.IsNullOrEmpty(model.UserName) && string.IsNullOrEmpty(model.Password))
            {
                return BadRequest(model);
            }

            // simulando criptografia do password.
            byte[] passwordInBytes = Encoding.ASCII.GetBytes(model.Password);
            model.Password = Convert.ToBase64String(passwordInBytes);

            _context.ApplicationUsers.Add(model);
            await _context.SaveChangesAsync();

            return Created($"/api/users/{model.Id}", model);
        }

    }
}
