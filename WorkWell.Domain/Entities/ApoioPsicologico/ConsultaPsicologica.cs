using System;
using System.ComponentModel.DataAnnotations;
using WorkWell.Domain.Entities.EmpresaOrganizacao;
using WorkWell.Domain.Enums.ApoioPsicologico;

namespace WorkWell.Domain.Entities.ApoioPsicologico
{
    public class ConsultaPsicologica
    {
        public long Id { get; set; }
        public long FuncionarioId { get; set; }
        public Funcionario? Funcionario { get; set; }
        public long PsicologoId { get; set; }
        public Psicologo? Psicologo { get; set; }
        public DateTime DataConsulta { get; set; }
        public TipoConsulta Tipo { get; set; }
        public StatusConsulta Status { get; set; }

        [MaxLength(2000)]
        public string AnotacoesSigilosas { get; set; } = string.Empty;
    }
}