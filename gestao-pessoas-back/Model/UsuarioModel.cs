using gestao_pessoas_back.Enum;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace gestao_pessoas_back.Model
{
    public class UsuarioModel: IdentityUser
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string SenhaHash { get; set; }
        public UserRoleEnum Role { get; set; }

    }
}
