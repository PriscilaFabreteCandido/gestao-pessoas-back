using gestao_pessoas_back.Settings;

namespace gestao_pessoas_back.Configurations
{
    public static class SettingsConfig
    {
        public static void ConfigureSettings(this WebApplicationBuilder builder)
        {

            var jwtSettings = builder.Configuration.GetSection("JwtSettings").Get<JwtSettings>();
            
            builder.Services.AddSingleton(jwtSettings);

        }
    }
}
