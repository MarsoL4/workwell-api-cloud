using Xunit;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;
using WorkWell.Application.Services.Notificacoes;
using WorkWell.Domain.Interfaces.Notificacoes;
using WorkWell.Application.DTOs.Notificacoes;
using WorkWell.Domain.Enums.Notificacoes;

namespace WorkWell.Tests.Application
{
    public class NotificacaoServiceTests
    {
        [Fact]
        public async Task CreateAsync_DeveRetornarId()
        {
            var repo = new Mock<INotificacaoRepository>();
            repo.Setup(r => r.AddAsync(It.IsAny<WorkWell.Domain.Entities.Notificacoes.Notificacao>()))
                .Returns(Task.CompletedTask)
                .Callback<WorkWell.Domain.Entities.Notificacoes.Notificacao>(n => n.Id = 13);

            var service = new NotificacaoService(repo.Object);
            var dto = new NotificacaoDto { FuncionarioId = 1, Mensagem = "M1", Tipo = TipoNotificacao.Consulta, Lida = false };

            var id = await service.CreateAsync(dto);

            Assert.Equal(13, id);
        }

        [Fact]
        public async Task GetAllAsync_DeveRetornarNotificacoes()
        {
            var repo = new Mock<INotificacaoRepository>();
            repo.Setup(r => r.GetAllAsync()).ReturnsAsync(new List<WorkWell.Domain.Entities.Notificacoes.Notificacao>
            {
                new() { Id = 2, FuncionarioId = 5, Mensagem = "Y", Tipo = TipoNotificacao.Pausa }
            });

            var service = new NotificacaoService(repo.Object);
            var result = await service.GetAllAsync();

            Assert.Single(result);
            Assert.Equal(2, result.First().Id);
        }
    }
}