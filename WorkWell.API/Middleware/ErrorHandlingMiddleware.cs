using System.Net;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace WorkWell.API.Middleware
{
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ErrorHandlingMiddleware> _logger;

        public ErrorHandlingMiddleware(RequestDelegate next, ILogger<ErrorHandlingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);

                // Se já retornou 400 BadRequest devido a erro de model validation capturado por filtro
                if (context.Response.StatusCode == 400 &&
                    context.Items.TryGetValue("ModelStateErrors", out var errorsObj) &&
                    errorsObj is IDictionary<string, string[]> errors)
                {
                    // O contexto de response NUNCA deveria ser nulo nesse ponto
                    var errorResult = new
                    {
                        StatusCode = 400,
                        Message = "Ocorreram erros de validação.",
                        Errors = errors
                    };

                    // Limpa a resposta anterior para garantir saída JSON padronizada
                    context.Response.Clear();
                    context.Response.StatusCode = 400;
                    context.Response.ContentType = "application/json";
                    var json = JsonSerializer.Serialize(errorResult);
                    await context.Response.WriteAsync(json);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro não tratado: {Message}", ex.Message);

                // Monta mensagem detalhada incluindo todas as inner exceptions (recursivamente)
                string GetAllMessages(Exception e)
                {
                    if (e.InnerException == null)
                        return $"{e.GetType().Name}: {e.Message}";
                    return $"{e.GetType().Name}: {e.Message} | Inner: {GetAllMessages(e.InnerException)}";
                }

                var error = new
                {
                    StatusCode = 500,
                    Message = "Ocorreu um erro inesperado.",
                    Detail = GetAllMessages(ex),
                    TraceId = context.TraceIdentifier
                };
                context.Response.Clear();
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                context.Response.ContentType = "application/json";
                await context.Response.WriteAsync(JsonSerializer.Serialize(error));
            }
        }
    }

    public static class ErrorHandlingExtensions
    {
        public static IDictionary<string, string[]> GetErrors(this ModelStateDictionary modelState)
        {
            return modelState
                .Where(static e => e.Value != null && e.Value.Errors.Count > 0)
                .ToDictionary(
                    kvp => kvp.Key,
                    static kvp => kvp.Value!.Errors.Select(e => e.ErrorMessage).ToArray()
                );
        }
    }
}