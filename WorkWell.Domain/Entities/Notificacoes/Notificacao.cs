using System;
using System.ComponentModel.DataAnnotations;
using WorkWell.Domain.Entities.EmpresaOrganizacao;
using WorkWell.Domain.Enums.Notificacoes;

namespace WorkWell.Domain.Entities.Notificacoes
{
    public class Notificacao
    {
        public long Id { get; set; }
        public long FuncionarioId { get; set; }
        public Funcionario? Funcionario { get; set; }

        [MaxLength(1000)]
        public string Mensagem { get; set; } = null!;

        public TipoNotificacao Tipo { get; set; }
        public bool Lida { get; set; }
        public DateTime DataEnvio { get; set; }
    }
}