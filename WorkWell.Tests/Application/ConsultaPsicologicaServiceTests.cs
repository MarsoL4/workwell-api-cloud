using Xunit;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;
using WorkWell.Application.Services.ApoioPsicologico;
using WorkWell.Domain.Interfaces.ApoioPsicologico;
using WorkWell.Application.DTOs.ApoioPsicologico;
using WorkWell.Domain.Enums.ApoioPsicologico;

namespace WorkWell.Tests.Application
{
    public class ConsultaPsicologicaServiceTests
    {
        [Fact]
        public async Task CreateAsync_DeveRetornarIdConsulta()
        {
            var mockRepo = new Mock<IConsultaPsicologicaRepository>();
            mockRepo.Setup(x => x.AddAsync(It.IsAny<WorkWell.Domain.Entities.ApoioPsicologico.ConsultaPsicologica>()))
                .Returns(Task.CompletedTask)
                .Callback<WorkWell.Domain.Entities.ApoioPsicologico.ConsultaPsicologica>(c => c.Id = 77);

            var service = new ConsultaPsicologicaService(mockRepo.Object);
            var dto = new ConsultaPsicologicaDto
            {
                FuncionarioId = 101,
                PsicologoId = 201,
                DataConsulta = DateTime.UtcNow,
                Tipo = TipoConsulta.Online,
                Status = StatusConsulta.Agendada,
                AnotacoesSigilosas = ""
            };

            var id = await service.CreateAsync(dto);

            Assert.Equal(77, id);
        }

        [Fact]
        public async Task GetAllAsync_DeveRetornarConsultas()
        {
            var lista = new List<WorkWell.Domain.Entities.ApoioPsicologico.ConsultaPsicologica>
            {
                new() { Id=10, FuncionarioId=1, PsicologoId=2, DataConsulta=DateTime.UtcNow, Tipo=TipoConsulta.Online, Status=StatusConsulta.Agendada, AnotacoesSigilosas="" }
            };
            var mockRepo = new Mock<IConsultaPsicologicaRepository>();
            mockRepo.Setup(x => x.GetAllAsync()).ReturnsAsync(lista);

            var service = new ConsultaPsicologicaService(mockRepo.Object);
            var result = await service.GetAllAsync();

            Assert.Single(result);
            Assert.Equal(10, result.First().Id);
        }
    }
}