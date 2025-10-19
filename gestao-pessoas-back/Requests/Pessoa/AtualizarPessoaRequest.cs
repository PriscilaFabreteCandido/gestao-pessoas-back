
using gestao_pessoas_back.Validations;
using System.ComponentModel.DataAnnotations;

namespace gestao_pessoas_back.Requests.Pessoa
{
    public class AtualizarPessoaRequest
    {
        [Required(ErrorMessage = "O nome é obrigatório")]
        public string Nome { get; set; }

        [StringLength(1, ErrorMessage = "Sexo deve ser M, F ou O")]
        [RegularExpression(@"^[MFO]$", ErrorMessage = "Sexo deve ser M (Masculino), F (Feminino) ou O (Outro)")]
        public string Sexo { get; set; }

        [EmailAddress(ErrorMessage = "E-mail em formato inválido")]
        public string Email { get; set; }

        [Required(ErrorMessage = "A data de nascimento é obrigatória")]
        [DataType(DataType.Date)]
        public DateTime DataNascimento { get; set; }

        public string Naturalidade { get; set; }

        public string Nacionalidade { get; set; }

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
