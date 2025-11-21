using Xunit;
using Moq;
using Microsoft.AspNetCore.Mvc;
using WorkWell.API.Controllers;
using WorkWell.Application.Services.OmbudMind;
using WorkWell.Application.DTOs.OmbudMind;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WorkWell.Tests.Controllers
{
    public class DenunciaControllerTests
    {
        private readonly Mock<IDenunciaService> _serviceMock;
        private readonly DenunciaController _controller;

        public DenunciaControllerTests()
        {
            _serviceMock = new Mock<IDenunciaService>();
            _controller = new DenunciaController(_serviceMock.Object);
        }

        [Fact]
        public async Task GetAll_ReturnsOk()
        {
            _serviceMock.Setup(x => x.GetAllAsync()).ReturnsAsync(new List<DenunciaDto>());
            var result = await _controller.GetAll();
            Assert.IsType<OkObjectResult>(result.Result);
        }

        [Fact]
        public async Task GetById_Found()
        {
            _serviceMock.Setup(x => x.GetByIdAsync(5)).ReturnsAsync(new DenunciaDto { Id = 5 });
            var result = await _controller.GetById(5);
            var ok = Assert.IsType<OkObjectResult>(result.Result);
            Assert.IsType<DenunciaDto>(ok.Value);
        }

        [Fact]
        public async Task GetById_NotFound()
        {
            _serviceMock.Setup(x => x.GetByIdAsync(99)).ReturnsAsync((DenunciaDto?)null);
            var result = await _controller.GetById(99);
            Assert.IsType<NotFoundObjectResult>(result.Result);
        }

        [Fact]
        public async Task GetByCodigoRastreamento_Found()
        {
            _serviceMock.Setup(x => x.GetByCodigoRastreamentoAsync("abc")).ReturnsAsync(new DenunciaDto { CodigoRastreamento = "abc" });
            var result = await _controller.GetByCodigoRastreamento("abc");
            var ok = Assert.IsType<OkObjectResult>(result.Result);
            Assert.IsType<DenunciaDto>(ok.Value);
        }

        [Fact]
        public async Task GetByCodigoRastreamento_NotFound()
        {
            _serviceMock.Setup(x => x.GetByCodigoRastreamentoAsync("xyz")).ReturnsAsync((DenunciaDto?)null);
            var result = await _controller.GetByCodigoRastreamento("xyz");
            Assert.IsType<NotFoundObjectResult>(result.Result);
        }

        [Fact]
        public async Task Create_ReturnsCreated()
        {
            _serviceMock.Setup(x => x.CreateAsync(It.IsAny<DenunciaDto>())).ReturnsAsync(19);
            _serviceMock.Setup(x => x.GetByIdAsync(19)).ReturnsAsync(new DenunciaDto { Id = 19 });
            var result = await _controller.Create(new DenunciaDto());
            var created = Assert.IsType<CreatedAtActionResult>(result.Result);
            var createdDto = Assert.IsType<DenunciaDto>(created.Value);
            Assert.Equal(19L, createdDto.Id);
        }

        [Fact]
        public async Task Update_ReturnsNoContent_WhenOk()
        {
            _serviceMock.Setup(x => x.GetByIdAsync(2)).ReturnsAsync(new DenunciaDto { Id = 2 });
            var dto = new DenunciaDto { Id = 2 };
            var result = await _controller.Update(2, dto);
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task Update_ReturnsBadRequest_IfIdsDiffer()
        {
            var dto = new DenunciaDto { Id = 8 };
            var result = await _controller.Update(7, dto);
            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public async Task Update_ReturnsNotFound_IfMissing()
        {
            _serviceMock.Setup(x => x.GetByIdAsync(14)).ReturnsAsync((DenunciaDto?)null);
            var dto = new DenunciaDto { Id = 14 };
            var result = await _controller.Update(14, dto);
            Assert.IsType<NotFoundObjectResult>(result);
        }

        [Fact]
        public async Task Delete_ReturnsNoContent()
        {
            var result = await _controller.Delete(52);
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task GetInvestigacoes_ReturnsOk()
        {
            _serviceMock.Setup(x => x.GetInvestigacoesAsync(2)).ReturnsAsync(new List<InvestigacaoDenunciaDto>());
            var res = await _controller.GetInvestigacoes(2);
            Assert.IsType<OkObjectResult>(res.Result);
        }

        [Fact]
        public async Task AdicionarInvestigacao_ReturnsCreated()
        {
            _serviceMock.Setup(x => x.AdicionarInvestigacaoAsync(1, It.IsAny<InvestigacaoDenunciaDto>())).ReturnsAsync(23);
            _serviceMock.Setup(x => x.GetInvestigacoesAsync(1)).ReturnsAsync(new List<InvestigacaoDenunciaDto> { new InvestigacaoDenunciaDto { Id = 23 } });
            var res = await _controller.AdicionarInvestigacao(1, new InvestigacaoDenunciaDto());
            var created = Assert.IsType<CreatedAtActionResult>(res.Result);
            var createdDto = Assert.IsType<InvestigacaoDenunciaDto>(created.Value);
            Assert.Equal(23L, createdDto.Id);
        }
    }
}