using gestao_pessoas_back.Enum;

namespace gestao_pessoas_back.Requests.Usuario
{
    public class CriarUsuarioRequest
    {
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
        public UserRoleEnum Role { get; set; }
    }
}
