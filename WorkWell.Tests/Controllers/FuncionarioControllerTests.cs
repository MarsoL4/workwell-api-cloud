using Xunit;
using Moq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using WorkWell.API.Controllers;
using WorkWell.Application.Services.EmpresaOrganizacao;
using WorkWell.Application.DTOs.EmpresaOrganizacao;
using WorkWell.Application.DTOs.Paginacao;

namespace WorkWell.Tests.Controllers
{
    public class FuncionarioControllerTests
    {
        private readonly Mock<IFuncionarioService> _serviceMock;
        private readonly FuncionarioController _controller;

        public FuncionarioControllerTests()
        {
            _serviceMock = new Mock<IFuncionarioService>();
            _controller = new FuncionarioController(_serviceMock.Object);
        }

        [Fact]
        public async Task GetAllPaged_ReturnsOk()
        {
            _controller.ControllerContext = new ControllerContext { HttpContext = new DefaultHttpContext() };
            _serviceMock.Setup(x => x.GetAllPagedAsync(1, 10)).ReturnsAsync(new PagedResultDto<FuncionarioDto>
            {
                Page = 1,
                PageSize = 10,
                TotalCount = 1
            });
            var result = await _controller.GetAllPaged(1, 10);
            Assert.IsType<OkObjectResult>(result.Result);
        }

        [Fact]
        public async Task GetById_ReturnsFuncionario_IfFound()
        {
            _serviceMock.Setup(x => x.GetByIdAsync(33)).ReturnsAsync(new FuncionarioDto { Id = 33 });
            var result = await _controller.GetById(33);
            var ok = Assert.IsType<OkObjectResult>(result.Result);
            Assert.IsType<FuncionarioDto>(ok.Value);
        }

        [Fact]
        public async Task GetById_ReturnsNotFound_IfMissing()
        {
            _serviceMock.Setup(x => x.GetByIdAsync(3333)).ReturnsAsync((FuncionarioDto?)null);
            var result = await _controller.GetById(3333);
            Assert.IsType<NotFoundObjectResult>(result.Result);
        }

        [Fact]
        public async Task Create_ReturnsCreatedAt()
        {
            _serviceMock.Setup(x => x.CreateAsync(It.IsAny<FuncionarioDto>())).ReturnsAsync(1234);
            _serviceMock.Setup(x => x.GetByIdAsync(1234)).ReturnsAsync(new FuncionarioDto { Id = 1234 });
            var result = await _controller.Create(new FuncionarioDto { Nome = "x", Email = "y", Cargo = Domain.Enums.EmpresaOrganizacao.Cargo.Funcionario, Ativo = true, SetorId = 1 });
            var action = Assert.IsType<CreatedAtActionResult>(result.Result);
            var createdDto = Assert.IsType<FuncionarioDto>(action.Value);
            Assert.Equal(1234L, createdDto.Id);
        }

        [Fact]
        public async Task Update_ReturnsNoContent_WhenOk()
        {
            var dto = new FuncionarioDto { Id = 88, Nome = "N", Email = "a", Cargo = Domain.Enums.EmpresaOrganizacao.Cargo.Funcionario, Ativo = true, SetorId = 1 };
            var result = await _controller.Update(88, dto);
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task Update_ReturnsBadRequest_IfIdsDiffer()
        {
            var dto = new FuncionarioDto { Id = 1, Nome = "N", Email = "a", Cargo = Domain.Enums.EmpresaOrganizacao.Cargo.Funcionario, Ativo = true, SetorId = 1 };
            var result = await _controller.Update(5, dto);
            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public async Task Delete_ReturnsNoContent()
        {
            var result = await _controller.Delete(1);
            Assert.IsType<NoContentResult>(result);
        }
    }
}