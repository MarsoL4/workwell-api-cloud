using Swashbuckle.AspNetCore.Filters;
using WorkWell.Application.DTOs.AvaliacoesEmocionais;

namespace WorkWell.API.SwaggerExamples
{
    public class RiscoPsicossocialDtoExample : IExamplesProvider<RiscoPsicossocialDto>
    {
        public RiscoPsicossocialDto GetExamples()
        {
            return new RiscoPsicossocialDto
            {
                FuncionarioId = 4, // Carlos Silva
                Categoria = "Sobrecarga de trabalho",
                NivelRisco = 3
            };
        }
    }
}