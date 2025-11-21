using Xunit;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;
using WorkWell.Application.Services.EmpresaOrganizacao;
using WorkWell.Domain.Interfaces.EmpresaOrganizacao;
using WorkWell.Application.DTOs.EmpresaOrganizacao;
using WorkWell.Domain.Enums.EmpresaOrganizacao;

namespace WorkWell.Tests.Application
{
    public class FuncionarioServiceTests
    {
        [Fact]
        public async Task CreateAsync_DeveRetornarId()
        {
            var mockRepo = new Mock<IFuncionarioRepository>();
            mockRepo.Setup(x => x.AddAsync(It.IsAny<WorkWell.Domain.Entities.EmpresaOrganizacao.Funcionario>()))
                .Returns(Task.CompletedTask)
                .Callback<WorkWell.Domain.Entities.EmpresaOrganizacao.Funcionario>(f => f.Id = 15);

            var service = new FuncionarioService(mockRepo.Object);
            var dto = new FuncionarioDto
            {
                Nome = "João",
                Email = "joao@empresa.com",
                Cargo = Cargo.Funcionario,
                Ativo = true,
                SetorId = 99
            };

            var id = await service.CreateAsync(dto);

            Assert.Equal(15, id);
        }

        [Fact]
        public async Task GetAllAsync_DeveRetornarFuncionarios()
        {
            var list = new List<WorkWell.Domain.Entities.EmpresaOrganizacao.Funcionario>
            {
                new() { Id = 1, Nome = "Funcionario 1", Email = "a@b", Cargo = Cargo.Funcionario, Ativo = true, SetorId = 1, Senha = "", TokenEmpresa = "" }
            };
            var mockRepo = new Mock<IFuncionarioRepository>();
            mockRepo.Setup(r => r.GetAllAsync()).ReturnsAsync(list);

            var service = new FuncionarioService(mockRepo.Object);
            var result = await service.GetAllAsync();

            Assert.Single(result);
            Assert.Equal("Funcionario 1", result.First().Nome);
        }
    }
}