using AutoMapper;
using gestao_pessoas_back.Model;
using gestao_pessoas_back.Reponse;
using gestao_pessoas_back.Requests.Pessoa;
using gestao_pessoas_back.Requests.Usuario;

namespace gestao_pessoas_back.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile() 
        {
            CreateMap<CriarPessoaRequest, PessoaModel>();
            CreateMap<AtualizarPessoaRequest, PessoaModel>();
            CreateMap<PessoaModel, PessoaResponse>();

            CreateMap<CriarUsuarioRequest, UsuarioModel>()
                .ForMember(dest => dest.SenhaHash, opt => opt.Ignore());

            CreateMap<UsuarioModel, UsuarioResponse>();
        } 
    }
}
