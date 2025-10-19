using gestao_pessoas_back.Reponse;
using gestao_pessoas_back.Requests.Pessoa;

namespace gestao_pessoas_back.Service.Interfaces
{
    public interface IPessoaService
    {
        Task<PessoaResponse> CriarPessoaAsync(CriarPessoaRequest request);
        Task<PessoaResponse> AtualizarPessoaAsync(Guid id, AtualizarPessoaRequest request);
        Task<bool> ExcluirPessoaAsync(Guid id);
        Task<PessoaResponse> ObterPessoaPorIdAsync(Guid id);
        Task<IEnumerable<PessoaResponse>> ObterTodasPessoasAsync();
        Task<PessoaResponse> ObterPessoaPorCpfAsync(string cpf);
    }
}
