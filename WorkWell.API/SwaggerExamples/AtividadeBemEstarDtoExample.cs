using Swashbuckle.AspNetCore.Filters;
using WorkWell.Application.DTOs.AtividadesBemEstar;
using WorkWell.Domain.Enums.AtividadesBemEstar;
using System;

namespace WorkWell.API.SwaggerExamples
{
    public class AtividadeBemEstarDtoExample : IExamplesProvider<AtividadeBemEstarDto>
    {
        public AtividadeBemEstarDto GetExamples()
        {
            return new AtividadeBemEstarDto
            {
                // Para POST, não enviar Id nem DataInicio/DataFim, mas para PUT exemplos pode usar:
                EmpresaId = 1,
                Tipo = TipoAtividade.PalestraBemEstar,
                Titulo = "Palestra - Equilíbrio Trabalho/Vida",
                Descricao = "Encontro sobre o futuro do trabalho saudável.",
                DataInicio = DateTime.Today.AddDays(1).AddHours(10), // Igual ao seed
                DataFim = DateTime.Today.AddDays(1).AddHours(11),
                SetorAlvoId = 2 // TI
            };
        }
    }
}