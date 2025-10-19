using gestao_pessoas_back.Service.Interfaces;
using gestao_pessoas_back.Service;

namespace gestao_pessoas_back.Configurations
{
    public static class ServicesConfig
    {
        public static void ConfigureServices(this WebApplicationBuilder builder)
        {
            builder.Services.AddHealthChecks();
            builder.Services.AddMemoryCache();


                builder.Services.AddScoped<IUsuarioService, UsuarioService>();
        }

    }
}
