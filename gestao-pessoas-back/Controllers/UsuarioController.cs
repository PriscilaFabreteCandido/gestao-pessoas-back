using gestao_pessoas_back.Reponse;
using gestao_pessoas_back.Requests.Usuario;
using gestao_pessoas_back.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace gestao_pessoas_back.Controllers
{
    [ApiController]
    [Route("v1/[controller]")]
    public class UsuarioController : ControllerBase
    {
        private readonly UsuarioService _usuarioService;

        public UsuarioController(UsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }

        [HttpPut("alterar-senha")]
        [Authorize]
        public async Task<IActionResult> AlterarSenha([FromBody] AlterarSenhaRequest request)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            await _usuarioService.AlterarSenha(Guid.Parse(userId), request);
            return Ok();
        }

        [HttpPost("reset-senha/link/solicitacao")]
        public async Task<IActionResult> ResetarSenhaPorLinkRequest([FromBody] EsqueceuSenhaRequest request)
        {
            await _usuarioService.ResetarSenhaPorLinkRequest(request);
            return Ok(new SuccessResponse("Link de redefinição enviado por e-mail."));
        }

        [HttpPost("reset-senha/link")]
        public async Task<IActionResult> ResetarSenhaPorLink([FromBody] ResetSenhaPorLinkRequest request)
        {
            await _usuarioService.ResetarSenhaPorLink(request);
            return Ok(new SuccessResponse("Senha alterada com sucesso."));
        }

        [HttpPost("reset-senha/codigo/solicitacao")]
        public async Task<IActionResult> ResetarSenhaPorCodigoRequest([FromBody] EsqueceuSenhaRequest request)
        {
            await _usuarioService.ResetarSenhaPorCodigoRequest(request);
            return Ok(new SuccessResponse("Código enviado para o e-mail."));
        }

        [HttpPost("reset-senha/codigo")]
        public async Task<IActionResult> ResetarSenhaPorCodigo([FromBody] ResetSenhaPorCodigoRequest request)
        {
            await _usuarioService.ResetarSenhaPorCodigo(request);
            return Ok(new SuccessResponse("Senha alterada com sucesso."));
        }
    }
}
