using WorkWell.Domain.Enums.Notificacoes;

namespace WorkWell.Application.DTOs.Notificacoes
{
    public class NotificacaoDto
    {
        public long Id { get; set; }
        public long FuncionarioId { get; set; }
        public string Mensagem { get; set; } = null!;
        public TipoNotificacao Tipo { get; set; }
        public bool Lida { get; set; }
        public DateTime? DataEnvio { get; set; }
    }
}