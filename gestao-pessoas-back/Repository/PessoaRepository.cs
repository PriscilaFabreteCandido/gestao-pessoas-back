using Microsoft.EntityFrameworkCore;
using gestao_pessoas_back.Repository.Interfaces;
using gestao_pessoas_back.Data;
using gestao_pessoas_back.Model;

namespace gestao_pessoas_back.Repository
{
    public class PessoaRepository : IPessoaRepository
    {
        private readonly ApplicationDbContext _context;

        public PessoaRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<PessoaModel> ObterPorIdAsync(Guid id)
        {
            return await _context.Pessoas.FindAsync(id);
        }

        public async Task<PessoaModel> ObterPorCpfAsync(string cpf)
        {
            return await _context.Pessoas
                .FirstOrDefaultAsync(p => p.CPF == cpf);
        }

        public async Task<IEnumerable<PessoaModel>> ObterTodasAsync()
        {
            return await _context.Pessoas
                .OrderBy(p => p.Nome)
                .ToListAsync();
        }

        public async Task<PessoaModel> CriarAsync(PessoaModel pessoa)
        {
            _context.Pessoas.Add(pessoa);
            await _context.SaveChangesAsync();
            return pessoa;
        }

        public async Task<PessoaModel> AtualizarAsync(PessoaModel pessoa)
        {
            _context.Pessoas.Update(pessoa);
            await _context.SaveChangesAsync();
            return pessoa;
        }

        public async Task<bool> ExcluirAsync(Guid id)
        {
            var pessoa = await ObterPorIdAsync(id);
            if (pessoa == null)
                return false;

            _context.Pessoas.Remove(pessoa);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> CpfExisteAsync(string cpf, Guid? excluirId = null)
        {
            var query = _context.Pessoas.Where(p => p.CPF == cpf);

            if (excluirId.HasValue)
                query = query.Where(p => p.Id != excluirId.Value);

            return await query.AnyAsync();
        }
    }
}