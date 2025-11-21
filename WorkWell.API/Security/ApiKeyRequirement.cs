using Microsoft.AspNetCore.Authorization;

namespace WorkWell.API.Security
{
    public class ApiKeyRequirement : IAuthorizationRequirement
    {
        public string[] Roles { get; }

        public ApiKeyRequirement(params string[] roles)
        {
            Roles = roles ?? [];
        }
    }
}