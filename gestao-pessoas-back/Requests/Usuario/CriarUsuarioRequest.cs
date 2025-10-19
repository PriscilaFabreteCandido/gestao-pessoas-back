using gestao_pessoas_back.Enum;
using System.ComponentModel.DataAnnotations;

namespace gestao_pessoas_back.Requests.Usuario
{
    public class CriarUsuarioRequest
    {
        public string Nome { get; set; }
        [EmailAddress(ErrorMessage = "E-mail em formato inválido")]
        public string Email { get; set; }
        public string Senha { get; set; }
        public UserRoleEnum Role { get; set; }
    }
}
