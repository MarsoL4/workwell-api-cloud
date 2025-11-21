using Xunit;
using Moq;
using Microsoft.AspNetCore.Mvc;
using WorkWell.API.Controllers;
using WorkWell.Application.Services.Agenda;
using WorkWell.Application.DTOs.Agenda;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WorkWell.Tests.Controllers
{
    public class AgendaFuncionarioControllerTests
    {
        private readonly Mock<IAgendaFuncionarioService> _serviceMock;
        private readonly AgendaFuncionarioController _controller;

        public AgendaFuncionarioControllerTests()
        {
            _serviceMock = new Mock<IAgendaFuncionarioService>();
            _controller = new AgendaFuncionarioController(_serviceMock.Object);
        }

        [Fact]
        public async Task GetAll_ReturnsOk()
        {
            _serviceMock.Setup(x => x.GetAllAsync()).ReturnsAsync(new List<AgendaFuncionarioDto>());
            var result = await _controller.GetAll();
            var ok = Assert.IsType<OkObjectResult>(result.Result);
            Assert.IsAssignableFrom<IEnumerable<AgendaFuncionarioDto>>(ok.Value);
        }

        [Fact]
        public async Task GetById_Found()
        {
            _serviceMock.Setup(x => x.GetByIdAsync(5)).ReturnsAsync(new AgendaFuncionarioDto { Id = 5 });
            var result = await _controller.GetById(5);
            var ok = Assert.IsType<OkObjectResult>(result.Result);
            Assert.IsType<AgendaFuncionarioDto>(ok.Value);
        }

        [Fact]
        public async Task GetById_NotFound()
        {
            _serviceMock.Setup(x => x.GetByIdAsync(99)).ReturnsAsync((AgendaFuncionarioDto?)null);
            var result = await _controller.GetById(99);
            Assert.IsType<NotFoundObjectResult>(result.Result);
        }

        [Fact]
        public async Task Create_ReturnsCreated()
        {
            _serviceMock.Setup(x => x.CreateAsync(It.IsAny<AgendaFuncionarioDto>())).ReturnsAsync(99);
            _serviceMock.Setup(x => x.GetByIdAsync(99)).ReturnsAsync(new AgendaFuncionarioDto { Id = 99, FuncionarioId = 123, Data = DateTime.Today });
            var result = await _controller.Create(new AgendaFuncionarioDto { FuncionarioId = 123, Data = DateTime.Today, Itens = new List<ItemAgendaDto>() });
            var created = Assert.IsType<CreatedAtActionResult>(result.Result);
            var createdDto = Assert.IsType<AgendaFuncionarioDto>(created.Value);
            Assert.Equal(99L, createdDto.Id);
        }

        [Fact]
        public async Task Update_ReturnsNoContent_WhenOk()
        {
            var dto = new AgendaFuncionarioDto { Id = 4, FuncionarioId = 88, Data = DateTime.Now, Itens = new List<ItemAgendaDto>() };
            var result = await _controller.Update(4, dto);
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task Update_ReturnsBadRequest_IfIdsDiffer()
        {
            var dto = new AgendaFuncionarioDto { Id = 77, FuncionarioId = 88, Data = DateTime.Now };
            var result = await _controller.Update(88, dto);
            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public async Task Delete_ReturnsNoContent()
        {
            var result = await _controller.Delete(123);
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task GetItens_ReturnsOk()
        {
            _serviceMock.Setup(x => x.GetItensAsync(66)).ReturnsAsync(new List<ItemAgendaDto>());
            var result = await _controller.GetItens(66);
            var ok = Assert.IsType<OkObjectResult>(result.Result);
            Assert.IsAssignableFrom<IEnumerable<ItemAgendaDto>>(ok.Value);
        }

        [Fact]
        public async Task AdicionarItem_ReturnsOk() // controller retorna OkObjectResult
        {
            _serviceMock.Setup(x => x.AdicionarItemAsync(1, It.IsAny<ItemAgendaDto>())).ReturnsAsync(77);
            _serviceMock.Setup(x => x.GetItensAsync(1)).ReturnsAsync(new List<ItemAgendaDto> { new ItemAgendaDto { Id = 77, Tipo = "atividade", Titulo = "Título", Horario = DateTime.Now } });
            var result = await _controller.AdicionarItem(1, new ItemAgendaDto { Tipo = "atividade", Titulo = "Título", Horario = DateTime.Now });
            var ok = Assert.IsType<OkObjectResult>(result.Result);
            var itemDto = Assert.IsType<ItemAgendaDto>(ok.Value);
            Assert.Equal(77L, itemDto.Id);
        }
    }
}