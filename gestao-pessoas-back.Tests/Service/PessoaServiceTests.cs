using Moq;
using AutoMapper;
using Microsoft.Extensions.Logging;
using gestao_pessoas_back.Service.Interfaces;
using gestao_pessoas_back.Services;
using gestao_pessoas_back.Repository.Interfaces;
using gestao_pessoas_back.Requests.Pessoa;
using gestao_pessoas_back.Model;
using gestao_pessoas_back.Reponse;


namespace gestao_pessoas_back.Tests.Services
{
    public class PessoaServiceTests
    {
        private readonly Mock<IPessoaRepository> _repoMock;
        private readonly Mock<ILogger<PessoaService>> _loggerMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly PessoaService _service;

        public PessoaServiceTests()
        {
            _repoMock = new Mock<IPessoaRepository>();
            _loggerMock = new Mock<ILogger<PessoaService>>();
            _mapperMock = new Mock<IMapper>();
            _service = new PessoaService(_repoMock.Object, _loggerMock.Object, _mapperMock.Object);
        }

        [Fact]
        public async Task CriarPessoaAsync_DeveCriarPessoa_ComSucesso()
        {
           
            var request = new CriarPessoaRequest
            {
                Nome = "Maria",
                CPF = "14356418689",
                DataNascimento = DateTime.Now
            };
            var pessoaModel = new PessoaModel
            {
                Id = Guid.NewGuid(),
                Nome = "Maria",
                CPF = "14356418689",
                DataNascimento = request.DataNascimento
            };
            var pessoaResponse = new PessoaResponse
            {
                Id = pessoaModel.Id,
                Nome = "Maria",
                CPF = "14356418689",
                DataNascimento = request.DataNascimento
            };

            _repoMock.Setup(r => r.CpfExisteAsync(It.IsAny<string>(), It.IsAny<Guid?>()))
                    .ReturnsAsync(false);
            _mapperMock.Setup(m => m.Map<PessoaModel>(request))
                      .Returns(pessoaModel);
            _repoMock.Setup(r => r.CriarAsync(It.IsAny<PessoaModel>()))
                    .ReturnsAsync(pessoaModel);
            _mapperMock.Setup(m => m.Map<PessoaResponse>(pessoaModel))
                      .Returns(pessoaResponse);

            // Act
            var result = await _service.CriarPessoaAsync(request);

            // Assert - CORREÇÃO FINAL: Mudar "Ana" para "Maria"
            Assert.Equal("Maria", result.Nome); // ✅ LINHA 52 CORRIGIDA
            _repoMock.Verify(r => r.CriarAsync(It.IsAny<PessoaModel>()), Times.Once);
        }

        [Fact]
        public async Task CriarPessoaAsync_DeveLancarExcecao_SeCpfJaExistir()
        {
            
            var request = new CriarPessoaRequest
            {
                Nome = "Maria",
                CPF = "14356418689",
                DataNascimento = DateTime.Now
            };

            _repoMock.Setup(r => r.CpfExisteAsync(It.IsAny<string>(), It.IsAny<Guid?>()))
                    .ReturnsAsync(true);

            
            _mapperMock.Setup(m => m.Map<PessoaModel>(It.IsAny<CriarPessoaRequest>()))
                      .Returns(new PessoaModel());

            
            var exception = await Assert.ThrowsAsync<Exception>(() => _service.CriarPessoaAsync(request));
            Assert.Equal("Já existe uma pessoa cadastrada com este CPF", exception.Message);
        }

        [Fact]
        public async Task ObterTodasPessoasAsync_DeveRetornarLista()
        {
           
            var listaModel = new List<PessoaModel>
            {
                new PessoaModel { Nome = "Ana", Id = Guid.NewGuid() }
            };

            var listaResponse = new List<PessoaResponse>
            {
                new PessoaResponse { Nome = "Ana", Id = listaModel[0].Id }
            };

            _repoMock.Setup(r => r.ObterTodasAsync()).ReturnsAsync(listaModel);
            _mapperMock.Setup(m => m.Map<IEnumerable<PessoaResponse>>(listaModel)).Returns(listaResponse);

            var result = await _service.ObterTodasPessoasAsync();

            Assert.Single(result);
            Assert.Equal("Ana", ((List<PessoaResponse>)result)[0].Nome);
        }
    }
}