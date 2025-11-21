using Swashbuckle.AspNetCore.Filters;
using WorkWell.Application.DTOs.AvaliacoesEmocionais;

namespace WorkWell.API.SwaggerExamples
{
    public class AvaliacaoProfundaDtoExample : IExamplesProvider<AvaliacaoProfundaDto>
    {
        public AvaliacaoProfundaDto GetExamples()
        {
            // Em POST, não incluir Id nem DataRegistro
            return new AvaliacaoProfundaDto
            {
                FuncionarioId = 4, // Carlos Silva do seed
                Gad7Score = 5,     // igual ao dado do seed para coerência
                Phq9Score = 3,
                Interpretacao = "Ansiedade leve"
            };
        }
    }
}