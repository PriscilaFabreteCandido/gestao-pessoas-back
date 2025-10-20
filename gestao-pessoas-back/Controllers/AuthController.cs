using gestao_pessoas_back.Requests.Usuario;
using gestao_pessoas_back.Service.Interfaces;
using gestao_pessoas_back.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace gestao_pessoas_back.Controllers
{
    [ApiController]
    [Route("v1/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IUsuarioService _usuarioService;
        private readonly ILogger<AuthController> _logger;

        public AuthController(
            IUsuarioService usuarioService,
            ILogger<AuthController> logger)
        {
            _usuarioService = usuarioService;
            _logger = logger;
        }

       
        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
           
            var token = await _usuarioService.AutenticarUsuario(request);

            return Ok(token);
           
        }


        [HttpGet("user-info")]
        [Authorize]
        public async Task<IActionResult> Get()
        {
            var userInfo = await _usuarioService.ObterInformacoesUsuario();
            return Ok(userInfo);
        }
    }
}