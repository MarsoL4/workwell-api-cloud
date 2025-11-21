using Swashbuckle.AspNetCore.Filters;
using WorkWell.Application.DTOs.Agenda;
using System;

namespace WorkWell.API.SwaggerExamples
{
    public class ItemAgendaDtoExample : IExamplesProvider<ItemAgendaDto>
    {
        public ItemAgendaDto GetExamples()
        {
            return new ItemAgendaDto
            {
                // Para POST, não informar Id ou AgendaFuncionarioId, o id da Agenda será passado pela rota
                Tipo = "atividade",
                Titulo = "Participação em palestra",
                Horario = DateTime.Today.AddDays(1).AddHours(10)
            };
        }
    }
}