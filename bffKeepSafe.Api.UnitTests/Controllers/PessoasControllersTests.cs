using bffKeepSafe.Api.Controllers;
using bffKeepSafe.Domain.Enums;
using bffKeepSafe.Domain.Interfaces.Pessoas;
using bffKeepSafe.Domain.Models.Pessoas;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace bffKeepSafe.Api.UnitTests.Controllers
{
    public class PessoasControllerTests
    {
        [Fact]
        public async Task Pessoas_ReturnsOkResultWithPessoasList()
        {
            // Arrange
            var pessoasResponses = new List<PessoasResponse>
            {
                new PessoasResponse { Id = 1, Nome = "Matheus" },
                new PessoasResponse { Id = 2, Nome = "Nicolau" }
            };
            var pessoasServiceMock = new Mock<IPessoasService>();
            pessoasServiceMock.Setup(service => service.GetAllPessoas()).ReturnsAsync(pessoasResponses);
            var controller = new PessoasController(pessoasServiceMock.Object);

            // Act
            var result = await controller.Pessoas();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var model = Assert.IsAssignableFrom<IEnumerable<PessoasResponse>>(okResult.Value);
            Assert.Equal(2, model.Count());
        }

        [Fact]
        public async Task PessoasCount_ReturnsOkResultWithPessoasCount()
        {
            // Arrange
            var pessoasCount = 5;
            var pessoasServiceMock = new Mock<IPessoasService>();
            pessoasServiceMock.Setup(service => service.GetPessoasCount()).ReturnsAsync(pessoasCount);
            var controller = new PessoasController(pessoasServiceMock.Object);

            // Act
            var result = await controller.PessoasCount();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var value = Assert.IsType<int>(okResult.Value);
            Assert.Equal(pessoasCount, value);
        }

        [Fact]
        public async Task PessoasMediaDeIdades_ReturnsOkResultWithMediaIdades()
        {
            // Arrange
            var mediaIdades = 25.5;
            var pessoasServiceMock = new Mock<IPessoasService>();
            pessoasServiceMock.Setup(service => service.GetIdadeMedia()).ReturnsAsync(mediaIdades);
            var controller = new PessoasController(pessoasServiceMock.Object);

            // Act
            var result = await controller.PessoasMediaDeIdades();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var value = Assert.IsType<double>(okResult.Value);
            Assert.Equal(mediaIdades, value);
        }

        [Fact]
        public async Task GetPessoasPorCidade_ReturnsOkResultWithFilteredPessoas()
        {
            // Arrange
            var cidade = "Santos";
            var pessoasResponses = new List<PessoasResponse>
            {
                new PessoasResponse { Id = 1, Nome = "Matheus", Cidade = "Santos" },
                new PessoasResponse { Id = 2, Nome = "Nicolau", Cidade = "Santos" },
                new PessoasResponse { Id = 3, Nome = "Maria", Cidade = "São Paulo" }
            };
            var pessoasServiceMock = new Mock<IPessoasService>();
            pessoasServiceMock.Setup(service => service.GetPessoasPorCidade(cidade)).ReturnsAsync(pessoasResponses.Where(p => p.Cidade == cidade));
            var controller = new PessoasController(pessoasServiceMock.Object);

            // Act
            var result = await controller.GetPessoasPorCidade(cidade);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var model = Assert.IsAssignableFrom<IEnumerable<PessoasResponse>>(okResult.Value);
            Assert.Equal(2, model.Count());
            Assert.True(model.All(p => p.Cidade == cidade));
        }

        [Fact]
        public async Task GetMenoresDeIdade_ReturnsOkResultWithMenoresDeIdade()
        {
            // Arrange
            var pessoasResponses = new List<PessoasResponse>
            {
                new PessoasResponse { Id = 1, Nome = "Matheus", Idade = 15 },
                new PessoasResponse { Id = 2, Nome = "Nicolau", Idade = 20 },
                new PessoasResponse { Id = 3, Nome = "Maria", Idade = 10 }
            };
            var pessoasServiceMock = new Mock<IPessoasService>();
            pessoasServiceMock.Setup(service => service.GetMenoresDeIdade()).ReturnsAsync(pessoasResponses.Where(p => p.Idade < 18));
            var controller = new PessoasController(pessoasServiceMock.Object);

            // Act
            var result = await controller.GetMenoresDeIdade();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var model = Assert.IsAssignableFrom<IEnumerable<PessoasResponse>>(okResult.Value);
            Assert.Equal(2, model.Count());
            Assert.True(model.All(p => p.Idade < 18));
        }

        [Fact]
        public async Task GetMaioresDeIdade_ReturnsOkResultWithMaioresDeIdade()
        {
            // Arrange
            var pessoasResponses = new List<PessoasResponse>
            {
                new PessoasResponse { Id = 1, Nome = "Matheus", Idade = 20 },
                new PessoasResponse { Id = 2, Nome = "Nicolau", Idade = 25 },
                new PessoasResponse { Id = 3, Nome = "Maria", Idade = 15 }
            };
            var pessoasServiceMock = new Mock<IPessoasService>();
            pessoasServiceMock.Setup(service => service.GetMaioresDeIdade()).ReturnsAsync(pessoasResponses.Where(p => p.Idade >= 18));
            var controller = new PessoasController(pessoasServiceMock.Object);

            // Act
            var result = await controller.GetMaioresDeIdade();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var model = Assert.IsAssignableFrom<IEnumerable<PessoasResponse>>(okResult.Value);
            Assert.Equal(2, model.Count());
            Assert.True(model.All(p => p.Idade >= 18));
        }

        [Fact]
        public async Task GetPessoasComMesmoNome_ReturnsOkResultWithFilteredPessoas()
        {
            // Arrange
            var nome = "Matheus";
            var pessoasResponses = new List<PessoasResponse>
            {
                new PessoasResponse { Id = 1, Nome = "Matheus" },
                new PessoasResponse { Id = 2, Nome = "Nicolau" },
                new PessoasResponse { Id = 3, Nome = "Maria" }
            };
            var pessoasServiceMock = new Mock<IPessoasService>();
            pessoasServiceMock.Setup(service => service.GetPessoasComMesmoNome(nome)).ReturnsAsync(pessoasResponses.Where(p => p.Nome == nome));
            var controller = new PessoasController(pessoasServiceMock.Object);

            // Act
            var result = await controller.GetPessoasComMesmoNome(nome);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var model = Assert.IsAssignableFrom<IEnumerable<PessoasResponse>>(okResult.Value);
            Assert.Single(model);
            Assert.True(model.All(p => p.Nome == nome));
        }

        [Fact]
        public async Task GetPorSexo_ReturnsOkResultWithFilteredPessoasBySexo()
        {
            // Arrange
            var sexo = TipoSexo.MASCULINO;
            var pessoasResponses = new List<PessoasResponse>
            {
                new PessoasResponse { Id = 1, Nome = "Matheus", Sexo = TipoSexo.MASCULINO },
                new PessoasResponse { Id = 2, Nome = "Nicolau", Sexo = TipoSexo.FEMININO },
                new PessoasResponse { Id = 3, Nome = "Maria", Sexo = TipoSexo.MASCULINO }
            };
            var pessoasServiceMock = new Mock<IPessoasService>();
            pessoasServiceMock.Setup(service => service.GetAllPorSexo(sexo)).ReturnsAsync(pessoasResponses.Where(p => p.Sexo == sexo));
            var controller = new PessoasController(pessoasServiceMock.Object);

            // Act
            var result = await controller.GetPorSexo(sexo);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var model = Assert.IsAssignableFrom<IEnumerable<PessoasResponse>>(okResult.Value);
            Assert.Equal(2, model.Count());
            Assert.True(model.All(p => p.Sexo == sexo));
        }

        [Fact]
        public async Task GetTotalPorSexo_ReturnsOkResultWithTotalPessoasBySexo()
        {
            // Arrange
            var sexo = TipoSexo.FEMININO;
            var total = 2;
            var pessoasResponses = new List<PessoasResponse>
            {
                new PessoasResponse { Id = 1, Nome = "Matheus", Sexo = TipoSexo.MASCULINO },
                new PessoasResponse { Id = 2, Nome = "Nicolau", Sexo = TipoSexo.FEMININO },
                new PessoasResponse { Id = 3, Nome = "Maria", Sexo = TipoSexo.FEMININO }
            };
            var pessoasServiceMock = new Mock<IPessoasService>();
            pessoasServiceMock.Setup(service => service.GetTotalPorSexo(sexo)).ReturnsAsync(total);
            var controller = new PessoasController(pessoasServiceMock.Object);

            // Act
            var result = await controller.GetTotalPorSexo(sexo);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var value = Assert.IsType<int>(okResult.Value);
            Assert.Equal(total, value);
        }
    }
}
