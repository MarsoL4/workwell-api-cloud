using Xunit;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;
using WorkWell.Application.Services.EmpresaOrganizacao;
using WorkWell.Domain.Interfaces.EmpresaOrganizacao;
using WorkWell.Application.DTOs.EmpresaOrganizacao;

namespace WorkWell.Tests.Application
{
    public class EmpresaServiceTests
    {
        [Fact]
        public async Task CreateAsync_DeveRetornarIdNovo()
        {
            var mockRepo = new Mock<IEmpresaRepository>();
            mockRepo.Setup(r => r.AddAsync(It.IsAny<WorkWell.Domain.Entities.EmpresaOrganizacao.Empresa>()))
                .Returns(Task.CompletedTask)
                .Callback<WorkWell.Domain.Entities.EmpresaOrganizacao.Empresa>(e => e.Id = 42);

            var service = new EmpresaService(mockRepo.Object);
            var empresaDto = new EmpresaDto
            {
                Nome = "Exemplo Ltda",
                EmailAdmin = "admin@exemplo.com",
                TokenAcesso = "token",
                LogoUrl = "",
                CorPrimaria = "",
                CorSecundaria = "",
                Missao = "",
                PoliticaBemEstar = ""
            };

            var id = await service.CreateAsync(empresaDto);

            Assert.Equal(42, id);
        }

        [Fact]
        public async Task GetAllAsync_DeveRetornarEmpresas()
        {
            var empresas = new List<WorkWell.Domain.Entities.EmpresaOrganizacao.Empresa>
            {
                new WorkWell.Domain.Entities.EmpresaOrganizacao.Empresa { Id = 1, Nome = "Empresa A", EmailAdmin = "a@a.com", TokenAcesso = "", LogoUrl = "", CorPrimaria = "", CorSecundaria = "", Missao = "", PoliticaBemEstar = "" }
            };
            var mockRepo = new Mock<IEmpresaRepository>();
            mockRepo.Setup(r => r.GetAllAsync()).ReturnsAsync(empresas);

            var service = new EmpresaService(mockRepo.Object);
            var result = await service.GetAllAsync();

            Assert.Single(result);
            Assert.Equal("Empresa A", result.First().Nome);
        }
    }
}