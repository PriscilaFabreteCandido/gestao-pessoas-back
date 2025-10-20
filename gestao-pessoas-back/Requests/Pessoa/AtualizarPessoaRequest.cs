
using gestao_pessoas_back.Validations;
using System.ComponentModel.DataAnnotations;

namespace gestao_pessoas_back.Requests.Pessoa
{
    public class AtualizarPessoaRequest: CriarPessoaRequest
    {
        public Guid Id { get; set; }
    }
}
