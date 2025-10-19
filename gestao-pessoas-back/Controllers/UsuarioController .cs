using gestao_pessoas_back.Requests.Usuario;
using gestao_pessoas_back.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace gestao_pessoas_back.Controllers
{
    [ApiController]
    [Route("v1/usuarios")]
    [Authorize]
    public class UsuariosController : ControllerBase
    {
        private readonly UsuarioService _usuarioService;
        private readonly ILogger<UsuariosController> _logger;

        public UsuariosController(
            UsuarioService usuarioService,
            ILogger<UsuariosController> logger)
        {
            _usuarioService = usuarioService;
            _logger = logger;
        }

        [HttpPost("/criar")]
        public async Task<IActionResult> Criar([FromBody] CriarUsuarioRequest request)
        {
             var usuarioAtualizado = await _usuarioService.CriarUsuario(request);
             return Ok(usuarioAtualizado);
            
        }



    }
}
