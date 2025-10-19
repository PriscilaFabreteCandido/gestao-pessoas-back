using gestao_pessoas_back.Service.Interfaces;
using gestao_pessoas_back.Service;
using gestao_pessoas_back.Services;
using gestao_pessoas_back.Repository.Interfaces;
using gestao_pessoas_back.Repositories;
using gestao_pessoas_back.Repository;

namespace gestao_pessoas_back.Configurations
{
    public static class ServicesConfig
    {
        public static void ConfigureServices(this WebApplicationBuilder builder)
        {
            builder.Services.AddHealthChecks();
            builder.Services.AddMemoryCache();

            builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();
            builder.Services.AddScoped<IPessoaRepository, PessoaRepository>();

            builder.Services.AddScoped<IPessoaService, PessoaService>();
            builder.Services.AddScoped<IUsuarioService, UsuarioService>();
            builder.Services.AddScoped<JwtService>();
        }

    }
}
