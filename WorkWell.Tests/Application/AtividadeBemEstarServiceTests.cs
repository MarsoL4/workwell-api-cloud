using Xunit;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;
using WorkWell.Application.Services.AtividadesBemEstar;
using WorkWell.Domain.Interfaces.AtividadesBemEstar;
using WorkWell.Application.DTOs.AtividadesBemEstar;
using WorkWell.Domain.Entities.AtividadesBemEstar;
using WorkWell.Domain.Enums.AtividadesBemEstar;

namespace WorkWell.Tests.Application
{
    public class AtividadeBemEstarServiceTests
    {
        [Fact]
        public async Task GetAllAsync_DeveRetornarAtividades()
        {
            var atividades = new List<AtividadeBemEstar>
            {
                new() { Id = 1, EmpresaId = 2, Tipo = TipoAtividade.PalestraBemEstar, Titulo = "A", Descricao = "", DataInicio = DateTime.Now, DataFim = DateTime.Now.AddHours(1) }
            };
            var mockAtivRepo = new Mock<IAtividadeBemEstarRepository>();
            var mockPartRepo = new Mock<IParticipacaoAtividadeRepository>();
            mockAtivRepo.Setup(r => r.GetAllAsync()).ReturnsAsync(atividades);

            var service = new AtividadeBemEstarService(mockAtivRepo.Object, mockPartRepo.Object);

            var result = await service.GetAllAsync();

            Assert.Single(result);
            Assert.Equal(1, result.First().Id);
        }

        [Fact]
        public async Task CreateAsync_DeveRetornarIdAtividade()
        {
            var mockAtivRepo = new Mock<IAtividadeBemEstarRepository>();
            var mockPartRepo = new Mock<IParticipacaoAtividadeRepository>();
            mockAtivRepo.Setup(r => r.AddAsync(It.IsAny<AtividadeBemEstar>()))
                .Returns(Task.CompletedTask)
                .Callback<AtividadeBemEstar>(a => a.Id = 22);

            var service = new AtividadeBemEstarService(mockAtivRepo.Object, mockPartRepo.Object);
            var dto = new AtividadeBemEstarDto
            {
                EmpresaId = 1,
                Tipo = TipoAtividade.PalestraBemEstar,
                Titulo = "Novo",
                Descricao = "",
                DataInicio = DateTime.Now,
                DataFim = DateTime.Now.AddHours(2)
            };

            var id = await service.CreateAsync(dto);

            Assert.Equal(22, id);
        }

        [Fact]
        public async Task AdicionarParticipacaoAsync_DeveRetornarIdParticipacao()
        {
            var atividadeId = 9L;
            var mockAtivRepo = new Mock<IAtividadeBemEstarRepository>();
            var mockPartRepo = new Mock<IParticipacaoAtividadeRepository>();
            mockPartRepo.Setup(r => r.AddAsync(It.IsAny<ParticipacaoAtividade>()))
                .Returns(Task.CompletedTask)
                .Callback<ParticipacaoAtividade>(p => p.Id = 55);

            var service = new AtividadeBemEstarService(mockAtivRepo.Object, mockPartRepo.Object);
            var dto = new ParticipacaoAtividadeDto
            {
                FuncionarioId = 123,
                Participou = true
            };

            var id = await service.AdicionarParticipacaoAsync(atividadeId, dto);

            Assert.Equal(55, id);
        }
    }
}