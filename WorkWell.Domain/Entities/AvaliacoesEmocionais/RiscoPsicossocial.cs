using System;
using System.ComponentModel.DataAnnotations;
using WorkWell.Domain.Entities.EmpresaOrganizacao;

namespace WorkWell.Domain.Entities.AvaliacoesEmocionais
{
    public class RiscoPsicossocial
    {
        public long Id { get; set; }
        public long FuncionarioId { get; set; }
        public Funcionario? Funcionario { get; set; }

        [MaxLength(100)]
        public string Categoria { get; set; } = null!;

        public int NivelRisco { get; set; }
        public DateTime DataRegistro { get; set; }
    }
}