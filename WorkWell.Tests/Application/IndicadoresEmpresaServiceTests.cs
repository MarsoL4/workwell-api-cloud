using Xunit;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;
using WorkWell.Application.Services.Indicadores;
using WorkWell.Domain.Interfaces.Indicadores;
using WorkWell.Application.DTOs.Indicadores;

namespace WorkWell.Tests.Application
{
    public class IndicadoresEmpresaServiceTests
    {
        [Fact]
        public async Task CreateAsync_DeveRetornarId()
        {
            var repo = new Mock<IIndicadoresEmpresaRepository>();
            repo.Setup(r => r.AddAsync(It.IsAny<WorkWell.Domain.Entities.Indicadores.IndicadoresEmpresa>()))
                .Returns(Task.CompletedTask)
                .Callback<WorkWell.Domain.Entities.Indicadores.IndicadoresEmpresa>(i => i.Id = 24);

            var service = new IndicadoresEmpresaService(repo.Object);
            var dto = new IndicadoresEmpresaDto { EmpresaId = 5, HumorMedio = 3.5, AdesaoAtividadesGeral = 0.7, FrequenciaConsultas = 0.1 };

            var id = await service.CreateAsync(dto);
            Assert.Equal(24, id);
        }

        [Fact]
        public async Task GetAllAsync_DeveRetornarIndicadores()
        {
            var repo = new Mock<IIndicadoresEmpresaRepository>();
            repo.Setup(r => r.GetAllAsync()).ReturnsAsync(new List<WorkWell.Domain.Entities.Indicadores.IndicadoresEmpresa>
            {
                new() { Id = 11, EmpresaId = 7, HumorMedio = 4.3, AdesaoAtividadesGeral = 0.6, FrequenciaConsultas = 0.05 }
            });

            var service = new IndicadoresEmpresaService(repo.Object);
            var result = await service.GetAllAsync();

            Assert.Single(result);
            Assert.Equal(11, result.First().Id);
        }
    }
}