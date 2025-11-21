using Swashbuckle.AspNetCore.Filters;
using WorkWell.Application.DTOs.ApoioPsicologico;

namespace WorkWell.API.SwaggerExamples
{
    public class ChatAnonimoDtoExample : IExamplesProvider<ChatAnonimoDto>
    {
        public ChatAnonimoDto GetExamples()
        {
            return new ChatAnonimoDto
            {
                RemetenteId = 4,      // Carlos Silva
                PsicologoId = 3,      // Dra. Helena
                Mensagem = "Sinto-me sobrecarregado nas tarefas.", // Igual ao seed
                // Não enviar DataEnvio para POST
                Anonimo = true
            };
        }
    }
}