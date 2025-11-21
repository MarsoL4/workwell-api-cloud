using System;
using WorkWell.Domain.Entities.EmpresaOrganizacao;

namespace WorkWell.Domain.Entities.AvaliacoesEmocionais
{
    public class MoodCheck
    {
        public long Id { get; set; }
        public long FuncionarioId { get; set; }
        public Funcionario? Funcionario { get; set; }
        public int Humor { get; set; }
        public bool Produtivo { get; set; }
        public bool Estressado { get; set; }
        public bool DormiuBem { get; set; }
        public DateTime DataRegistro { get; set; }
    }
}