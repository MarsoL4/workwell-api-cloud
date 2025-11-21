using Xunit;
using Moq;
using Microsoft.AspNetCore.Mvc;
using WorkWell.API.Controllers;
using WorkWell.Application.Services.ApoioPsicologico;
using WorkWell.Application.DTOs.ApoioPsicologico;
using System.Collections.Generic;

namespace WorkWell.Tests.Controllers
{
    public class ChatAnonimoControllerTests
    {
        private readonly Mock<IChatAnonimoService> _serviceMock;
        private readonly ChatAnonimoController _controller;

        public ChatAnonimoControllerTests()
        {
            _serviceMock = new Mock<IChatAnonimoService>();
            _controller = new ChatAnonimoController(_serviceMock.Object);
        }

        [Fact]
        public async Task GetAll_ReturnsOk()
        {
            _serviceMock.Setup(x => x.GetAllAsync()).ReturnsAsync(new List<ChatAnonimoDto>());
            var result = await _controller.GetAll();
            Assert.IsType<OkObjectResult>(result.Result);
        }

        [Fact]
        public async Task GetById_Found()
        {
            _serviceMock.Setup(x => x.GetByIdAsync(55)).ReturnsAsync(new ChatAnonimoDto { Id = 55 });
            var result = await _controller.GetById(55);
            var ok = Assert.IsType<OkObjectResult>(result.Result);
            Assert.IsType<ChatAnonimoDto>(ok.Value);
        }

        [Fact]
        public async Task GetById_NotFound()
        {
            _serviceMock.Setup(x => x.GetByIdAsync(99)).ReturnsAsync((ChatAnonimoDto?)null);
            var result = await _controller.GetById(99);
            Assert.IsType<NotFoundObjectResult>(result.Result);
        }

        [Fact]
        public async Task Create_ReturnsCreated()
        {
            _serviceMock.Setup(x => x.CreateAsync(It.IsAny<ChatAnonimoDto>())).ReturnsAsync(9);
            _serviceMock.Setup(x => x.GetByIdAsync(9)).ReturnsAsync(new ChatAnonimoDto { Id = 9, PsicologoId = 1, Mensagem = "msg", Anonimo = true });
            var result = await _controller.Create(new ChatAnonimoDto { PsicologoId = 1, Mensagem = "msg", Anonimo = true });
            var created = Assert.IsType<CreatedAtActionResult>(result.Result);
            var createdDto = Assert.IsType<ChatAnonimoDto>(created.Value);
            Assert.Equal(9L, createdDto.Id);
        }

        [Fact]
        public async Task Update_ReturnsNoContent_WhenOk()
        {
            var dto = new ChatAnonimoDto { Id = 3, PsicologoId = 1, Mensagem = "msg", Anonimo = true };
            _serviceMock.Setup(x => x.GetByIdAsync(3)).ReturnsAsync(dto);
            var result = await _controller.Update(3, dto);
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task Update_ReturnsBadRequest_IfIdsDiffer()
        {
            var result = await _controller.Update(1, new ChatAnonimoDto { Id = 6 });
            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public async Task Update_ReturnsNotFound_IfNotFound()
        {
            var dto = new ChatAnonimoDto { Id = 7 };
            _serviceMock.Setup(x => x.GetByIdAsync(7)).ReturnsAsync((ChatAnonimoDto?)null);
            var result = await _controller.Update(7, dto);
            Assert.IsType<NotFoundObjectResult>(result);
        }

        [Fact]
        public async Task Delete_ReturnsNoContent()
        {
            var result = await _controller.Delete(22);
            Assert.IsType<NoContentResult>(result);
        }
    }
}