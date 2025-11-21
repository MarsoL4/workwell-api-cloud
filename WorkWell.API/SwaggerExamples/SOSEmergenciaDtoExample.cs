using Swashbuckle.AspNetCore.Filters;
using WorkWell.Application.DTOs.ApoioPsicologico;

namespace WorkWell.API.SwaggerExamples
{
    public class SOSemergenciaDtoExample : IExamplesProvider<SOSemergenciaDto>
    {
        public SOSemergenciaDto GetExamples()
        {
            return new SOSemergenciaDto
            {
                FuncionarioId = 4, // Carlos Silva
                Tipo = "Crise de ansiedade"
                // Não enviar DataAcionamento ou PsicologoNotificado no POST
            };
        }
    }
}