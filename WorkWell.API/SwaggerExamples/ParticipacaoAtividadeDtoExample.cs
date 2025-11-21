using Swashbuckle.AspNetCore.Filters;
using WorkWell.Application.DTOs.AtividadesBemEstar;

namespace WorkWell.API.SwaggerExamples
{
    public class ParticipacaoAtividadeDtoExample : IExamplesProvider<ParticipacaoAtividadeDto>
    {
        public ParticipacaoAtividadeDto GetExamples()
        {
            // Para POST, AtividadeId é obtido via rota
            return new ParticipacaoAtividadeDto
            {
                FuncionarioId = 4,   // Carlos Silva
                Participou = true
            };
        }
    }
}