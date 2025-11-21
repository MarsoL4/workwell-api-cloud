using Microsoft.AspNetCore.Authorization;

namespace WorkWell.API.Security
{
    public class ApiKeyAuthorizeAttribute : AuthorizeAttribute
    {
        public ApiKeyAuthorizeAttribute(params string[] roles)
        {
            if (roles is { Length: > 0 })
            {
                // GARANTE policy name padronizada, ordem alfabética
                Policy = $"ApiKey_{string.Join("_", roles.OrderBy(r => r))}";
            }
            else
            {
                Policy = "ApiKey";
            }
        }
    }
}