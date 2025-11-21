using Swashbuckle.AspNetCore.Filters;
using WorkWell.Application.DTOs.OmbudMind;
using System;

namespace WorkWell.API.SwaggerExamples
{
    public class InvestigacaoDenunciaDtoExample : IExamplesProvider<InvestigacaoDenunciaDto>
    {
        public InvestigacaoDenunciaDto GetExamples()
        {
            return new InvestigacaoDenunciaDto
            {
                // Para POST, não informar Id ou DataFim
                DenunciaId = 1, // Única denúncia criada no seed
                EquipeResponsavel = "RH",
                DataInicio = DateTime.Today,
                MedidasAdotadas = "Conversas e orientação com as partes.",
                Concluida = false
            };
        }
    }
}