using gestao_pessoas_back.Enum;

namespace gestao_pessoas_back.Reponse
{
    public class PessoaResponse
    {
        public Guid Id { get; set; }

        public string Nome { get; set; }

        public SexoEnum? Sexo { get; set; }

        public string Email { get; set; }

        public DateTime DataNascimento { get; set; }
        public string Naturalidade { get; set; }
        public string Nacionalidade { get; set; }

        public string CPF { get; set; }

    }
}
