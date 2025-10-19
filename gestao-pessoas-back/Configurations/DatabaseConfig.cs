using gestao_pessoas_back.Data;
using Microsoft.EntityFrameworkCore;

namespace gestao_pessoas_back.Config
{
    public static class DatabaseConfig
    {
        public static void AddDatabaseConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));
            if (configuration == null) throw new ArgumentNullException(nameof(configuration));

            try
            {
                var connectionString = configuration.GetConnectionString("DefaultConnection");

                if (string.IsNullOrEmpty(connectionString))
                    throw new InvalidOperationException("Connection string 'DefaultConnection' não encontrada.");

                services.AddDbContext<ApplicationDbContext>(options =>
                {
                    options.UseNpgsql(connectionString); 

                    // Opcional: Habilitar logging detalhado em desenvolvimento
                    if (Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Development")
                    {
                        options.EnableSensitiveDataLogging();
                        options.EnableDetailedErrors();
                    }
                });

                Console.WriteLine("PostgreSQL database configuration completed successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error configuring PostgreSQL database: {ex.Message}");
                throw;
            }
        }
    }
}