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
        public DbSet<UsuarioModel> Usuarios { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
           
            modelBuilder.Entity<PessoaModel>(entity =>
            {
                entity.HasKey(a => a.Id);
                entity.Property(a => a.Id).ValueGeneratedOnAdd();
                entity.HasIndex(t => t.CPF).IsUnique();// // Configurar índice único para CPF

            });

            modelBuilder.Entity<UsuarioModel>(entity =>
            {
                entity.HasKey(a => a.Id);
                entity.Property(a => a.Id).ValueGeneratedOnAdd();
                entity.HasIndex(t => t.Email).IsUnique();// // Configurar índice único para Email

            });

            base.OnModelCreating(modelBuilder);
        }
    }
}
