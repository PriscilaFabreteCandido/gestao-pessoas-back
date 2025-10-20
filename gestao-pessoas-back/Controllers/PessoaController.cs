using gestao_pessoas_back.Reponse;
using gestao_pessoas_back.Requests.Pessoa;
using gestao_pessoas_back.Service.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace gestao_pessoas_back.Controllers
{
    [ApiController]
    [Authorize()]
    [Route("v1/[controller]")]
    public class PessoaController : ControllerBase
    {
        private readonly IPessoaService _pessoaService;

        public PessoaController(IPessoaService pessoaService)
        {
            _pessoaService = pessoaService;
        }

        [HttpPost("criar")]
        public async Task<IActionResult> CriarPessoa([FromBody] CriarPessoaRequest request)
        {
            
            var pessoa = await _pessoaService.CriarPessoaAsync(request);
            return Ok(pessoa);
        }

        [HttpPut("atualizar/{id}")]
        public async Task<IActionResult> AtualizarPessoa(Guid id, [FromBody] AtualizarPessoaRequest request)
        {
            var pessoa = await _pessoaService.AtualizarPessoaAsync(id, request);
            return Ok(pessoa);
        }

        [HttpDelete("excluir/{id}")]
        public async Task<IActionResult> ExcluirPessoa(Guid id)
        {

            var sucesso = await _pessoaService.ExcluirPessoaAsync(id);
            return Ok(new SuccessResponse("Excluído com sucesso."));
        }

        [HttpGet("obter-por-id/{id}")]
        public async Task<IActionResult> ObterPessoaPorId(Guid id)
        {
            var pessoa = await _pessoaService.ObterPessoaPorIdAsync(id);
            return Ok(pessoa);
        }

        [HttpGet("obter-todos")]
        public async Task<IActionResult> ObterTodasPessoas()
        {
            
            var pessoas = await _pessoaService.ObterTodasPessoasAsync();
            return Ok(pessoas);
            
        }

        [HttpGet("obter-por-cpf/{cpf}")]
        public async Task<IActionResult> ObterPessoaPorCpf(string cpf)
        {
            
            var pessoa = await _pessoaService.ObterPessoaPorCpfAsync(cpf);
            return Ok(pessoa);
        }
    }
}
