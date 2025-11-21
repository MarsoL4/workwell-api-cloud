using System;
using System.Collections.Generic;

namespace WorkWell.Application.DTOs.Agenda
{
    public class AgendaFuncionarioDto
    {
        public long Id { get; set; }
        public long FuncionarioId { get; set; }
        public DateTime Data { get; set; }
        public List<ItemAgendaDto> Itens { get; set; } = [];
    }
}