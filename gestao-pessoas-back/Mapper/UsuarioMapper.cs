using AutoMapper;
using gestao_pessoas_back.Reponse;
using gestao_pessoas_back.Requests.Usuario;

namespace gestao_pessoas_back.Mapper
{
  
    public class UsuarioProfile : Profile
    {
        public UsuarioProfile()
        {
           
            CreateMap<CriarUsuarioRequest, UsuarioResponse>();

        }
    }
}
