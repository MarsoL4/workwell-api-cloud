using Xunit;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;
using WorkWell.Application.Services.AvaliacoesEmocionais;
using WorkWell.Domain.Interfaces.AvaliacoesEmocionais;
using WorkWell.Application.DTOs.AvaliacoesEmocionais;

namespace WorkWell.Tests.Application
{
    public class PerfilEmocionalServiceTests
    {
        [Fact]
        public async Task CreateAsync_DeveRetornarId()
        {
            var mockRepo = new Mock<IPerfilEmocionalRepository>();
            mockRepo.Setup(r => r.AddAsync(It.IsAny<WorkWell.Domain.Entities.AvaliacoesEmocionais.PerfilEmocional>()))
                .Returns(Task.CompletedTask)
                .Callback<WorkWell.Domain.Entities.AvaliacoesEmocionais.PerfilEmocional>(p => p.Id = 7);

            var service = new PerfilEmocionalService(mockRepo.Object);
            var dto = new PerfilEmocionalDto { FuncionarioId = 33, HumorInicial = "OK", Rotina = "dev", PrincipaisEstressores = "n/a" };

            var id = await service.CreateAsync(dto);

            Assert.Equal(7, id);
        }

        [Fact]
        public async Task GetAllAsync_DeveRetornarPerfis()
        {
            var repo = new Mock<IPerfilEmocionalRepository>();
            repo.Setup(r => r.GetAllAsync()).ReturnsAsync(new List<WorkWell.Domain.Entities.AvaliacoesEmocionais.PerfilEmocional>
            {
                new() { Id = 1, FuncionarioId = 12, HumorInicial = "Bem", Rotina = "", PrincipaisEstressores = "" }
            });

            var service = new PerfilEmocionalService(repo.Object);
            var result = await service.GetAllAsync();

            Assert.Single(result);
            Assert.Equal(1, result.First().Id);
        }
    }
}