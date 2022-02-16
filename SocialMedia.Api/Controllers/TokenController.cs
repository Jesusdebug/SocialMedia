
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using SocialMedia.Core.Entities;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace SocialMedia.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        private readonly IConfiguration _configuration; 
        public TokenController(IConfiguration configuration)
        {
                _configuration=configuration;
        }
        [HttpPost]
        public IActionResult Authentication(UserLoging user)
        {
            if (validateUser(user))
            {
                string token = GenerateToken();
                return Ok(new { token });
            }
            return NotFound();

        }

        private string GenerateToken()
        {
            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Authentication:secretKey"]));
            var signinfCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);
            var header = new JwtHeader(signinfCredentials);
            var claims = new[]
            {
                new Claim(ClaimTypes.Name,"Jesus Antonio Rozo"),
                new Claim(ClaimTypes.Email,"rozozapatajesus1997@gmial.com"),
                new Claim(ClaimTypes.Role,"Administrador")
            };
            var payload = new JwtPayload
            (
                _configuration["Authentication:Issure"],
                _configuration["Authentication:Audience"],
                claims,
                DateTime.Now,
                DateTime.UtcNow.AddMinutes(2)
            );
            var token=new JwtSecurityToken(header, payload);
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public bool validateUser(UserLoging user)
        {
            return true;
        }
    }
}
