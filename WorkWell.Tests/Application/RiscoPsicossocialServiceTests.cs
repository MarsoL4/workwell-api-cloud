using Xunit;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;
using WorkWell.Application.Services.AvaliacoesEmocionais;
using WorkWell.Domain.Interfaces.AvaliacoesEmocionais;
using WorkWell.Application.DTOs.AvaliacoesEmocionais;

namespace WorkWell.Tests.Application
{
    public class RiscoPsicossocialServiceTests
    {
        [Fact]
        public async Task CreateAsync_DeveRetornarIdNovo()
        {
            var mockRepo = new Mock<IRiscoPsicossocialRepository>();
            mockRepo.Setup(r => r.AddAsync(It.IsAny<WorkWell.Domain.Entities.AvaliacoesEmocionais.RiscoPsicossocial>()))
                .Returns(Task.CompletedTask)
                .Callback<WorkWell.Domain.Entities.AvaliacoesEmocionais.RiscoPsicossocial>(r => r.Id = 13);

            var service = new RiscoPsicossocialService(mockRepo.Object);
            var dto = new RiscoPsicossocialDto { FuncionarioId = 2, Categoria = "Estresse", NivelRisco = 3 };

            var id = await service.CreateAsync(dto);

            Assert.Equal(13, id);
        }

        [Fact]
        public async Task GetAllAsync_DeveRetornarRiscos()
        {
            var repo = new Mock<IRiscoPsicossocialRepository>();
            repo.Setup(r => r.GetAllAsync()).ReturnsAsync(new List<WorkWell.Domain.Entities.AvaliacoesEmocionais.RiscoPsicossocial>
            {
                new() { Id = 5, FuncionarioId = 9, Categoria = "Sobrecarga", NivelRisco = 2 }
            });

            var service = new RiscoPsicossocialService(repo.Object);
            var result = await service.GetAllAsync();

            Assert.Single(result);
            Assert.Equal(5, result.First().Id);
        }
    }
}