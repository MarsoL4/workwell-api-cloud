using Xunit;
using Moq;
using Microsoft.AspNetCore.Mvc;
using WorkWell.API.Controllers;
using WorkWell.Application.Services.Enquetes;
using WorkWell.Application.DTOs.Enquetes;
using WorkWell.Application.DTOs.Paginacao;
using System.Collections.Generic;

namespace WorkWell.Tests.Controllers
{
    public class EnqueteControllerTests
    {
        private readonly Mock<IEnqueteService> _serviceMock;
        private readonly EnqueteController _controller;

        public EnqueteControllerTests()
        {
            _serviceMock = new Mock<IEnqueteService>();
            _controller = new EnqueteController(_serviceMock.Object);
        }

        [Fact]
        public async Task GetAllPaged_ReturnsOk()
        {
            _serviceMock.Setup(x => x.GetAllPagedAsync(1, 10)).ReturnsAsync(new PagedResultDto<EnqueteDto>
            {
                Page = 1,
                PageSize = 10,
                TotalCount = 1
            });
            var result = await _controller.GetAllPaged(1, 10);
            Assert.IsType<OkObjectResult>(result.Result);
        }

        [Fact]
        public async Task GetById_ReturnsOk_IfFound()
        {
            _serviceMock.Setup(x => x.GetByIdAsync(15)).ReturnsAsync(new EnqueteDto { Id = 15 });
            var result = await _controller.GetById(15);
            var ok = Assert.IsType<OkObjectResult>(result.Result);
            Assert.IsType<EnqueteDto>(ok.Value);
        }

        [Fact]
        public async Task GetById_ReturnsNotFound_IfMissing()
        {
            _serviceMock.Setup(x => x.GetByIdAsync(10)).ReturnsAsync((EnqueteDto?)null);
            var result = await _controller.GetById(10);
            Assert.IsType<NotFoundObjectResult>(result.Result);
        }

        [Fact]
        public async Task Create_ReturnsCreated()
        {
            _serviceMock.Setup(x => x.CreateAsync(It.IsAny<EnqueteDto>())).ReturnsAsync(5);
            _serviceMock.Setup(x => x.GetByIdAsync(5)).ReturnsAsync(new EnqueteDto { Id = 5, EmpresaId = 1, Pergunta = "Pergunta", Ativa = true });
            var result = await _controller.Create(new EnqueteDto { EmpresaId = 1, Pergunta = "Pergunta", Ativa = true });
            var created = Assert.IsType<CreatedAtActionResult>(result.Result);
            var createdDto = Assert.IsType<EnqueteDto>(created.Value);
            Assert.Equal(5L, createdDto.Id);
        }

        [Fact]
        public async Task Update_ReturnsNoContent_WhenOk()
        {
            var dto = new EnqueteDto { Id = 7, EmpresaId = 1, Pergunta = "", Ativa = true };
            var result = await _controller.Update(7, dto);
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task Update_ReturnsBadRequest_IfIdsDiffer()
        {
            var dto = new EnqueteDto { Id = 91, EmpresaId = 1, Pergunta = "" };
            var result = await _controller.Update(7, dto);
            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public async Task Delete_ReturnsNoContent()
        {
            var result = await _controller.Delete(111);
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task GetRespostas_ReturnsOk()
        {
            _serviceMock.Setup(x => x.GetRespostasAsync(11)).ReturnsAsync(new List<RespostaEnqueteDto>());
            var result = await _controller.GetRespostas(11);
            Assert.IsType<OkObjectResult>(result.Result);
        }

        [Fact]
        public async Task AdicionarResposta_ReturnsCreated()
        {
            _serviceMock.Setup(x => x.AdicionarRespostaAsync(11, It.IsAny<RespostaEnqueteDto>())).ReturnsAsync(78);
            _serviceMock.Setup(x => x.GetRespostasAsync(11)).ReturnsAsync(new List<RespostaEnqueteDto> { new RespostaEnqueteDto { Id = 78, EnqueteId = 11, FuncionarioId = 9, Resposta = "resposta" } });
            var dto = new RespostaEnqueteDto { EnqueteId = 11, FuncionarioId = 9, Resposta = "resposta" };
            var result = await _controller.AdicionarResposta(11, dto);
            var created = Assert.IsType<CreatedAtActionResult>(result.Result);
            var rspDto = Assert.IsType<RespostaEnqueteDto>(created.Value);
            Assert.Equal(78L, rspDto.Id);
        }
    }
}