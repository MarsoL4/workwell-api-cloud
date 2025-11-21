using System;
using System.ComponentModel.DataAnnotations;
using WorkWell.Domain.Entities.EmpresaOrganizacao;

namespace WorkWell.Domain.Entities.AvaliacoesEmocionais
{
    public class PerfilEmocional
    {
        public long Id { get; set; }
        public long FuncionarioId { get; set; }
        public Funcionario? Funcionario { get; set; }

        [MaxLength(50)]
        public string HumorInicial { get; set; } = null!;

        [MaxLength(500)]
        public string Rotina { get; set; } = null!;

        [MaxLength(500)]
        public string PrincipaisEstressores { get; set; } = null!;

        public DateTime DataCriacao { get; set; }
    }
}