using Swashbuckle.AspNetCore.Filters;
using WorkWell.Application.DTOs.AvaliacoesEmocionais;

namespace WorkWell.API.SwaggerExamples
{
    public class MoodCheckDtoExample : IExamplesProvider<MoodCheckDto>
    {
        public MoodCheckDto GetExamples()
        {
            return new MoodCheckDto
            {
                FuncionarioId = 4,   // Carlos Silva
                Humor = 4,
                Produtivo = true,
                Estressado = false,
                DormiuBem = true
            };
        }
    }
}