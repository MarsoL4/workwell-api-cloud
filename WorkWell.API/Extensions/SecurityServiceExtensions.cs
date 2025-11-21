using Microsoft.AspNetCore.Authorization;
using WorkWell.API.Security;
using System.Linq;

namespace WorkWell.API.Extensions
{
    public static class SecurityServiceExtensions
    {
        public static IServiceCollection AddApiKeySecurity(this IServiceCollection services, IConfiguration config)
        {
            // Registra opções a partir do appsettings.json -> "ApiKeys" e "SuperApiKey"
            var apiKeysSection = config.GetSection("ApiKeys");
            var superApiKey = config.GetValue<string>("SuperApiKey");

            var keyDict = new Dictionary<string, string>
            {
                { "Admin", apiKeysSection.GetValue<string>("Admin") ?? "admin-key" },
                { "RH", apiKeysSection.GetValue<string>("RH") ?? "rh-key" },
                { "Psicologo", apiKeysSection.GetValue<string>("Psicologo") ?? "psicologo-key" },
                { "Funcionario", apiKeysSection.GetValue<string>("Funcionario") ?? "funcionario-key" },
            };

            services.Configure<ApiKeyOptions>(opts =>
            {
                opts.Keys = keyDict;
                opts.SuperKey = superApiKey ?? "super-key";
            });

            services.AddSingleton<IAuthorizationHandler, ApiKeyHandler>();

            // Políticas básicas (isoladas)
            services.AddAuthorization(options =>
            {
                options.AddPolicy("ApiKey", policy =>
                {
                    policy.Requirements.Add(new ApiKeyRequirement());
                });

                foreach (var role in keyDict.Keys)
                {
                    options.AddPolicy($"ApiKey_{role}", policy =>
                    {
                        policy.Requirements.Add(new ApiKeyRequirement(role));
                    });
                }

                // Adicione explicitamente todas as combinações usadas nos controllers (sempre em ordem alfabética):

                // Admin + RH
                options.AddPolicy("ApiKey_Admin_RH", policy =>
                {
                    policy.Requirements.Add(new ApiKeyRequirement("Admin", "RH"));
                });

                // Admin + Funcionario + RH
                options.AddPolicy("ApiKey_Admin_Funcionario_RH", policy =>
                {
                    policy.Requirements.Add(new ApiKeyRequirement("Admin", "Funcionario", "RH"));
                });

                // Admin + Funcionario + Psicologo + RH
                options.AddPolicy("ApiKey_Admin_Funcionario_Psicologo_RH", policy =>
                {
                    policy.Requirements.Add(new ApiKeyRequirement("Admin", "Funcionario", "Psicologo", "RH"));
                });

                // Funcionario + Psicologo + RH
                options.AddPolicy("ApiKey_Funcionario_Psicologo_RH", policy =>
                {
                    policy.Requirements.Add(new ApiKeyRequirement("Funcionario", "Psicologo", "RH"));
                });

                // Funcionario + RH
                options.AddPolicy("ApiKey_Funcionario_RH", policy =>
                {
                    policy.Requirements.Add(new ApiKeyRequirement("Funcionario", "RH"));
                });

                // Admin + Psicologo + RH
                options.AddPolicy("ApiKey_Admin_Psicologo_RH", policy =>
                {
                    policy.Requirements.Add(new ApiKeyRequirement("Admin", "Psicologo", "RH"));
                });

                // Admin + Psicologo
                options.AddPolicy("ApiKey_Admin_Psicologo", policy =>
                {
                    policy.Requirements.Add(new ApiKeyRequirement("Admin", "Psicologo"));
                });

                // Adicione combos em ordem alfabética se houver outros usos no futuro.
            });

            return services;
        }
    }
}