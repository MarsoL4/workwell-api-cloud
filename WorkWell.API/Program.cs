using Microsoft.AspNetCore.Mvc.ApiExplorer;
using WorkWell.API.Extensions;
using WorkWell.API.Filters;
using WorkWell.API.HealthChecks;
using WorkWell.API.Security;
using WorkWell.Application.DependencyInjection;
using WorkWell.Infrastructure.Configurations;

var builder = WebApplication.CreateBuilder(args);

// Dummy Auth para API Key
builder.Services.AddAuthentication("ApiKeyScheme")
    .AddScheme<Microsoft.AspNetCore.Authentication.AuthenticationSchemeOptions, DummyAuthenticationHandler>("ApiKeyScheme", _ => { });

// Add Infrastructure (DbContext e Repositories) - Azure SQL
builder.Services.AddInfrastructure(builder.Configuration);

// Add Application services
builder.Services.AddApplication();

// Add controllers with global model validation filter
builder.Services.AddControllers(opt =>
{
    opt.Filters.Add<ModelValidationFilter>();
});

// Swagger/OpenAPI + EXAMPLES
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddWorkWellSwagger();

// Versioning
builder.Services.AddWorkWellApiVersioning();

// Health Check
builder.Services.AddWorkWellHealthChecks();

// API Key Security
builder.Services.AddApiKeySecurity(builder.Configuration);

var app = builder.Build();

// ApiVersionDescriptionProvider necess�rio para o Swagger UI suportar versionamento
var provider = app.Services.GetRequiredService<IApiVersionDescriptionProvider>();

// Swagger UI com Versionamento
app.UseSwagger();
app.UseSwaggerUI(options =>
{
    foreach (var description in provider.ApiVersionDescriptions)
    {
        options.SwaggerEndpoint($"/swagger/{description.GroupName}/swagger.json", $"WorkWell API {description.GroupName.ToUpperInvariant()}");
    }
});

// Global Error + API Key Middlewares
app.UseMiddleware<WorkWell.API.Middleware.ErrorHandlingMiddleware>();
app.UseMiddleware<ApiKeyMiddleware>();

app.UseAuthentication();
app.UseAuthorization();

// Middleware para personalizar resposta 403 Forbidden (perm role)
app.Use(async (context, next) =>
{
    await next();

    if (context.Response.StatusCode == 403 && !context.Response.HasStarted)
    {
        context.Response.ContentType = "application/json";
        await context.Response.WriteAsync("{\"statusCode\":403,\"message\":\"Permiss�o insuficiente para este endpoint\"}");
    }
});

app.UseHttpsRedirection();

// Health check endpoint
app.MapHealthChecks("/health");
app.MapControllers();

app.Run();