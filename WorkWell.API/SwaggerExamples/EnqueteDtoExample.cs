using Swashbuckle.AspNetCore.Filters;
using WorkWell.Application.DTOs.Enquetes;

namespace WorkWell.API.SwaggerExamples
{
    public class EnqueteDtoExample : IExamplesProvider<EnqueteDto>
    {
        public EnqueteDto GetExamples()
        {
            return new EnqueteDto
            {
                EmpresaId = 1,
                Pergunta = "Você está satisfeito com as condições de trabalho?",
                Ativa = true
            };
        }
    }
}