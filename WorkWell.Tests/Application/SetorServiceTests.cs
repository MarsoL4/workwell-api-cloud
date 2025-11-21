using Xunit;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;
using WorkWell.Application.Services.EmpresaOrganizacao;
using WorkWell.Domain.Interfaces.EmpresaOrganizacao;
using WorkWell.Application.DTOs.EmpresaOrganizacao;

namespace WorkWell.Tests.Application
{
    public class SetorServiceTests
    {
        [Fact]
        public async Task CreateAsync_DeveRetornarIdNovo()
        {
            var mockRepo = new Mock<ISetorRepository>();
            mockRepo.Setup(r => r.AddAsync(It.IsAny<WorkWell.Domain.Entities.EmpresaOrganizacao.Setor>()))
                .Returns(Task.CompletedTask)
                .Callback<WorkWell.Domain.Entities.EmpresaOrganizacao.Setor>(e => e.Id = 7);

            var service = new SetorService(mockRepo.Object);
            var dto = new SetorDto
            {
                Nome = "Recursos Humanos",
                EmpresaId = 1
            };

            var id = await service.CreateAsync(dto);

            Assert.Equal(7, id);
        }

        [Fact]
        public async Task GetAllAsync_DeveRetornarSetores()
        {
            var setores = new List<WorkWell.Domain.Entities.EmpresaOrganizacao.Setor>
            {
                new WorkWell.Domain.Entities.EmpresaOrganizacao.Setor { Id = 3, Nome = "Financeiro", EmpresaId = 1 }
            };
            var mockRepo = new Mock<ISetorRepository>();
            mockRepo.Setup(r => r.GetAllAsync()).ReturnsAsync(setores);

            var service = new SetorService(mockRepo.Object);
            var result = await service.GetAllAsync();

            Assert.Single(result);
            Assert.Equal("Financeiro", result.First().Nome);
        }
    }
}