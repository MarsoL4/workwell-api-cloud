using Xunit;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;
using WorkWell.Application.Services.Enquetes;
using WorkWell.Domain.Interfaces.Enquetes;
using WorkWell.Application.DTOs.Enquetes;

namespace WorkWell.Tests.Application
{
    public class EnqueteServiceTests
    {
        [Fact]
        public async Task CreateAsync_DeveRetornarId()
        {
            var mockEnqRepo = new Mock<IEnqueteRepository>();
            var mockRespRepo = new Mock<IRespostaEnqueteRepository>();

            mockEnqRepo.Setup(r => r.AddAsync(It.IsAny<WorkWell.Domain.Entities.Enquetes.Enquete>()))
                .Returns(Task.CompletedTask)
                .Callback<WorkWell.Domain.Entities.Enquetes.Enquete>(e => e.Id = 9);

            var service = new EnqueteService(mockEnqRepo.Object, mockRespRepo.Object);
            var dto = new EnqueteDto { EmpresaId = 1, Pergunta = "P1", Ativa = true };

            var id = await service.CreateAsync(dto);

            Assert.Equal(9, id);
        }

        [Fact]
        public async Task GetAllAsync_DeveRetornarEnquetes()
        {
            var mockEnqRepo = new Mock<IEnqueteRepository>();
            var mockRespRepo = new Mock<IRespostaEnqueteRepository>();
            mockEnqRepo.Setup(r => r.GetAllAsync()).ReturnsAsync(new List<WorkWell.Domain.Entities.Enquetes.Enquete>
            {
                new() { Id = 1, EmpresaId = 7, Pergunta = "X", Ativa = true }
            });

            var service = new EnqueteService(mockEnqRepo.Object, mockRespRepo.Object);
            var enqs = await service.GetAllAsync();

            Assert.Single(enqs);
            Assert.Equal(1, enqs.First().Id);
        }
    }
}