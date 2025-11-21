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
    public class MoodCheckControllerTests
    {
        private readonly Mock<IMoodCheckService> _serviceMock;
        private readonly MoodCheckController _controller;

        public MoodCheckControllerTests()
        {
            _serviceMock = new Mock<IMoodCheckService>();
            _controller = new MoodCheckController(_serviceMock.Object);
        }

        [Fact]
        public async Task GetAll_ReturnsOk()
        {
            _serviceMock.Setup(x => x.GetAllAsync()).ReturnsAsync(new List<MoodCheckDto>());
            var result = await _controller.GetAll();
            Assert.IsType<OkObjectResult>(result.Result);
        }

        [Fact]
        public async Task GetById_Found()
        {
            _serviceMock.Setup(x => x.GetByIdAsync(7)).ReturnsAsync(new MoodCheckDto { Id = 7 });
            var result = await _controller.GetById(7);
            var ok = Assert.IsType<OkObjectResult>(result.Result);
            Assert.IsType<MoodCheckDto>(ok.Value);
        }

        [Fact]
        public async Task GetById_NotFound()
        {
            _serviceMock.Setup(x => x.GetByIdAsync(99)).ReturnsAsync((MoodCheckDto?)null);
            var result = await _controller.GetById(99);
            Assert.IsType<NotFoundObjectResult>(result.Result);
        }

        [Fact]
        public async Task Create_ReturnsCreated()
        {
            _serviceMock.Setup(x => x.CreateAsync(It.IsAny<MoodCheckDto>())).ReturnsAsync(18);
            _serviceMock.Setup(x => x.GetByIdAsync(18)).ReturnsAsync(new MoodCheckDto { Id = 18, FuncionarioId = 2, Humor = 4, Produtivo = true, Estressado = false, DormiuBem = true });
            var result = await _controller.Create(new MoodCheckDto { FuncionarioId = 2, Humor = 4, Produtivo = true, Estressado = false, DormiuBem = true });
            var created = Assert.IsType<CreatedAtActionResult>(result.Result);
            var createdDto = Assert.IsType<MoodCheckDto>(created.Value);
            Assert.Equal(18L, createdDto.Id);
        }

        [Fact]
        public async Task Update_ReturnsNoContent_WhenOk()
        {
            var dto = new MoodCheckDto { Id = 4, FuncionarioId = 8, Humor = 2, Produtivo = true, Estressado = true, DormiuBem = false };
            var result = await _controller.Update(4, dto);
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task Update_ReturnsBadRequest_IfIdsDiffer()
        {
            var dto = new MoodCheckDto { Id = 8, FuncionarioId = 1 };
            var result = await _controller.Update(77, dto);
            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public async Task Delete_ReturnsNoContent()
        {
            var result = await _controller.Delete(8);
            Assert.IsType<NoContentResult>(result);
        }
    }
}