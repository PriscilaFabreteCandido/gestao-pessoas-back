using Microsoft.EntityFrameworkCore;
using gestao_pessoas_back.Model;

namespace gestao_pessoas_back.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
        public DbSet<PessoaModel> Pessoas { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configurar índice único para CPF
            modelBuilder.Entity<PessoaModel>()
                .HasIndex(p => p.CPF)
                .IsUnique();

            base.OnModelCreating(modelBuilder);
        }
    }
}
