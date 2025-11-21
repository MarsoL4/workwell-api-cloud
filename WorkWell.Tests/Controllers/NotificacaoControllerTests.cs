using Xunit;
using Moq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using WorkWell.API.Controllers;
using WorkWell.Application.Services.Notificacoes;
using WorkWell.Application.DTOs.Notificacoes;
using WorkWell.Application.DTOs.Paginacao;
using System.Collections.Generic;

namespace WorkWell.Tests.Controllers
{
    public class NotificacaoControllerTests
    {
        private readonly Mock<INotificacaoService> _serviceMock;
        private readonly NotificacaoController _controller;

        public NotificacaoControllerTests()
        {
            _serviceMock = new Mock<INotificacaoService>();
            _controller = new NotificacaoController(_serviceMock.Object);
        }

        [Fact]
        public async Task GetAllPaged_ReturnsOk()
        {
            _controller.ControllerContext = new ControllerContext { HttpContext = new DefaultHttpContext() };
            _serviceMock.Setup(x => x.GetAllPagedAsync(1, 10)).ReturnsAsync(new PagedResultDto<NotificacaoDto>
            {
                Page = 1,
                PageSize = 10,
                TotalCount = 1,
            });
            var result = await _controller.GetAllPaged(1, 10);
            Assert.IsType<OkObjectResult>(result.Result);
        }

        [Fact]
        public async Task GetByFuncionario_ReturnsOk()
        {
            _serviceMock.Setup(x => x.GetAllByFuncionarioIdAsync(20)).ReturnsAsync(new List<NotificacaoDto>());
            var result = await _controller.GetByFuncionario(20);
            Assert.IsType<OkObjectResult>(result.Result);
        }

        [Fact]
        public async Task GetById_ReturnsOk_IfFound()
        {
            _serviceMock.Setup(x => x.GetByIdAsync(111)).ReturnsAsync(new NotificacaoDto { Id = 111 });
            var result = await _controller.GetById(111);
            var ok = Assert.IsType<OkObjectResult>(result.Result);
            Assert.IsType<NotificacaoDto>(ok.Value);
        }

        [Fact]
        public async Task GetById_ReturnsNotFound_IfMissing()
        {
            _serviceMock.Setup(x => x.GetByIdAsync(555)).ReturnsAsync((NotificacaoDto?)null);
            var result = await _controller.GetById(555);
            Assert.IsType<NotFoundObjectResult>(result.Result);
        }

        [Fact]
        public async Task Create_ReturnsCreated()
        {
            _serviceMock.Setup(x => x.CreateAsync(It.IsAny<NotificacaoDto>())).ReturnsAsync(31);
            _serviceMock.Setup(x => x.GetByIdAsync(31)).ReturnsAsync(new NotificacaoDto { Id = 31, FuncionarioId = 1, Mensagem = "mens", Tipo = Domain.Enums.Notificacoes.TipoNotificacao.Consulta, Lida = false });
            var result = await _controller.Create(new NotificacaoDto { FuncionarioId = 1, Mensagem = "mens", Tipo = Domain.Enums.Notificacoes.TipoNotificacao.Consulta, Lida = false });
            var created = Assert.IsType<CreatedAtActionResult>(result.Result);
            var createdDto = Assert.IsType<NotificacaoDto>(created.Value);
            Assert.Equal(31L, createdDto.Id);
        }

        [Fact]
        public async Task Update_ReturnsNoContent_WhenOk()
        {
            var dto = new NotificacaoDto { Id = 80, FuncionarioId = 1, Lida = false, Mensagem = "", Tipo = Domain.Enums.Notificacoes.TipoNotificacao.Pausa };
            var result = await _controller.Update(80, dto);
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task Update_ReturnsBadRequest_IfIdsDiffer()
        {
            var dto = new NotificacaoDto { Id = 13, FuncionarioId = 5, Lida = false, Mensagem = "x", Tipo = Domain.Enums.Notificacoes.TipoNotificacao.Pausa };
            var result = await _controller.Update(99, dto);
            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public async Task Delete_ReturnsNoContent()
        {
            var result = await _controller.Delete(19);
            Assert.IsType<NoContentResult>(result);
        }
    }
}