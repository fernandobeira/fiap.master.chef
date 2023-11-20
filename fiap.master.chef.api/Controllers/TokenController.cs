using fiap.master.chef.core.Context;
using fiap.master.chef.core.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace fiap.master.chef.api.Controllers
{
    [ApiController]
    [Route("api/token")]
    public class TokenController : Controller
    {
        private readonly MasterChefDBContext _context;

        public TokenController(MasterChefDBContext context)
        {
            _context = context;
        }

        [HttpPost]
        public IActionResult Create(TokenInfo model)
        {
            if (IsValidApiLogin(model))
            {
                var token = GenerateToken(model.UserName);
                var refreshToken = Guid.NewGuid().ToString();

                _context.Tokens.Add(new Token
                {
                    Refresh = refreshToken,
                    UserApi = model.UserName,
                    Revoked = false,
                    DtCreated = DateTime.UtcNow,
                    Used = false

                });
                _context.SaveChanges();

                return Ok(new { token, refreshToken });
            }

            return new BadRequestResult();
        }

        [HttpPost("refresh")]
        public IActionResult Refresh(RefreshTokenInfo model) 
        {
            var token = _context.Tokens.FirstOrDefault(x => x.Refresh.Equals(model.Refresh) && x.Used == false && x.Revoked == false);

            if (token == null)
            {
                return BadRequest();
            }

            var tokenJwt = GenerateToken(token.UserApi);
            var refreshToken = Guid.NewGuid().ToString();

            _context.Tokens.Add(new Token
            {
                Refresh = refreshToken,
                UserApi = token.UserApi,
                Revoked = false,
                DtCreated = DateTime.UtcNow,
                Used = false

            });

            token.Used = true;
            _context.SaveChanges();

            return Ok(new { token, refreshToken });
        }

        private bool IsValidApiLogin(TokenInfo model)
        {
            byte[] passwordInBytes = Encoding.ASCII.GetBytes(model.Password);
            var password = Convert.ToBase64String(passwordInBytes);

            var applicationUser = _context.ApplicationUsers.FirstOrDefault(x => x.UserName.Equals(model.UserName) && x.Password.Equals(password));

            if (applicationUser != null)
            {
                return true;
            }
            return false;
        }

        private string GenerateToken(string userName)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, userName),
                new Claim(JwtRegisteredClaimNames.Iss, "master.chef.api"),
                new Claim(JwtRegisteredClaimNames.Aud, "master.chef.api"),
                new Claim(JwtRegisteredClaimNames.Nbf, new DateTimeOffset(DateTime.Now).ToUnixTimeSeconds().ToString()),
                new Claim(JwtRegisteredClaimNames.Exp, new DateTimeOffset(DateTime.Now).AddMinutes(5).ToUnixTimeSeconds().ToString())
            };

            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("master-chef-api-auth-valid"));
            var signinCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

            var jwtHeader = new JwtHeader(signinCredentials);
            var jwtPayload = new JwtPayload(claims);

            var token = new JwtSecurityToken(jwtHeader, jwtPayload);

            var tokenHandler = new JwtSecurityTokenHandler();

            var tokenResult = tokenHandler.WriteToken(token);

            return tokenResult;
        }

    }
}
