using Xunit;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;
using WorkWell.Application.Services.OmbudMind;
using WorkWell.Domain.Interfaces.OmbudMind;
using WorkWell.Application.DTOs.OmbudMind;
using WorkWell.Domain.Enums.OmbudMind;

namespace WorkWell.Tests.Application
{
    public class DenunciaServiceTests
    {
        [Fact]
        public async Task CreateAsync_DeveRetornarIdDenuncia()
        {
            var mockRepo = new Mock<IDenunciaRepository>();
            mockRepo.Setup(r => r.AddAsync(It.IsAny<WorkWell.Domain.Entities.OmbudMind.Denuncia>()))
                .Returns(Task.CompletedTask)
                .Callback<WorkWell.Domain.Entities.OmbudMind.Denuncia>(d => d.Id = 99);

            var service = new DenunciaService(mockRepo.Object, Mock.Of<IInvestigacaoDenunciaRepository>());
            var dto = new DenunciaDto
            {
                EmpresaId = 1,
                Tipo = TipoDenuncia.Bullying,
                Descricao = "desc",
                Status = StatusDenuncia.Aberta,
                CodigoRastreamento = "abc"
            };

            var id = await service.CreateAsync(dto);

            Assert.Equal(99, id);
        }

        [Fact]
        public async Task GetByIdAsync_DeveRetornarDenuncia()
        {
            var mockRepo = new Mock<IDenunciaRepository>();
            mockRepo.Setup(r => r.GetByIdAsync(2)).ReturnsAsync(new WorkWell.Domain.Entities.OmbudMind.Denuncia { Id = 2 });

            var service = new DenunciaService(mockRepo.Object, Mock.Of<IInvestigacaoDenunciaRepository>());
            var denuncia = await service.GetByIdAsync(2);

            Assert.NotNull(denuncia);
            Assert.Equal(2, denuncia?.Id);
        }
    }
}