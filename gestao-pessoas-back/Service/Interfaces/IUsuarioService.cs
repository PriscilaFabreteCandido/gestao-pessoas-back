using gestao_pessoas_back.Reponse;
using gestao_pessoas_back.Requests.Usuario;

namespace gestao_pessoas_back.Service.Interfaces
{
    public interface IUsuarioService
    {
        public  Task<UsuarioResponse?> ObterInformacoesUsuario();
    }
}
