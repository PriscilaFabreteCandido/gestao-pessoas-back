using gestao_pessoas_back.Enum;

namespace gestao_pessoas_back.Reponse
{
    public class UsuarioResponse
    {
        public Guid Id { get; set; }
        public string NomeCompleto { get; set; }
        public string Email { get; set; }
        public string SenhaHash { get; set; }
        public UserRoleEnum Role { get; set; }
    }
}
