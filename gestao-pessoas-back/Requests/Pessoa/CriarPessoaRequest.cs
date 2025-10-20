using gestao_pessoas_back.Enum;
using gestao_pessoas_back.Validations;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace gestao_pessoas_back.Requests.Pessoa
{
    public class CriarPessoaRequest
    {
        [Required(ErrorMessage = "O nome é obrigatório")]
        public string Nome { get; set; }

      
        [EnumDataType(typeof(SexoEnum), ErrorMessage = "Sexo deve ser M, F ou O")]
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public SexoEnum? Sexo { get; set; } = null;

        [EmailAddress(ErrorMessage = "E-mail em formato inválido")]
        public string? Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "A data de nascimento é obrigatória")]
        public DateTime DataNascimento { get; set; }

        public string? Naturalidade { get; set; } = string.Empty;

        public string? Nacionalidade { get; set; } = string.Empty;

        [Required(ErrorMessage = "O CPF é obrigatório")]
        [Cpf(ErrorMessage = "CPF inválido")]
        public string CPF
        {
            get => _cpf;
            set => _cpf = LimparCpf(value);
        }

        private string _cpf;

        private static string LimparCpf(string cpf)
        {
            if (string.IsNullOrWhiteSpace(cpf))
                return cpf;

            return System.Text.RegularExpressions.Regex.Replace(cpf, @"[^\d]", "");
        }
    }

   
}