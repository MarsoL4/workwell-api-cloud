using Swashbuckle.AspNetCore.Filters;
using WorkWell.Application.DTOs.Notificacoes;
using WorkWell.Domain.Enums.Notificacoes;

namespace WorkWell.API.SwaggerExamples
{
    public class NotificacaoDtoExample : IExamplesProvider<NotificacaoDto>
    {
        public NotificacaoDto GetExamples()
        {
            return new NotificacaoDto
            {
                FuncionarioId = 4, // Carlos Silva
                Mensagem = "Você tem uma atividade planejada para amanhã!",
                Tipo = TipoNotificacao.AlertaAtividade,
                Lida = false
            };
        }
    }
}