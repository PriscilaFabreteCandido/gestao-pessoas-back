using gestao_pessoas_back.Enum;

namespace gestao_pessoas_back.Reponse
{
    public class PessoaResponse: AuditoriaResponse
    {
        public Guid Id { get; set; }

        public string Nome { get; set; }

        public SexoEnum? Sexo { get; set; }

        public string Email { get; set; }

        public DateTime DataNascimento { get; set; }
        public string Naturalidade { get; set; }
        public string Nacionalidade { get; set; }

        public string CPF { get; set; }

        public string CPFFormatado
        {
            get
            {
                if (string.IsNullOrEmpty(CPF) || CPF.Length != 11)
                    return CPF;

                return $"{CPF.Substring(0, 3)}.{CPF.Substring(3, 3)}.{CPF.Substring(6, 3)}-{CPF.Substring(9, 2)}";
            }
        }
    }
}
