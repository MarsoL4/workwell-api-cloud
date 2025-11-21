using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using WorkWell.Domain.Entities.EmpresaOrganizacao;

namespace WorkWell.Domain.Entities.ApoioPsicologico
{
    public class Psicologo : Funcionario
    {
        [MaxLength(30)]
        public string Crp { get; set; } = null!;
        public List<ConsultaPsicologica> Atendimentos { get; set; } = [];
    }
}