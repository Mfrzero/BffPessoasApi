using bffKeepSafe.application.Services.Pessoas;
using bffKeepSafe.Domain.Enums;
using bffKeepSafe.Domain.Interfaces.Api;
using bffKeepSafe.Domain.Models.Pessoas;
using Moq;
using Xunit;

namespace bffKeepSafe.Infrastructure.UnitTests.Pessoas
{
    public class PessoasServiceTests
    {
        [Fact]
        public async Task GetAllPessoas_ReturnsPessoasResponseList()
        {
            // Arrange
            var apiPessoasServiceMock = new Mock<IApiPessoasService>();
            apiPessoasServiceMock.Setup(service => service.GetApiResponseAsync()).ReturnsAsync(new List<PessoasResponse>());
            var service = new PessoasService(apiPessoasServiceMock.Object);

            // Act
            var result = await service.GetAllPessoas();

            // Assert
            Assert.NotNull(result);
            Assert.IsAssignableFrom<IEnumerable<PessoasResponse>>(result);
        }

        [Fact]
        public async Task GetPessoasCount_ReturnsPessoasCount()
        {
            // Arrange
            var apiPessoasServiceMock = new Mock<IApiPessoasService>();
            apiPessoasServiceMock.Setup(service => service.GetApiResponseAsync()).ReturnsAsync(new List<PessoasResponse>());
            var service = new PessoasService(apiPessoasServiceMock.Object);

            // Act
            var result = await service.GetPessoasCount();

            // Assert
            Assert.Equal(0, result);
        }

        [Fact]
        public async Task GetIdadeMedia_ReturnsAverageAge()
        {
            // Arrange
            var pessoasResponses = new List<PessoasResponse>
            {
                new PessoasResponse { Idade = 20 },
                new PessoasResponse { Idade = 25 },
                new PessoasResponse { Idade = 30 }
            };
            var apiPessoasServiceMock = new Mock<IApiPessoasService>();
            apiPessoasServiceMock.Setup(service => service.GetApiResponseAsync()).ReturnsAsync(pessoasResponses);
            var service = new PessoasService(apiPessoasServiceMock.Object);

            // Act
            var result = await service.GetIdadeMedia();

            // Assert
            Assert.Equal(25, result);
        }

        [Fact]
        public async Task GetPessoasPorCidade_ReturnsPessoasPorCidade()
        {
            // Arrange
            var cidade = "SANTOS";
            var pessoasResponses = new List<PessoasResponse>
            {
                new PessoasResponse { Id = 1, Cidade = "SANTOS" },
                new PessoasResponse { Id = 2, Cidade = "SAO VICENTE" },
                new PessoasResponse { Id = 3, Cidade = "SANTOS" }
            };
            var apiPessoasServiceMock = new Mock<IApiPessoasService>();
            apiPessoasServiceMock.Setup(service => service.GetApiResponseAsync()).ReturnsAsync(pessoasResponses);
            var service = new PessoasService(apiPessoasServiceMock.Object);

            // Act
            var result = await service.GetPessoasPorCidade(cidade);

            // Assert
            Assert.NotNull(result);
            Assert.IsAssignableFrom<IEnumerable<PessoasResponse>>(result);
            Assert.Equal(2, result.Count());
            Assert.All(result, pessoa => Assert.Equal(cidade, pessoa.Cidade));
        }

        [Fact]
        public async Task GetMenoresDeIdade_ReturnsMenoresDeIdade()
        {
            // Arrange
            var pessoasResponses = new List<PessoasResponse>
            {
                new PessoasResponse { Id = 1, Idade = 20 },
                new PessoasResponse { Id = 2, Idade = 15 },
                new PessoasResponse { Id = 3, Idade = 30 }
            };
            var apiPessoasServiceMock = new Mock<IApiPessoasService>();
            apiPessoasServiceMock.Setup(service => service.GetApiResponseAsync()).ReturnsAsync(pessoasResponses);
            var service = new PessoasService(apiPessoasServiceMock.Object);

            // Act
            var result = await service.GetMenoresDeIdade();

            // Assert
            Assert.NotNull(result);
            Assert.IsAssignableFrom<IEnumerable<PessoasResponse>>(result);
            Assert.Single(result);
            Assert.All(result, pessoa => Assert.True(pessoa.Idade < 18));
        }

        [Fact]
        public async Task GetMaioresDeIdade_ReturnsMaioresDeIdade()
        {
            // Arrange
            var pessoasResponses = new List<PessoasResponse>
            {
                new PessoasResponse { Id = 1, Idade = 20 },
                new PessoasResponse { Id = 2, Idade = 15 },
                new PessoasResponse { Id = 3, Idade = 30 }
            };
            var apiPessoasServiceMock = new Mock<IApiPessoasService>();
            apiPessoasServiceMock.Setup(service => service.GetApiResponseAsync()).ReturnsAsync(pessoasResponses);
            var service = new PessoasService(apiPessoasServiceMock.Object);

            // Act
            var result = await service.GetMaioresDeIdade();

            // Assert
            Assert.NotNull(result);
            Assert.IsAssignableFrom<IEnumerable<PessoasResponse>>(result);
            Assert.Equal(2, result.Count());
            Assert.All(result, pessoa => Assert.True(pessoa.Idade >= 18));
        }
        [Fact]
        public async Task GetPessoasComMesmoNome_ReturnsPessoasWithSameName()
        {
            // Arrange
            var nome = "Matheus";
            var pessoasResponses = new List<PessoasResponse>
            {
                new PessoasResponse { Id = 1, Nome = "Matheus" },
                new PessoasResponse { Id = 2, Nome = "Nicolau" },
                new PessoasResponse { Id = 3, Nome = "Matheus" }
            };
            var apiPessoasServiceMock = new Mock<IApiPessoasService>();
            apiPessoasServiceMock.Setup(service => service.GetApiResponseAsync()).ReturnsAsync(pessoasResponses);
            var service = new PessoasService(apiPessoasServiceMock.Object);

            // Act
            var result = await service.GetPessoasComMesmoNome(nome);

            // Assert
            Assert.NotNull(result);
            Assert.IsAssignableFrom<IEnumerable<PessoasResponse>>(result);
            Assert.Equal(2, result.Count());
            Assert.All(result, pessoa => Assert.Equal(nome, pessoa.Nome));
        }

        [Fact]
        public async Task GetAllPorSexo_ReturnsPessoasBySex()
        {
            // Arrange
            var sexo = TipoSexo.MASCULINO;
            var pessoasResponses = new List<PessoasResponse>
            {
                new PessoasResponse { Id = 1, Sexo = TipoSexo.MASCULINO },
                new PessoasResponse { Id = 2, Sexo = TipoSexo.FEMININO },
                new PessoasResponse { Id = 3, Sexo = TipoSexo.MASCULINO }
            };
            var apiPessoasServiceMock = new Mock<IApiPessoasService>();
            apiPessoasServiceMock.Setup(service => service.GetApiResponseAsync()).ReturnsAsync(pessoasResponses);
            var service = new PessoasService(apiPessoasServiceMock.Object);

            // Act
            var result = await service.GetAllPorSexo(sexo);

            // Assert
            Assert.NotNull(result);
            Assert.IsAssignableFrom<IEnumerable<PessoasResponse>>(result);
            Assert.Equal(2, result.Count());
            Assert.All(result, pessoa => Assert.Equal(sexo, pessoa.Sexo));
        }

        [Fact]
        public async Task GetTotalPorSexo_ReturnsTotalPessoasBySex()
        {
            // Arrange
            var sexo = TipoSexo.FEMININO;
            var pessoasResponses = new List<PessoasResponse>
            {
                new PessoasResponse { Id = 1, Sexo = TipoSexo.MASCULINO },
                new PessoasResponse { Id = 2, Sexo = TipoSexo.FEMININO },
                new PessoasResponse { Id = 3, Sexo = TipoSexo.FEMININO }
            };
            var apiPessoasServiceMock = new Mock<IApiPessoasService>();
            apiPessoasServiceMock.Setup(service => service.GetApiResponseAsync()).ReturnsAsync(pessoasResponses);
            var service = new PessoasService(apiPessoasServiceMock.Object);

            // Act
            var result = await service.GetTotalPorSexo(sexo);

            // Assert
            Assert.Equal(2, result);
        }
    }
}
