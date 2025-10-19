using gestao_pessoas_back.Requests.Usuario;

namespace gestao_pessoas_back.Service.Interfaces
{
    public interface IUsuarioService
    {
        Task AlterarSenha(Guid userId, AlterarSenhaRequest request);
        Task ResetarSenhaPorLinkRequest(EsqueceuSenhaRequest request);
        Task ResetarSenhaPorLink(ResetSenhaPorLinkRequest request);
        Task ResetarSenhaPorCodigoRequest(EsqueceuSenhaRequest request);
        Task ResetarSenhaPorCodigo(ResetSenhaPorCodigoRequest request);
    }
}
