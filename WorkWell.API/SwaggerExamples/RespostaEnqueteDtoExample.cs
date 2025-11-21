using Swashbuckle.AspNetCore.Filters;
using WorkWell.Application.DTOs.Enquetes;

namespace WorkWell.API.SwaggerExamples
{
    public class RespostaEnqueteDtoExample : IExamplesProvider<RespostaEnqueteDto>
    {
        public RespostaEnqueteDto GetExamples()
        {
            // Para POST, EnqueteId é informado na rota!
            return new RespostaEnqueteDto
            {
                FuncionarioId = 4, // Carlos Silva
                Resposta = "Sim"
            };
        }
    }
}