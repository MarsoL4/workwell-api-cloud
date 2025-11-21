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
    public class AvaliacaoProfundaControllerTests
    {
        private readonly Mock<IAvaliacaoProfundaService> _serviceMock;
        private readonly AvaliacaoProfundaController _controller;

        public AvaliacaoProfundaControllerTests()
        {
            _serviceMock = new Mock<IAvaliacaoProfundaService>();
            _controller = new AvaliacaoProfundaController(_serviceMock.Object);
        }

        [Fact]
        public async Task GetAll_ReturnsOk()
        {
            _serviceMock.Setup(x => x.GetAllAsync()).ReturnsAsync(new List<AvaliacaoProfundaDto>());
            var result = await _controller.GetAll();
            Assert.IsType<OkObjectResult>(result.Result);
        }

        [Fact]
        public async Task GetById_Found()
        {
            _serviceMock.Setup(x => x.GetByIdAsync(7)).ReturnsAsync(new AvaliacaoProfundaDto { Id = 7 });
            var result = await _controller.GetById(7);
            var ok = Assert.IsType<OkObjectResult>(result.Result);
            Assert.IsType<AvaliacaoProfundaDto>(ok.Value);
        }

        [Fact]
        public async Task GetById_NotFound()
        {
            _serviceMock.Setup(x => x.GetByIdAsync(99)).ReturnsAsync((AvaliacaoProfundaDto?)null);
            var result = await _controller.GetById(99);
            Assert.IsType<NotFoundObjectResult>(result.Result);
        }

        [Fact]
        public async Task Create_ReturnsCreated()
        {
            _serviceMock.Setup(x => x.CreateAsync(It.IsAny<AvaliacaoProfundaDto>())).ReturnsAsync(12);
            _serviceMock.Setup(x => x.GetByIdAsync(12)).ReturnsAsync(new AvaliacaoProfundaDto { Id = 12 });
            var result = await _controller.Create(new AvaliacaoProfundaDto());
            var created = Assert.IsType<CreatedAtActionResult>(result.Result);
            var createdDto = Assert.IsType<AvaliacaoProfundaDto>(created.Value);
            Assert.Equal(12L, createdDto.Id);
        }

        [Fact]
        public async Task Update_ReturnsNoContent_WhenOk()
        {
            _serviceMock.Setup(x => x.GetByIdAsync(5)).ReturnsAsync(new AvaliacaoProfundaDto { Id = 5 });
            var dto = new AvaliacaoProfundaDto { Id = 5 };
            var result = await _controller.Update(5, dto);
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task Update_ReturnsBadRequest_IfIdsDiffer()
        {
            var result = await _controller.Update(1, new AvaliacaoProfundaDto { Id = 8 });
            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public async Task Update_ReturnsNotFound_IfMissing()
        {
            _serviceMock.Setup(x => x.GetByIdAsync(13)).ReturnsAsync((AvaliacaoProfundaDto?)null);
            var dto = new AvaliacaoProfundaDto { Id = 13 };
            var result = await _controller.Update(13, dto);
            Assert.IsType<NotFoundObjectResult>(result);
        }

        [Fact]
        public async Task Delete_ReturnsNoContent()
        {
            var result = await _controller.Delete(5);
            Assert.IsType<NoContentResult>(result);
        }
    }
}