using gestao_pessoas_back.Enum;
using System.ComponentModel.DataAnnotations;

namespace gestao_pessoas_back.Model
{
    public class UsuarioModel: AuditoriaBaseModel
    {
        public Guid Id { get; set; }
        public string NomeCompleto { get; set; }
        public string Email { get; set; }
        public string SenhaHash { get; set; }
        public UserRoleEnum Role { get; set; }

    }
}
