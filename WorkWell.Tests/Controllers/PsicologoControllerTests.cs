using Xunit;
using Moq;
using Microsoft.AspNetCore.Mvc;
using WorkWell.API.Controllers;
using WorkWell.Application.Services.ApoioPsicologico;
using WorkWell.Application.DTOs.ApoioPsicologico;
using WorkWell.Application.DTOs.Paginacao;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WorkWell.Tests.Controllers
{
    public class PsicologoControllerTests
    {
        private readonly Mock<IPsicologoService> _serviceMock;
        private readonly PsicologoController _controller;

        public PsicologoControllerTests()
        {
            _serviceMock = new Mock<IPsicologoService>();
            _controller = new PsicologoController(_serviceMock.Object);
        }

        [Fact]
        public async Task GetAllPaged_ReturnsOk()
        {
            _serviceMock.Setup(x => x.GetAllPagedAsync(1, 10))
                .ReturnsAsync(new PagedResultDto<PsicologoDto>
                {
                    Page = 1,
                    PageSize = 10,
                    TotalCount = 1
                });
            var result = await _controller.GetAllPaged(1, 10);
            Assert.IsType<OkObjectResult>(result.Result);
        }

        [Fact]
        public async Task GetById_ReturnsFound()
        {
            _serviceMock.Setup(x => x.GetByIdAsync(22)).ReturnsAsync(new PsicologoDto { Id = 22 });
            var result = await _controller.GetById(22);
            var ok = Assert.IsType<OkObjectResult>(result.Result);
            Assert.IsType<PsicologoDto>(ok.Value);
        }

        [Fact]
        public async Task GetById_ReturnsNotFound()
        {
            _serviceMock.Setup(x => x.GetByIdAsync(44)).ReturnsAsync((PsicologoDto?)null);
            var result = await _controller.GetById(44);
            Assert.IsType<NotFoundObjectResult>(result.Result);
        }

        [Fact]
        public async Task Create_ReturnsCreated()
        {
            _serviceMock.Setup(x => x.CreateAsync(It.IsAny<PsicologoDto>())).ReturnsAsync(51);
            _serviceMock.Setup(x => x.GetByIdAsync(51)).ReturnsAsync(new PsicologoDto { Id = 51 });
            var result = await _controller.Create(new PsicologoDto { Nome = "Psico", Email = "e@e.com", Crp = "1", Ativo = true, SetorId = 1, Senha = "", TokenEmpresa = "" });
            var created = Assert.IsType<CreatedAtActionResult>(result.Result);
            var createdDto = Assert.IsType<PsicologoDto>(created.Value);
            Assert.Equal(51L, createdDto.Id);
        }

        [Fact]
        public async Task Update_ReturnsNoContent_WhenOk()
        {
            var dto = new PsicologoDto { Id = 7, Nome = "Ps", Email = "a", Crp = "b", Ativo = true, SetorId = 2, Senha = "", TokenEmpresa = "" };
            var result = await _controller.Update(7, dto);
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task Update_ReturnsBadRequest_IfIdsDiffer()
        {
            var dto = new PsicologoDto { Id = 77, Nome = "Ps", Email = "a", Crp = "b", Ativo = true, SetorId = 2, Senha = "", TokenEmpresa = "" };
            var result = await _controller.Update(7, dto);
            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public async Task Delete_ReturnsNoContent()
        {
            var result = await _controller.Delete(123);
            Assert.IsType<NoContentResult>(result);
        }
    }
}