using AutoMapper;
using gestao_pessoas_back.Requests.Pessoa;
using gestao_pessoas_back.Reponse;
using gestao_pessoas_back.Model;

namespace gestao_pessoas_back.Mappings
{
    public class PessoaProfile : Profile
    {
        public PessoaProfile()
        {
            // Request para Entity
            CreateMap<CriarPessoaRequest, PessoaModel>();

            CreateMap<AtualizarPessoaRequest, PessoaModel>();

            // Entity para Response
            CreateMap<PessoaModel, PessoaResponse>();
        }
    }
}