using Swashbuckle.AspNetCore.Filters;
using WorkWell.Application.DTOs.ApoioPsicologico;
using WorkWell.Domain.Enums.ApoioPsicologico;
using System;

namespace WorkWell.API.SwaggerExamples
{
    public class ConsultaPsicologicaDtoExample : IExamplesProvider<ConsultaPsicologicaDto>
    {
        public ConsultaPsicologicaDto GetExamples()
        {
            return new ConsultaPsicologicaDto
            {
                FuncionarioId = 4,            // Carlos Silva
                PsicologoId = 3,              // Dra. Helena
                DataConsulta = DateTime.Today.AddDays(2).AddHours(15), // igual seed
                Tipo = TipoConsulta.Online,
                Status = StatusConsulta.Agendada,
                AnotacoesSigilosas = "Primeira sessão, relata ansiedade."
            };
        }
    }
}