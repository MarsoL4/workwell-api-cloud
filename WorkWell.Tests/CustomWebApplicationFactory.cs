using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using WorkWell.Infrastructure.Persistence;
using WorkWell.Infrastructure.Seed;

namespace WorkWell.Tests
{
    public class CustomWebApplicationFactory<TStartup> : WebApplicationFactory<TStartup> where TStartup : class
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                // Remove contexto Oracle se registrado anteriormente (produção)
                var descriptor = services.SingleOrDefault(
                    d => d.ServiceType == typeof(DbContextOptions<WorkWellDbContext>));
                if (descriptor != null)
                    services.Remove(descriptor);

                // Adiciona contexto InMemory para testes
                services.AddDbContext<WorkWellDbContext>(options =>
                {
                    options.UseInMemoryDatabase("WorkWellTestDb");
                });

                // Seed e limpeza
                using var scope = services.BuildServiceProvider().CreateScope();
                var db = scope.ServiceProvider.GetRequiredService<WorkWellDbContext>();
                db.Database.EnsureDeleted();
                db.Database.EnsureCreated();

                // SEED opcional, igual ambiente real
                DbInitializer.Seed(db);
            });
        }
    }
}