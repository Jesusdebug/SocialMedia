using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using SocialMedia.Core.Entities;
using SocialMedia.Core.Interfaces;
using SocialMedia.Infraestructure.Interfaz;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace SocialMedia.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IPasswordService _passwordService;
        private readonly ISecurityService _securityService;
        public TokenController(IConfiguration configuration, IPasswordService passwordService, ISecurityService securityService)
        {
            _configuration = configuration;
            _passwordService = passwordService;
            _securityService = securityService;
        }
        [HttpPost]
        public async Task<IActionResult> Authentication(UserLoging user)
        {
            var validation = await validateUser(user);
            if (validation.Item1)
            {
                string token = GenerateToken(validation.Item2);
                return Ok(new { token });
            }
            return NotFound();
        }
        private string GenerateToken(Security security)
        {
            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Authentication:secretKey"]));
            var signinfCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);
            var header = new JwtHeader(signinfCredentials);
            var claims = new[]
            {
                new Claim(ClaimTypes.Name,security.UserName),
                new Claim("User",security.User),
                new Claim(ClaimTypes.Role,security.Role.ToString())
            };
            var payload = new JwtPayload
            (
                _configuration["Authentication:Issure"],
                _configuration["Authentication:Audience"],
                claims,
                DateTime.Now,
                DateTime.UtcNow.AddMinutes(2)
            );
            var token = new JwtSecurityToken(header, payload);
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
        public async Task<(bool,Security)> validateUser(UserLoging login)
        {
            var user = await _securityService.GetLoginByCredentials(login);
            var isValid = _passwordService.Check(user.Password, login.password);
            return (isValid,user);
        }
    }
}
