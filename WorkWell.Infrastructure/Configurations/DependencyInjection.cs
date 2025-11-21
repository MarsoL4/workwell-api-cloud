using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WorkWell.Infrastructure.Persistence;

using WorkWell.Domain.Interfaces.EmpresaOrganizacao;
using WorkWell.Infrastructure.Repositories.EmpresaOrganizacao;
using WorkWell.Domain.Interfaces.Agenda;
using WorkWell.Infrastructure.Repositories.Agenda;
using WorkWell.Domain.Interfaces.ApoioPsicologico;
using WorkWell.Infrastructure.Repositories.ApoioPsicologico;
using WorkWell.Domain.Interfaces.AtividadesBemEstar;
using WorkWell.Infrastructure.Repositories.AtividadesBemEstar;
using WorkWell.Domain.Interfaces.Enquetes;
using WorkWell.Infrastructure.Repositories.Enquetes;
using WorkWell.Domain.Interfaces.Indicadores;
using WorkWell.Infrastructure.Repositories.Indicadores;
using WorkWell.Domain.Interfaces.Notificacoes;
using WorkWell.Infrastructure.Repositories.Notificacoes;
using WorkWell.Domain.Interfaces.OmbudMind;
using WorkWell.Infrastructure.Repositories.OmbudMind;
using WorkWell.Domain.Interfaces.AvaliacoesEmocionais;
using WorkWell.Infrastructure.Repositories.AvaliacoesEmocionais;

namespace WorkWell.Infrastructure.Configurations
{
    public static class InfrastructureDependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            // Use a connection string named "Oracle"
            services.AddDbContext<WorkWellDbContext>(options =>
                options.UseOracle(configuration.GetConnectionString("Oracle")));

            // Empresa & Organização
            services.AddScoped<IEmpresaRepository, EmpresaRepository>();
            services.AddScoped<IFuncionarioRepository, FuncionarioRepository>();
            services.AddScoped<ISetorRepository, SetorRepository>();

            // Agenda
            services.AddScoped<IAgendaFuncionarioRepository, AgendaFuncionarioRepository>();
            services.AddScoped<IItemAgendaRepository, ItemAgendaRepository>();

            // Apoio Psicológico
            services.AddScoped<IConsultaPsicologicaRepository, ConsultaPsicologicaRepository>();
            services.AddScoped<IChatAnonimoRepository, ChatAnonimoRepository>();
            services.AddScoped<IPsicologoRepository, PsicologoRepository>();
            services.AddScoped<ISOSemergenciaRepository, SOSemergenciaRepository>();

            // Atividades Bem-Estar
            services.AddScoped<IAtividadeBemEstarRepository, AtividadeBemEstarRepository>();
            services.AddScoped<IParticipacaoAtividadeRepository, ParticipacaoAtividadeRepository>();

            // Enquetes
            services.AddScoped<IEnqueteRepository, EnqueteRepository>();
            services.AddScoped<IRespostaEnqueteRepository, RespostaEnqueteRepository>();

            // Indicadores
            services.AddScoped<IIndicadoresEmpresaRepository, IndicadoresEmpresaRepository>();

            // Notificações
            services.AddScoped<INotificacaoRepository, NotificacaoRepository>();

            // OmbudMind
            services.AddScoped<IDenunciaRepository, DenunciaRepository>();
            services.AddScoped<IInvestigacaoDenunciaRepository, InvestigacaoDenunciaRepository>();

            // Avaliações Emocionais
            services.AddScoped<IAvaliacaoProfundaRepository, AvaliacaoProfundaRepository>();
            services.AddScoped<IMoodCheckRepository, MoodCheckRepository>();
            services.AddScoped<IPerfilEmocionalRepository, PerfilEmocionalRepository>();
            services.AddScoped<IRiscoPsicossocialRepository, RiscoPsicossocialRepository>();

            return services;
        }
    }
}