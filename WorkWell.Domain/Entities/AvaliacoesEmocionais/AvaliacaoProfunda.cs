using System;
using System.ComponentModel.DataAnnotations;
using WorkWell.Domain.Entities.EmpresaOrganizacao;

namespace WorkWell.Domain.Entities.AvaliacoesEmocionais
{
    public class AvaliacaoProfunda
    {
        public long Id { get; set; }
        public long FuncionarioId { get; set; }
        public Funcionario? Funcionario { get; set; }
        public int Gad7Score { get; set; }
        public int? Phq9Score { get; set; }

        [MaxLength(1000)]
        public string Interpretacao { get; set; } = null!;

        public DateTime DataRegistro { get; set; }
    }
}