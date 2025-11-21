using Xunit;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;
using WorkWell.Application.Services.ApoioPsicologico;
using WorkWell.Domain.Interfaces.ApoioPsicologico;
using WorkWell.Application.DTOs.ApoioPsicologico;
using WorkWell.Domain.Entities.ApoioPsicologico;

namespace WorkWell.Tests.Application
{
    public class ChatAnonimoServiceTests
    {
        [Fact]
        public async Task GetAllAsync_DeveRetornarChats()
        {
            var chats = new List<ChatAnonimo>
            {
                new() { Id = 1, PsicologoId = 10, Mensagem = "a", DataEnvio = DateTime.Now, Anonimo = true }
            };
            var mockRepo = new Mock<IChatAnonimoRepository>();
            mockRepo.Setup(r => r.GetAllAsync()).ReturnsAsync(chats);

            var service = new ChatAnonimoService(mockRepo.Object);

            var result = await service.GetAllAsync();

            Assert.Single(result);
            Assert.Equal(1, result.First().Id);
        }

        [Fact]
        public async Task CreateAsync_DeveRetornarIdChat()
        {
            var mockRepo = new Mock<IChatAnonimoRepository>();
            mockRepo.Setup(r => r.AddAsync(It.IsAny<ChatAnonimo>()))
                .Returns(Task.CompletedTask)
                .Callback<ChatAnonimo>(c => c.Id = 33);

            var service = new ChatAnonimoService(mockRepo.Object);
            var dto = new ChatAnonimoDto
            {
                PsicologoId = 10,
                Mensagem = "oi",
                Anonimo = true
            };

            var id = await service.CreateAsync(dto);
            Assert.Equal(33, id);
        }
    }
}