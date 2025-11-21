using System;
using WorkWell.Domain.Enums.ApoioPsicologico;

namespace WorkWell.Application.DTOs.ApoioPsicologico
{
    public class ConsultaPsicologicaDto
    {
        public long Id { get; set; }
        public long FuncionarioId { get; set; }
        public long PsicologoId { get; set; }
        public DateTime DataConsulta { get; set; }
        public TipoConsulta Tipo { get; set; }
        public StatusConsulta Status { get; set; }
        public string AnotacoesSigilosas { get; set; } = string.Empty;
    }
}