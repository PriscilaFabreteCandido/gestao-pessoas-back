using gestao_pessoas_back.Data;
using gestao_pessoas_back.Model;
using gestao_pessoas_back.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace gestao_pessoas_back.Repositories
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly ApplicationDbContext _context;

        public UsuarioRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<UsuarioModel?> ObterPorEmailAsync(string email)
        {
            return await _context.Usuarios
                .FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task<UsuarioModel?> ObterPorIdAsync(Guid id)
        {
            return await _context.Usuarios
                .FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<UsuarioModel> CriarAsync(UsuarioModel usuario)
        {
            _context.Usuarios.Add(usuario);
            await _context.SaveChangesAsync();
            return usuario;
        }

        public async Task<bool> AtualizarAsync(UsuarioModel usuario)
        {
            var usuarioExistente = await ObterPorIdAsync(usuario.Id);
            if (usuarioExistente == null)
                return false;

            _context.Usuarios.Update(usuario);
            var affectedRows = await _context.SaveChangesAsync();
            return affectedRows > 0;
        }

        public async Task<bool> ExcluirAsync(Guid id)
        {
            var usuario = await ObterPorIdAsync(id);
            if (usuario == null)
                return false;

            _context.Usuarios.Remove(usuario);
            var affectedRows = await _context.SaveChangesAsync();
            return affectedRows > 0;
        }

        public async Task<List<UsuarioModel>> ObterTodosAsync()
        {
            return await _context.Usuarios
                .ToListAsync();
        }

        public async Task<bool> VerificarEmailExisteAsync(string email)
        {
            return await _context.Usuarios
                .AnyAsync(u => u.Email == email);
        }
    }
}