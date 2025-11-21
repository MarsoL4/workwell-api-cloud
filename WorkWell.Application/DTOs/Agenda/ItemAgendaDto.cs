using System;

namespace WorkWell.Application.DTOs.Agenda
{
    public class ItemAgendaDto
    {
        public long Id { get; set; }
        public long AgendaFuncionarioId { get; set; }
        public string Tipo { get; set; } = null!;
        public string Titulo { get; set; } = null!;
        public DateTime Horario { get; set; }
    }
}