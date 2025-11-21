using Xunit;
using Moq;
using Microsoft.AspNetCore.Mvc;
using WorkWell.API.Controllers;
using WorkWell.Application.Services.ApoioPsicologico;
using WorkWell.Application.DTOs.ApoioPsicologico;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WorkWell.Tests.Controllers
{
    public class ConsultaPsicologicaControllerTests
    {
        private readonly Mock<IConsultaPsicologicaService> _serviceMock;
        private readonly ConsultaPsicologicaController _controller;

        public ConsultaPsicologicaControllerTests()
        {
            _serviceMock = new Mock<IConsultaPsicologicaService>();
            _controller = new ConsultaPsicologicaController(_serviceMock.Object);
        }

        [Fact]
        public async Task GetAll_ReturnsOk()
        {
            _serviceMock.Setup(x => x.GetAllAsync()).ReturnsAsync(new List<ConsultaPsicologicaDto>());
            var result = await _controller.GetAll();
            Assert.IsType<OkObjectResult>(result.Result);
        }

        [Fact]
        public async Task GetById_Found()
        {
            _serviceMock.Setup(x => x.GetByIdAsync(11)).ReturnsAsync(new ConsultaPsicologicaDto { Id = 11 });
            var result = await _controller.GetById(11);
            var ok = Assert.IsType<OkObjectResult>(result.Result);
            Assert.IsType<ConsultaPsicologicaDto>(ok.Value);
        }

        [Fact]
        public async Task GetById_NotFound()
        {
            _serviceMock.Setup(x => x.GetByIdAsync(1000)).ReturnsAsync((ConsultaPsicologicaDto?)null);
            var result = await _controller.GetById(1000);
            Assert.IsType<NotFoundObjectResult>(result.Result);
        }

        [Fact]
        public async Task Create_ReturnsCreated()
        {
            _serviceMock.Setup(x => x.CreateAsync(It.IsAny<ConsultaPsicologicaDto>())).ReturnsAsync(33);
            _serviceMock.Setup(x => x.GetByIdAsync(33)).ReturnsAsync(new ConsultaPsicologicaDto { Id = 33 });
            var result = await _controller.Create(new ConsultaPsicologicaDto());
            var created = Assert.IsType<CreatedAtActionResult>(result.Result);
            var createdDto = Assert.IsType<ConsultaPsicologicaDto>(created.Value);
            Assert.Equal(33L, createdDto.Id);
        }

        [Fact]
        public async Task Update_ReturnsNoContent_WhenOk()
        {
            _serviceMock.Setup(x => x.GetByIdAsync(7)).ReturnsAsync(new ConsultaPsicologicaDto { Id = 7 });
            var dto = new ConsultaPsicologicaDto { Id = 7 };
            var result = await _controller.Update(7, dto);
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task Update_ReturnsBadRequest_IfIdsDiffer()
        {
            var dto = new ConsultaPsicologicaDto { Id = 5 };
            var result = await _controller.Update(8, dto);
            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public async Task Update_ReturnsNotFound_IfMissing()
        {
            _serviceMock.Setup(x => x.GetByIdAsync(12)).ReturnsAsync((ConsultaPsicologicaDto?)null);
            var dto = new ConsultaPsicologicaDto { Id = 12 };
            var result = await _controller.Update(12, dto);
            Assert.IsType<NotFoundObjectResult>(result);
        }

        [Fact]
        public async Task Delete_ReturnsNoContent()
        {
            var result = await _controller.Delete(11);
            Assert.IsType<NoContentResult>(result);
        }
    }
}