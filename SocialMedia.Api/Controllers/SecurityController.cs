using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SocialMedia.Api.Response;
using SocialMedia.Core.DTOs;
using SocialMedia.Core.Entities;
using SocialMedia.Core.Enumerations;
using SocialMedia.Core.Interfaces;
using SocialMedia.Infraestructure.Interfaz;
using System.Threading.Tasks;

namespace SocialMedia.Api.Controllers
{
    /// <summary>
    /// Roles 
    /// permite que solo administradores puedan crear usuarios
    /// </summary>
    [Authorize(Roles = nameof(RoleType.Administrador))]
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class SecurityController : ControllerBase
    {
        private readonly ISecurityService _securityService;
        private readonly IMapper _mapper;
        private readonly IPasswordService _passwordService;
        public SecurityController(ISecurityService securityService, IMapper mapper,IPasswordService passwordService)
        {
            _securityService = securityService;
            _mapper = mapper;
            _passwordService = passwordService;
        }
        [HttpPost]
        public async Task<IActionResult> Post(SecurityDTO securityDTO)
        {
            var security = _mapper.Map<Security>(securityDTO);
            security.Password = _passwordService.Hash(security.Password);
            await _securityService.RegisterUser(security);
            securityDTO= _mapper.Map<SecurityDTO>(security);
            var response = new ApiResponse<SecurityDTO>(securityDTO);
            return Ok(response);
        } 
    }
}
