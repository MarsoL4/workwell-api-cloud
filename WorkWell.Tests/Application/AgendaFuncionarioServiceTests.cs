using Xunit;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;
using WorkWell.Application.Services.Agenda;
using WorkWell.Domain.Interfaces.Agenda;
using WorkWell.Application.DTOs.Agenda;
using WorkWell.Domain.Entities.Agenda;

namespace WorkWell.Tests.Application
{
    public class AgendaFuncionarioServiceTests
    {
        [Fact]
        public async Task GetAllAsync_DeveRetornarAgendas()
        {
            var agendas = new List<AgendaFuncionario>
            {
                new() { Id = 1, FuncionarioId = 10, Data = DateTime.Today }
            };
            var mockAgendaRepo = new Mock<IAgendaFuncionarioRepository>();
            var mockItemRepo = new Mock<IItemAgendaRepository>();
            mockAgendaRepo.Setup(r => r.GetAllAsync()).ReturnsAsync(agendas);

            var service = new AgendaFuncionarioService(mockAgendaRepo.Object, mockItemRepo.Object);

            var result = await service.GetAllAsync();

            Assert.Single(result);
            Assert.Equal(1, result.First().Id);
        }

        [Fact]
        public async Task CreateAsync_DeveRetornarIdNovo()
        {
            var mockAgendaRepo = new Mock<IAgendaFuncionarioRepository>();
            var mockItemRepo = new Mock<IItemAgendaRepository>();
            mockAgendaRepo.Setup(r => r.AddAsync(It.IsAny<AgendaFuncionario>()))
                .Returns(Task.CompletedTask)
                .Callback<AgendaFuncionario>(a => a.Id = 17);

            var service = new AgendaFuncionarioService(mockAgendaRepo.Object, mockItemRepo.Object);
            var dto = new AgendaFuncionarioDto
            {
                FuncionarioId = 21,
                Data = DateTime.Today,
                Itens = new List<ItemAgendaDto>()
            };

            var id = await service.CreateAsync(dto);

            Assert.Equal(17, id);
        }

        [Fact]
        public async Task AdicionarItemAsync_DeveRetornarIdItem()
        {
            var agendaId = 4L;
            var mockAgendaRepo = new Mock<IAgendaFuncionarioRepository>();
            var mockItemRepo = new Mock<IItemAgendaRepository>();
            mockItemRepo.Setup(r => r.AddAsync(It.IsAny<ItemAgenda>()))
                .Returns(Task.CompletedTask)
                .Callback<ItemAgenda>(i => i.Id = 111);

            var service = new AgendaFuncionarioService(mockAgendaRepo.Object, mockItemRepo.Object);
            var dto = new ItemAgendaDto
            {
                Tipo = "atividade",
                Titulo = "Teste",
                Horario = DateTime.Now
            };

            var id = await service.AdicionarItemAsync(agendaId, dto);
            Assert.Equal(111, id);
        }
    }
}