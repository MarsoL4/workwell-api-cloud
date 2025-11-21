using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using System.Reflection;
using WorkWell.API.Filters; 

namespace WorkWell.API.Extensions
{
    public static class SwaggerExtensions
    {
        public static IServiceCollection AddWorkWellSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(options =>
            {
                // Versão e summary
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "WorkWell API",
                    Version = "v1",
                    Description = "API REST para gestão de bem-estar corporativo e apoio psicológico."
                });

                // Lê os comentários XML para os summaries
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                if (File.Exists(xmlPath))
                    options.IncludeXmlComments(xmlPath, includeControllerXmlComments: true);

                // Adiciona anotações para summaries/response codes
                options.EnableAnnotations();

                // Adiciona os exemplos customizados de payload
                options.ExampleFilters();

                // Adiciona definição de segurança por ApiKey
                options.AddSecurityDefinition("ApiKey", new OpenApiSecurityScheme
                {
                    Description = "Chave de autenticação no header X-API-KEY",
                    Name = "X-API-KEY",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "ApiKeyScheme"
                });

                options.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "ApiKey"
                            }
                        },
                        new List<string>()
                    }
                });

                // ADICIONA filtro de documentação global para respostas 401 e 403
                options.OperationFilter<AuthorizeResponsesOperationFilter>();
            });

            // Registra exemplos do assembly
            services.AddSwaggerExamplesFromAssemblyOf<Program>();
            return services;
        }
    }
}