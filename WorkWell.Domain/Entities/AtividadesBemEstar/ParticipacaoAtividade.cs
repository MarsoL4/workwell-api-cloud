using System;
using WorkWell.Domain.Entities.EmpresaOrganizacao;

namespace WorkWell.Domain.Entities.AtividadesBemEstar
{
    public class ParticipacaoAtividade
    {
        public long Id { get; set; }
        public long FuncionarioId { get; set; }
        public Funcionario? Funcionario { get; set; }
        public long AtividadeId { get; set; }
        public AtividadeBemEstar? Atividade { get; set; }
        public bool Participou { get; set; }
        public DateTime DataParticipacao { get; set; }
    }
}