namespace gestao_pessoas_back.Configurations
{
    public static class CorsConfig
    {
        private static readonly string _policyName = "AllowSpecificOrigins";

        public static void ConfigureCors(this WebApplicationBuilder builder)
        {
            builder.Services.AddCors(options =>
            {
                options.AddPolicy(_policyName,
                    builder => builder
                        .WithOrigins("http://localhost:3000", "https://localhost:3000") 
                        .AllowAnyMethod()
                        .AllowAnyHeader()
                        .AllowCredentials()); 
            });
        }

        public static void UseCorsConfiguration(this WebApplication app)
        {
            app.UseRouting();
            app.UseCors(_policyName); 
            app.UseAuthentication();
            app.UseAuthorization();
        }
    }
}
