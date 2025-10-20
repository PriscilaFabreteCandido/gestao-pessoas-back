using gestao_pessoas_back.Enum;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace gestao_pessoas_back.Model
{
    public class PessoaModel: AuditoriaBaseModel
    {
        [Key]
        public Guid Id { get; set; }

        public string Nome { get; set; }

        [JsonConverter(typeof(JsonStringEnumConverter))]
        public SexoEnum? Sexo { get; set; }

        [EmailAddress]
        public string? Email { get; set; }

        public DateTime DataNascimento { get; set; }
        public string? Naturalidade { get; set; }
        public string? Nacionalidade { get; set; }

        [StringLength(11)]
        public string CPF { get; set; }

    }
}
