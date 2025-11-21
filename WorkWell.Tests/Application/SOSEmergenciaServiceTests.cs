using Xunit;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;
using WorkWell.Application.Services.ApoioPsicologico;
using WorkWell.Domain.Interfaces.ApoioPsicologico;
using WorkWell.Application.DTOs.ApoioPsicologico;
using WorkWell.Domain.Entities.ApoioPsicologico;

namespace WorkWell.Tests.Application
{
    public class SOSEmergenciaServiceTests
    {
        [Fact]
        public async Task GetAllAsync_DeveRetornarSOS()
        {
            var registros = new List<SOSemergencia>
            {
                new() { Id = 10, FuncionarioId = 1, Tipo = "Crise", DataAcionamento = DateTime.Now }
            };
            var mockRepo = new Mock<ISOSemergenciaRepository>();
            mockRepo.Setup(r => r.GetAllAsync()).ReturnsAsync(registros);

            var service = new SOSemergenciaService(mockRepo.Object);

            var result = await service.GetAllAsync();

            Assert.Single(result);
            Assert.Equal(10, result.First().Id);
        }

        [Fact]
        public async Task CreateAsync_DeveRetornarIdSOS()
        {
            var mockRepo = new Mock<ISOSemergenciaRepository>();
            mockRepo.Setup(r => r.AddAsync(It.IsAny<SOSemergencia>()))
                .Returns(Task.CompletedTask)
                .Callback<SOSemergencia>(s => s.Id = 8);

            var service = new SOSemergenciaService(mockRepo.Object);
            var dto = new SOSemergenciaDto
            {
                FuncionarioId = 999,
                Tipo = "Crise"
            };

            var id = await service.CreateAsync(dto);
            Assert.Equal(8, id);
        }
    }
}