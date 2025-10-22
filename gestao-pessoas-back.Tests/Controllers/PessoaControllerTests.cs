using gestao_pessoas_back.Controllers;
using gestao_pessoas_back.Reponse;
using gestao_pessoas_back.Requests.Pessoa;
using gestao_pessoas_back.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace gestao_pessoas_back.Tests.Controllers
{
    public class PessoaControllerTests
    {
        private readonly Mock<IPessoaService> _pessoaServiceMock;
        private readonly PessoaController _controller;

        public PessoaControllerTests()
        {
            _pessoaServiceMock = new Mock<IPessoaService>();
            _controller = new PessoaController(_pessoaServiceMock.Object);
        }

        [Fact]
        public async Task CriarPessoa_DeveRetornarOk_ComPessoaCriada()
        {
            
            var request = new CriarPessoaRequest
            {
                Nome = "Maria",
                CPF = "14356418689",
                DataNascimento = DateTime.Now
            };

            var expectedResponse = new PessoaResponse
            {
                Id = Guid.NewGuid(),
                Nome = "Maria",
                CPF = "14356418689",
                DataNascimento = request.DataNascimento
            };

            _pessoaServiceMock
                .Setup(s => s.CriarPessoaAsync(It.IsAny<CriarPessoaRequest>()))
                .ReturnsAsync(expectedResponse);

            var result = await _controller.CriarPessoa(request) as OkObjectResult;

            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);
            var pessoa = Assert.IsType<PessoaResponse>(result.Value);
            Assert.Equal("Maria", pessoa.Nome);
            Assert.Equal("14356418689", pessoa.CPF);
        }

        [Fact]
        public async Task ObterTodasPessoas_DeveRetornarListaDePessoas()
        {
            
            var lista = new List<PessoaResponse>
            {
                new PessoaResponse { Id = Guid.NewGuid(), Nome = "Maria" },
                new PessoaResponse { Id = Guid.NewGuid(), Nome = "João" }
            };

            _pessoaServiceMock
                .Setup(s => s.ObterTodasPessoasAsync())
                .ReturnsAsync(lista);

            
            var result = await _controller.ObterTodasPessoas() as OkObjectResult;

            
            Assert.NotNull(result);
            var pessoas = Assert.IsAssignableFrom<IEnumerable<PessoaResponse>>(result.Value);
            Assert.Collection(pessoas,
                p => Assert.Equal("Maria", p.Nome),
                p => Assert.Equal("João", p.Nome));
        }

        [Fact]
        public async Task ExcluirPessoa_DeveRetornarMensagemDeSucesso()
        {
            
            var id = Guid.NewGuid();
            _pessoaServiceMock
                .Setup(s => s.ExcluirPessoaAsync(It.IsAny<Guid>()))
                .ReturnsAsync(true);

            
            var result = await _controller.ExcluirPessoa(id) as OkObjectResult;

            
            Assert.NotNull(result);
            var response = Assert.IsType<SuccessResponse>(result.Value);
            Assert.Equal("Excluído com sucesso.", response.Message);
        }
    }
}
