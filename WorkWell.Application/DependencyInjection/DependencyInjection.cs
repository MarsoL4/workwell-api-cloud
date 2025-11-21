using Microsoft.Extensions.DependencyInjection;
using WorkWell.Application.Services.EmpresaOrganizacao;
using WorkWell.Application.Services.Agenda;
using WorkWell.Application.Services.ApoioPsicologico;
using WorkWell.Application.Services.AtividadesBemEstar;
using WorkWell.Application.Services.Enquetes;
using WorkWell.Application.Services.Indicadores;
using WorkWell.Application.Services.Notificacoes;
using WorkWell.Application.Services.OmbudMind;
using WorkWell.Application.Services.AvaliacoesEmocionais;

namespace WorkWell.Application.DependencyInjection
{
    public static class ApplicationDependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            // Empresa e Organização
            services.AddScoped<IEmpresaService, EmpresaService>();
            services.AddScoped<IFuncionarioService, FuncionarioService>();
            services.AddScoped<ISetorService, SetorService>();

            // Agenda
            services.AddScoped<IAgendaFuncionarioService, AgendaFuncionarioService>();

            // Apoio Psicológico
            services.AddScoped<IConsultaPsicologicaService, ConsultaPsicologicaService>();
            services.AddScoped<IChatAnonimoService, ChatAnonimoService>();
            services.AddScoped<IPsicologoService, PsicologoService>();
            services.AddScoped<ISOSemergenciaService, SOSemergenciaService>();

            // Atividades Bem-Estar
            services.AddScoped<IAtividadeBemEstarService, AtividadeBemEstarService>();

            // Enquetes
            services.AddScoped<IEnqueteService, EnqueteService>();

            // Indicadores
            services.AddScoped<IIndicadoresEmpresaService, IndicadoresEmpresaService>();

            // Notificações
            services.AddScoped<INotificacaoService, NotificacaoService>();

            // OmbudMind
            services.AddScoped<IDenunciaService, DenunciaService>();

            // Avaliações Emocionais
            services.AddScoped<IAvaliacaoProfundaService, AvaliacaoProfundaService>();
            services.AddScoped<IMoodCheckService, MoodCheckService>();
            services.AddScoped<IPerfilEmocionalService, PerfilEmocionalService>();
            services.AddScoped<IRiscoPsicossocialService, RiscoPsicossocialService>();

            return services;
        }
    }
}