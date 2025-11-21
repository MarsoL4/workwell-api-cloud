using Xunit;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;
using WorkWell.Application.Services.AvaliacoesEmocionais;
using WorkWell.Domain.Interfaces.AvaliacoesEmocionais;
using WorkWell.Application.DTOs.AvaliacoesEmocionais;

namespace WorkWell.Tests.Application
{
    public class MoodCheckServiceTests
    {
        [Fact]
        public async Task CreateAsync_DeveRetornarIdNovo()
        {
            var mockRepo = new Mock<IMoodCheckRepository>();
            mockRepo.Setup(r => r.AddAsync(It.IsAny<WorkWell.Domain.Entities.AvaliacoesEmocionais.MoodCheck>()))
                .Returns(Task.CompletedTask)
                .Callback<WorkWell.Domain.Entities.AvaliacoesEmocionais.MoodCheck>(m => m.Id = 5);

            var service = new MoodCheckService(mockRepo.Object);
            var dto = new MoodCheckDto { FuncionarioId = 10, Humor = 3, Produtivo = true, Estressado = false, DormiuBem = true };

            var id = await service.CreateAsync(dto);

            Assert.Equal(5, id);
        }

        [Fact]
        public async Task GetAllAsync_DeveRetornarChecks()
        {
            var repo = new Mock<IMoodCheckRepository>();
            repo.Setup(r => r.GetAllAsync()).ReturnsAsync(new List<WorkWell.Domain.Entities.AvaliacoesEmocionais.MoodCheck>
            {
                new() { Id = 1, FuncionarioId = 1, Humor = 3, Produtivo = false, Estressado = true, DormiuBem = true }
            });

            var service = new MoodCheckService(repo.Object);
            var result = await service.GetAllAsync();
            Assert.Single(result);
            Assert.Equal(1, result.First().Id);
        }
    }
}