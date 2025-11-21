using Swashbuckle.AspNetCore.Filters;
using WorkWell.Application.DTOs.Agenda;
using System;
using System.Collections.Generic;

namespace WorkWell.API.SwaggerExamples
{
    public class AgendaFuncionarioDtoExample : IExamplesProvider<AgendaFuncionarioDto>
    {
        public AgendaFuncionarioDto GetExamples()
        {
            return new AgendaFuncionarioDto
            {
                // No POST não preencher Id nem Data.
                FuncionarioId = 4, // Carlos Silva (criado no seed)
                // Data será preenchida automaticamente se não informado, mas como no seed a agenda tem Data = DateTime.Today.AddDays(1), pode ser usado para update/teste.
                Data = DateTime.Today.AddDays(1),
                Itens = new List<ItemAgendaDto>
                {
                    new ItemAgendaDto
                    {
                        // Para POST criar item, Id e Data não são informados!
                        Tipo = "atividade",
                        Titulo = "Participação em palestra",
                        Horario = DateTime.Today.AddDays(1).AddHours(10)
                    }
                }
            };
        }
    }
}