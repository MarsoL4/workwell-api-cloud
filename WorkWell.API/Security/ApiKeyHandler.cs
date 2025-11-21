using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;
using System.Security.Claims;

namespace WorkWell.API.Security
{
    public class ApiKeyHandler : AuthorizationHandler<ApiKeyRequirement>
    {
        private readonly ApiKeyOptions _options;

        public ApiKeyHandler(IOptions<ApiKeyOptions> options)
        {
            _options = options.Value;
        }

        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, ApiKeyRequirement requirement)
        {
            if (context.Resource is not HttpContext httpContext)
            {
                // API Key auth é feita via middleware, nunca diretamente
                return Task.CompletedTask;
            }

            // Já autenticado via ApiKeyMiddleware
            var apiKeyRole = httpContext.Items["ApiKeyRole"] as string;

            if (apiKeyRole == null)
                return Task.CompletedTask; // não autenticado

            // Se a super chave, passa qualquer role
            if (apiKeyRole == "Super")
            {
                context.Succeed(requirement);
                return Task.CompletedTask;
            }

            // Sucesso se qualquer role requerida está na policy
            if (requirement.Roles.Length == 0 || requirement.Roles.Contains(apiKeyRole))
            {
                context.Succeed(requirement);
            }
            else
            {
                context.Fail(); // <<< Necessário para retornar 403 em vez de 401
            }

            return Task.CompletedTask;
        }
    }
}