using gestao_pessoas_back.Enum;
using System.ComponentModel.DataAnnotations;

namespace gestao_pessoas_back.Model
{
    public class PessoaModel: AuditoriaBaseModel
    {
        [Key]
        public Guid Id { get; set; }

        public string Nome { get; set; }

        [Required(ErrorMessage = "Sexo é obrigatório")]
        [EnumDataType(typeof(SexoEnum), ErrorMessage = "Sexo deve ser M, F ou O")]
        public SexoEnum Sexo { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        public DateTime DataNascimento { get; set; }
        public string Naturalidade { get; set; }
        public string Nacionalidade { get; set; }

        [StringLength(11)]
        public string CPF { get; set; }

    }
}
