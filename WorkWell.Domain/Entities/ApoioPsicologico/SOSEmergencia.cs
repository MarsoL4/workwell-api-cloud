using System;
using System.ComponentModel.DataAnnotations;
using WorkWell.Domain.Entities.EmpresaOrganizacao;

namespace WorkWell.Domain.Entities.ApoioPsicologico
{
    public class SOSemergencia
    {
        public long Id { get; set; }
        public long FuncionarioId { get; set; }
        public Funcionario? Funcionario { get; set; }
        public DateTime DataAcionamento { get; set; }

        [MaxLength(100)]
        public string Tipo { get; set; } = null!;

        public bool PsicologoNotificado { get; set; }
    }
}