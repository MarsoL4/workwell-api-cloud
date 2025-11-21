using Xunit;
using Moq;
using Microsoft.AspNetCore.Mvc;
using WorkWell.API.Controllers;
using WorkWell.Application.Services.AtividadesBemEstar;
using WorkWell.Application.DTOs.AtividadesBemEstar;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WorkWell.Tests.Controllers
{
    public class AtividadeBemEstarControllerTests
    {
        private readonly Mock<IAtividadeBemEstarService> _serviceMock;
        private readonly AtividadeBemEstarController _controller;

        public AtividadeBemEstarControllerTests()
        {
            _serviceMock = new Mock<IAtividadeBemEstarService>();
            _controller = new AtividadeBemEstarController(_serviceMock.Object);
        }

        [Fact]
        public async Task GetAll_ReturnsOk()
        {
            _serviceMock.Setup(x => x.GetAllAsync()).ReturnsAsync(new List<AtividadeBemEstarDto>());
            var result = await _controller.GetAll();
            var ok = Assert.IsType<OkObjectResult>(result.Result);
            Assert.IsAssignableFrom<IEnumerable<AtividadeBemEstarDto>>(ok.Value);
        }

        [Fact]
        public async Task GetById_Found()
        {
            _serviceMock.Setup(x => x.GetByIdAsync(17)).ReturnsAsync(new AtividadeBemEstarDto { Id = 17 });
            var result = await _controller.GetById(17);
            var ok = Assert.IsType<OkObjectResult>(result.Result);
            Assert.IsType<AtividadeBemEstarDto>(ok.Value);
        }

        [Fact]
        public async Task GetById_NotFound()
        {
            _serviceMock.Setup(x => x.GetByIdAsync(88)).ReturnsAsync((AtividadeBemEstarDto?)null);
            var result = await _controller.GetById(88);
            Assert.IsType<NotFoundObjectResult>(result.Result);
        }

        [Fact]
        public async Task Create_ReturnsCreated()
        {
            _serviceMock.Setup(x => x.CreateAsync(It.IsAny<AtividadeBemEstarDto>())).ReturnsAsync(15);
            _serviceMock.Setup(x => x.GetByIdAsync(15)).ReturnsAsync(new AtividadeBemEstarDto { Id = 15 });
            var dto = new AtividadeBemEstarDto { EmpresaId = 1, Titulo = "X", Tipo = 0, Descricao = "", DataInicio = System.DateTime.Now, DataFim = System.DateTime.Now };
            var result = await _controller.Create(dto);
            var created = Assert.IsType<CreatedAtActionResult>(result.Result);
            var createdDto = Assert.IsType<AtividadeBemEstarDto>(created.Value);
            Assert.Equal(15L, createdDto.Id);
        }

        [Fact]
        public async Task Update_ReturnsNoContent_WhenOk()
        {
            _serviceMock.Setup(x => x.GetByIdAsync(5)).ReturnsAsync(new AtividadeBemEstarDto { Id = 5 });
            var dto = new AtividadeBemEstarDto { Id = 5, EmpresaId = 1, Titulo = "", Tipo = 0, Descricao = "", DataInicio = System.DateTime.Now, DataFim = System.DateTime.Now };
            var result = await _controller.Update(5, dto);
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task Update_ReturnsBadRequest_IfIdsDiffer()
        {
            var result = await _controller.Update(2, new AtividadeBemEstarDto { Id = 99 });
            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public async Task Update_ReturnsNotFound_IfNotFound()
        {
            _serviceMock.Setup(x => x.GetByIdAsync(44)).ReturnsAsync((AtividadeBemEstarDto?)null);
            var dto = new AtividadeBemEstarDto { Id = 44, EmpresaId = 1, Titulo = "", Tipo = 0, Descricao = "", DataInicio = System.DateTime.Now, DataFim = System.DateTime.Now };
            var result = await _controller.Update(44, dto);
            Assert.IsType<NotFoundObjectResult>(result);
        }

        [Fact]
        public async Task Delete_ReturnsNoContent_WhenFound()
        {
            _serviceMock.Setup(x => x.GetByIdAsync(3)).ReturnsAsync(new AtividadeBemEstarDto { Id = 3 });
            var result = await _controller.Delete(3);
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task Delete_ReturnsNotFound_WhenMissing()
        {
            _serviceMock.Setup(x => x.GetByIdAsync(87)).ReturnsAsync((AtividadeBemEstarDto?)null);
            var result = await _controller.Delete(87);
            Assert.IsType<NotFoundObjectResult>(result);
        }

        [Fact]
        public async Task GetParticipacoes_ReturnsOk()
        {
            _serviceMock.Setup(x => x.GetParticipacoesAsync(32)).ReturnsAsync(new List<ParticipacaoAtividadeDto>());
            var result = await _controller.GetParticipacoes(32);
            var ok = Assert.IsType<OkObjectResult>(result.Result);
            Assert.IsAssignableFrom<IEnumerable<ParticipacaoAtividadeDto>>(ok.Value);
        }

        [Fact]
        public async Task Participar_ReturnsOk() // controller devolve Ok
        {
            _serviceMock.Setup(x => x.AdicionarParticipacaoAsync(2, It.IsAny<ParticipacaoAtividadeDto>())).ReturnsAsync(111);
            _serviceMock.Setup(x => x.GetParticipacoesAsync(2)).ReturnsAsync(new List<ParticipacaoAtividadeDto> { new ParticipacaoAtividadeDto { Id = 111, FuncionarioId = 23, Participou = true } });
            var dto = new ParticipacaoAtividadeDto { FuncionarioId = 23, Participou = true };
            var result = await _controller.Participar(2, dto);
            var ok = Assert.IsType<OkObjectResult>(result.Result);
            var partDto = Assert.IsType<ParticipacaoAtividadeDto>(ok.Value);
            Assert.Equal(111L, partDto.Id);
        }
    }
}