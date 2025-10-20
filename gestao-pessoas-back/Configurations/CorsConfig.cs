namespace gestao_pessoas_back.Configurations
{
    public static class CorsConfig
    {
        private static readonly string _policyName = "AllowAll";

        public static void ConfigureCors(this WebApplicationBuilder builder)
        {
            builder.Services.AddCors(options =>
            {
                options.AddPolicy(_policyName, policy =>
                {
                    policy
                        .AllowAnyOrigin() 
                        .AllowAnyMethod() 
                        .AllowAnyHeader(); 
                });
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
