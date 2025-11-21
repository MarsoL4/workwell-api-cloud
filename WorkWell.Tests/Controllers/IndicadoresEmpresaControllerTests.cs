using Xunit;
using Moq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using WorkWell.API.Controllers;
using WorkWell.Application.Services.Indicadores;
using WorkWell.Application.DTOs.Indicadores;
using WorkWell.Application.DTOs.Paginacao;
using System.Collections.Generic;

namespace WorkWell.Tests.Controllers
{
    public class IndicadoresEmpresaControllerTests
    {
        private readonly Mock<IIndicadoresEmpresaService> _serviceMock;
        private readonly IndicadoresEmpresaController _controller;

        public IndicadoresEmpresaControllerTests()
        {
            _serviceMock = new Mock<IIndicadoresEmpresaService>();
            _controller = new IndicadoresEmpresaController(_serviceMock.Object);
        }

        [Fact]
        public async Task GetAllPaged_ReturnsOk()
        {
            _controller.ControllerContext = new ControllerContext { HttpContext = new DefaultHttpContext() };
            _serviceMock.Setup(x => x.GetAllPagedAsync(1, 10)).ReturnsAsync(new PagedResultDto<IndicadoresEmpresaDto>
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
            _serviceMock.Setup(x => x.GetByIdAsync(9)).ReturnsAsync(new IndicadoresEmpresaDto { Id = 9 });
            var result = await _controller.GetById(9);
            var ok = Assert.IsType<OkObjectResult>(result.Result);
            Assert.IsType<IndicadoresEmpresaDto>(ok.Value);
        }

        [Fact]
        public async Task GetById_ReturnsNotFound()
        {
            _serviceMock.Setup(x => x.GetByIdAsync(54)).ReturnsAsync((IndicadoresEmpresaDto?)null);
            var result = await _controller.GetById(54);
            Assert.IsType<NotFoundObjectResult>(result.Result);
        }

        [Fact]
        public async Task Create_ReturnsCreated()
        {
            _serviceMock.Setup(x => x.CreateAsync(It.IsAny<IndicadoresEmpresaDto>())).ReturnsAsync(12);
            _serviceMock.Setup(x => x.GetByIdAsync(12)).ReturnsAsync(new IndicadoresEmpresaDto { Id = 12 });
            var result = await _controller.Create(new IndicadoresEmpresaDto { EmpresaId = 2, HumorMedio = 2, AdesaoAtividadesGeral = 0.5, FrequenciaConsultas = 0.1 });
            var created = Assert.IsType<CreatedAtActionResult>(result.Result);
            var createdDto = Assert.IsType<IndicadoresEmpresaDto>(created.Value);
            Assert.Equal(12L, createdDto.Id);
        }

        [Fact]
        public async Task Update_ReturnsNoContent_WhenOk()
        {
            var dto = new IndicadoresEmpresaDto { Id = 889, EmpresaId = 2 };
            var result = await _controller.Update(889, dto);
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task Update_ReturnsBadRequest_IfIdsDiffer()
        {
            var dto = new IndicadoresEmpresaDto { Id = 7, EmpresaId = 2 };
            var result = await _controller.Update(3, dto);
            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public async Task Delete_ReturnsNoContent()
        {
            var result = await _controller.Delete(456);
            Assert.IsType<NoContentResult>(result);
        }
    }
}