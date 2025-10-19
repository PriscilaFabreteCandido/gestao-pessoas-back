using gestao_pessoas_back.Model;

namespace gestao_pessoas_back.Repository.Interfaces
{
    public interface IUsuarioRepository
    {
        Task<UsuarioModel?> ObterPorEmailAsync(string email);
        Task<UsuarioModel?> ObterPorIdAsync(Guid id);
        Task<UsuarioModel> CriarAsync(UsuarioModel usuario);
        Task<bool> AtualizarAsync(UsuarioModel usuario);
        Task<bool> ExcluirAsync(Guid id);
        Task<List<UsuarioModel>> ObterTodosAsync();
        Task<bool> VerificarEmailExisteAsync(string email);
    }
}
