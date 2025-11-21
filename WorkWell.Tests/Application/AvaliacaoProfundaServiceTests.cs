using Xunit;
using Moq;
using WorkWell.Application.Services.AvaliacoesEmocionais;
using WorkWell.Domain.Interfaces.AvaliacoesEmocionais;
using WorkWell.Application.DTOs.AvaliacoesEmocionais;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WorkWell.Tests.Application
{
    public class AvaliacaoProfundaServiceTests
    {
        [Fact]
        public async Task CreateAsync_DeveRetornarId()
        {
            var repo = new Mock<IAvaliacaoProfundaRepository>();
            repo.Setup(r => r.AddAsync(It.IsAny<WorkWell.Domain.Entities.AvaliacoesEmocionais.AvaliacaoProfunda>()))
                .Returns(Task.CompletedTask)
                .Callback<WorkWell.Domain.Entities.AvaliacoesEmocionais.AvaliacaoProfunda>(a => a.Id = 8);

            var service = new AvaliacaoProfundaService(repo.Object);
            var dto = new AvaliacaoProfundaDto { FuncionarioId = 1, Gad7Score = 5, Phq9Score = 3, Interpretacao = "desc" };

            var id = await service.CreateAsync(dto);

            Assert.Equal(8, id);
        }

        [Fact]
        public async Task GetAllAsync_DeveRetornarAvaliacoes()
        {
            var repo = new Mock<IAvaliacaoProfundaRepository>();
            repo.Setup(r => r.GetAllAsync()).ReturnsAsync(new List<WorkWell.Domain.Entities.AvaliacoesEmocionais.AvaliacaoProfunda>
            {
                new() { Id = 1, FuncionarioId = 1, Gad7Score = 4, Phq9Score = 2, Interpretacao = "ok" }
            });

            var service = new AvaliacaoProfundaService(repo.Object);

            var result = await service.GetAllAsync();
            Assert.Single(result);
            Assert.Equal(1, result.First().Id);
        }
    }
}