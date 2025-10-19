using gestao_pessoas_back.Model;

namespace gestao_pessoas_back.Repository.Interfaces
{
    public interface IPessoaRepository
    {
        Task<PessoaModel> ObterPorIdAsync(Guid id);
        Task<PessoaModel> ObterPorCpfAsync(string cpf);
        Task<IEnumerable<PessoaModel>> ObterTodasAsync();
        Task<PessoaModel> CriarAsync(PessoaModel pessoa);
        Task<PessoaModel> AtualizarAsync(PessoaModel pessoa);
        Task<bool> ExcluirAsync(Guid id);
        Task<bool> CpfExisteAsync(string cpf, Guid? excluirId = null);
    }
}
