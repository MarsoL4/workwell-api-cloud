using Xunit;
using Moq;
using Microsoft.AspNetCore.Mvc;
using WorkWell.API.Controllers;
using WorkWell.Application.Services.AvaliacoesEmocionais;
using WorkWell.Application.DTOs.AvaliacoesEmocionais;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WorkWell.Tests.Controllers
{
    public class PerfilEmocionalControllerTests
    {
        private readonly Mock<IPerfilEmocionalService> _serviceMock;
        private readonly PerfilEmocionalController _controller;

        public PerfilEmocionalControllerTests()
        {
            _serviceMock = new Mock<IPerfilEmocionalService>();
            _controller = new PerfilEmocionalController(_serviceMock.Object);
        }

        [Fact]
        public async Task GetAll_ReturnsOk()
        {
            _serviceMock.Setup(x => x.GetAllAsync()).ReturnsAsync(new List<PerfilEmocionalDto>());
            var result = await _controller.GetAll();
            Assert.IsType<OkObjectResult>(result.Result);
        }

        [Fact]
        public async Task GetById_ReturnsPerfil_IfFound()
        {
            _serviceMock.Setup(x => x.GetByIdAsync(22)).ReturnsAsync(new PerfilEmocionalDto { Id = 22 });
            var result = await _controller.GetById(22);
            var ok = Assert.IsType<OkObjectResult>(result.Result);
            Assert.IsType<PerfilEmocionalDto>(ok.Value);
        }

        [Fact]
        public async Task GetById_ReturnsNotFound()
        {
            _serviceMock.Setup(x => x.GetByIdAsync(110)).ReturnsAsync((PerfilEmocionalDto?)null);
            var result = await _controller.GetById(110);
            Assert.IsType<NotFoundObjectResult>(result.Result);
        }

        [Fact]
        public async Task Create_ReturnsCreated()
        {
            _serviceMock.Setup(x => x.CreateAsync(It.IsAny<PerfilEmocionalDto>())).ReturnsAsync(12);
            _serviceMock.Setup(x => x.GetByIdAsync(12)).ReturnsAsync(new PerfilEmocionalDto { Id = 12 });
            var result = await _controller.Create(new PerfilEmocionalDto());
            var created = Assert.IsType<CreatedAtActionResult>(result.Result);
            var createdDto = Assert.IsType<PerfilEmocionalDto>(created.Value);
            Assert.Equal(12L, createdDto.Id);
        }

        [Fact]
        public async Task Update_ReturnsNoContent_WhenOk()
        {
            var dto = new PerfilEmocionalDto { Id = 6 };
            var result = await _controller.Update(6, dto);
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task Update_ReturnsBadRequest_IfIdsDiffer()
        {
            var dto = new PerfilEmocionalDto { Id = 7 };
            var result = await _controller.Update(8, dto);
            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public async Task Delete_ReturnsNoContent()
        {
            var result = await _controller.Delete(100);
            Assert.IsType<NoContentResult>(result);
        }
    }
}