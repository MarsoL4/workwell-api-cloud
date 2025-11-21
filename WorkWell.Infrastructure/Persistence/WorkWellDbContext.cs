using Microsoft.EntityFrameworkCore;
using WorkWell.Domain.Entities.EmpresaOrganizacao;
using WorkWell.Domain.Entities.ApoioPsicologico;
using WorkWell.Domain.Entities.AtividadesBemEstar;
using WorkWell.Domain.Entities.Enquetes;
using WorkWell.Domain.Entities.Indicadores;
using WorkWell.Domain.Entities.AvaliacoesEmocionais;
using WorkWell.Domain.Entities.Notificacoes;
using WorkWell.Domain.Entities.OmbudMind;
using WorkWell.Domain.Entities.Agenda;

namespace WorkWell.Infrastructure.Persistence
{
    public class WorkWellDbContext : DbContext
    {
        public WorkWellDbContext(DbContextOptions<WorkWellDbContext> options) : base(options) { }

        public DbSet<Empresa> Empresas => Set<Empresa>();
        public DbSet<Funcionario> Funcionarios => Set<Funcionario>();
        public DbSet<Setor> Setores => Set<Setor>();
        public DbSet<Psicologo> Psicologos => Set<Psicologo>();
        public DbSet<ConsultaPsicologica> ConsultasPsicologicas => Set<ConsultaPsicologica>();
        public DbSet<ChatAnonimo> ChatsAnonimos => Set<ChatAnonimo>();
        public DbSet<SOSemergencia> SOSemergencias => Set<SOSemergencia>();
        public DbSet<AtividadeBemEstar> AtividadesBemEstar => Set<AtividadeBemEstar>();
        public DbSet<ParticipacaoAtividade> ParticipacoesAtividade => Set<ParticipacaoAtividade>();
        public DbSet<Enquete> Enquetes => Set<Enquete>();
        public DbSet<RespostaEnquete> RespostasEnquete => Set<RespostaEnquete>();
        public DbSet<IndicadoresEmpresa> IndicadoresEmpresa => Set<IndicadoresEmpresa>();
        public DbSet<AvaliacaoProfunda> AvaliacoesProfundas => Set<AvaliacaoProfunda>();
        public DbSet<MoodCheck> MoodChecks => Set<MoodCheck>();
        public DbSet<RiscoPsicossocial> RiscosPsicossociais => Set<RiscoPsicossocial>();
        public DbSet<PerfilEmocional> PerfisEmocionais => Set<PerfilEmocional>();
        public DbSet<Notificacao> Notificacoes => Set<Notificacao>();
        public DbSet<Denuncia> Denuncias => Set<Denuncia>();
        public DbSet<InvestigacaoDenuncia> InvestigacoesDenuncia => Set<InvestigacaoDenuncia>();
        public DbSet<AgendaFuncionario> AgendasFuncionarios => Set<AgendaFuncionario>();
        public DbSet<ItemAgenda> ItensAgenda => Set<ItemAgenda>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Relacionamento 1:1 Funcionario <-> PerfilEmocional
            modelBuilder.Entity<Funcionario>()
                .HasOne(f => f.PerfilEmocional)
                .WithOne(p => p.Funcionario)
                .HasForeignKey<PerfilEmocional>(p => p.FuncionarioId)
                .OnDelete(DeleteBehavior.Cascade);

            // Psicologo herda Funcionario
            modelBuilder.Entity<Psicologo>()
                .HasBaseType<Funcionario>();
        }
    }
}