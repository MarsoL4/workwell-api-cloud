using System;
using System.ComponentModel.DataAnnotations;

namespace WorkWell.Domain.Entities.Agenda
{
    public class ItemAgenda
    {
        public long Id { get; set; }
        public long AgendaFuncionarioId { get; set; }
        public AgendaFuncionario? AgendaFuncionario { get; set; }

        [MaxLength(30)]
        public string Tipo { get; set; } = null!;

        [MaxLength(100)]
        public string Titulo { get; set; } = null!;
        public DateTime Horario { get; set; }
    }
}