using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Linq;

namespace WorkWell.API.Filters
{
    /// <summary>
    /// Adiciona as respostas 401 e 403 em endpoints protegidos.
    /// </summary>
    public class AuthorizeResponsesOperationFilter : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            // Busca atributos em controller e método
            var hasAuthorize =
                context.MethodInfo.DeclaringType?.GetCustomAttributes(true)
                    .Any(attr => attr is Microsoft.AspNetCore.Authorization.AuthorizeAttribute || attr.GetType().Name == "ApiKeyAuthorizeAttribute") == true
                || context.MethodInfo.GetCustomAttributes(true)
                    .Any(attr => attr is Microsoft.AspNetCore.Authorization.AuthorizeAttribute || attr.GetType().Name == "ApiKeyAuthorizeAttribute");

            if (hasAuthorize)
            {
                // Adiciona 401 se não houver
                if (!operation.Responses.ContainsKey("401"))
                {
                    operation.Responses.Add("401", new OpenApiResponse
                    {
                        Description = "Não autorizado – forneça o header X-API-KEY válido"
                    });
                }
                // Adiciona 403 se não houver
                if (!operation.Responses.ContainsKey("403"))
                {
                    operation.Responses.Add("403", new OpenApiResponse
                    {
                        Description = "Proibido – sua chave de acesso não tem permissão para este endpoint"
                    });
                }
            }
        }
    }
}