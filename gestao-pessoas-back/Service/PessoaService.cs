using AutoMapper;
using gestao_pessoas_back.Model;
using gestao_pessoas_back.Reponse;
using gestao_pessoas_back.Repository.Interfaces;
using gestao_pessoas_back.Requests.Pessoa;
using gestao_pessoas_back.Service.Interfaces;

namespace gestao_pessoas_back.Services
{
    public class PessoaService : IPessoaService
    {
        private readonly IPessoaRepository _pessoaRepository;
        private readonly ILogger<PessoaService> _logger;
        private readonly IMapper _mapper;

        public PessoaService(
            IPessoaRepository pessoaRepository,
            ILogger<PessoaService> logger,
            IMapper mapper)
        {
            _pessoaRepository = pessoaRepository;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<PessoaResponse> CriarPessoaAsync(CriarPessoaRequest request)
        {
            try
            {
                // Validar unicidade do CPF
                if (await _pessoaRepository.CpfExisteAsync(request.CPF))
                    throw new InvalidOperationException("Já existe uma pessoa cadastrada com este CPF");

                var pessoa = _mapper.Map<PessoaModel>(request);
                var pessoaCriada = await _pessoaRepository.CriarAsync(pessoa);

                _logger.LogInformation("Pessoa criada com ID: {Id}", pessoaCriada.Id);
                return _mapper.Map<PessoaResponse>(pessoaCriada);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao criar pessoa");
                throw new Exception("Erro ao criar pessoa");
            }
        }

        public async Task<PessoaResponse> AtualizarPessoaAsync(Guid id, AtualizarPessoaRequest request)
        {
            try
            {
                var pessoaExistente = await _pessoaRepository.ObterPorIdAsync(id);
                if (pessoaExistente == null)
                    throw new KeyNotFoundException("Pessoa não encontrada");

                // Validar unicidade do CPF (excluindo a própria pessoa)
                if (await _pessoaRepository.CpfExisteAsync(request.CPF, id))
                    throw new InvalidOperationException("Já existe outra pessoa cadastrada com este CPF");

                _mapper.Map(request, pessoaExistente);
                var resultado = await _pessoaRepository.AtualizarAsync(pessoaExistente);

                _logger.LogInformation("Pessoa atualizada com ID: {Id}", id);
                return _mapper.Map<PessoaResponse>(resultado);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao atualizar pessoa com ID: {Id}", id);
                throw new Exception("Erro ao atualizar pessoa");
            }
        }

        public async Task<bool> ExcluirPessoaAsync(Guid id)
        {
            try
            {
                var resultado = await _pessoaRepository.ExcluirAsync(id);

                if (resultado)
                    _logger.LogInformation("Pessoa excluída com ID: {Id}", id);
                else
                    _logger.LogWarning("Tentativa de excluir pessoa não encontrada com ID: {Id}", id);

                return resultado;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao excluir pessoa com ID: {Id}", id);
                throw new Exception("Erro ao excluir pessoa");
            }
        }

        public async Task<PessoaResponse> ObterPessoaPorIdAsync(Guid id)
        {
            var pessoa = await _pessoaRepository.ObterPorIdAsync(id);
            return _mapper.Map<PessoaResponse>(pessoa);
        }

        public async Task<IEnumerable<PessoaResponse>> ObterTodasPessoasAsync()
        {
            var pessoas = await _pessoaRepository.ObterTodasAsync();
            return _mapper.Map<IEnumerable<PessoaResponse>>(pessoas);
        }

        public async Task<PessoaResponse> ObterPessoaPorCpfAsync(string cpf)
        {
            var cpfLimpo = System.Text.RegularExpressions.Regex.Replace(cpf, @"[^\d]", "");
            var pessoa = await _pessoaRepository.ObterPorCpfAsync(cpfLimpo);
            return _mapper.Map<PessoaResponse>(pessoa);
        }
    }
}