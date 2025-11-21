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
using AdesaoSetor = WorkWell.Domain.Entities.Indicadores.AdesaoSetor;

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

            // Mapear todas as tabelas para os nomes corretos do banco (singular)
            // O EF Core pluraliza por padrão, mas o script SQL usa nomes no singular
            modelBuilder.Entity<Empresa>().ToTable("Empresa");
            modelBuilder.Entity<Setor>().ToTable("Setor");
            modelBuilder.Entity<Funcionario>().ToTable("Funcionario");
            modelBuilder.Entity<PerfilEmocional>().ToTable("PerfilEmocional");
            modelBuilder.Entity<AtividadeBemEstar>().ToTable("AtividadeBemEstar");
            modelBuilder.Entity<ParticipacaoAtividade>().ToTable("ParticipacaoAtividade");
            modelBuilder.Entity<AgendaFuncionario>().ToTable("AgendaFuncionario");
            modelBuilder.Entity<ItemAgenda>().ToTable("ItemAgenda");
            modelBuilder.Entity<Enquete>().ToTable("Enquete");
            modelBuilder.Entity<RespostaEnquete>().ToTable("RespostaEnquete");
            modelBuilder.Entity<IndicadoresEmpresa>().ToTable("IndicadoresEmpresa");
            modelBuilder.Entity<Notificacao>().ToTable("Notificacao");
            modelBuilder.Entity<ChatAnonimo>().ToTable("ChatAnonimo");
            modelBuilder.Entity<ConsultaPsicologica>().ToTable("ConsultaPsicologica");
            modelBuilder.Entity<SOSemergencia>().ToTable("SOSemergencia");
            modelBuilder.Entity<Denuncia>().ToTable("Denuncia");
            modelBuilder.Entity<InvestigacaoDenuncia>().ToTable("InvestigacaoDenuncia");
            modelBuilder.Entity<AvaliacaoProfunda>().ToTable("AvaliacaoProfunda");
            modelBuilder.Entity<MoodCheck>().ToTable("MoodCheck");
            modelBuilder.Entity<RiscoPsicossocial>().ToTable("RiscoPsicossocial");

            // Relacionamento 1:1 Funcionario <-> PerfilEmocional
            modelBuilder.Entity<Funcionario>()
                .HasOne(f => f.PerfilEmocional)
                .WithOne(p => p.Funcionario)
                .HasForeignKey<PerfilEmocional>(p => p.FuncionarioId)
                .OnDelete(DeleteBehavior.Cascade);

            // Psicologo herda Funcionario - Table per Type (TPT)
            // Alinhado com script-bd.sql que cria tabela separada Psicologo
            modelBuilder.Entity<Psicologo>()
                .ToTable("Psicologo")
                .HasBaseType<Funcionario>();

            // AdesaoSetor - relacionamento com IndicadoresEmpresa
            modelBuilder.Entity<IndicadoresEmpresa>()
                .HasMany(i => i.AdesaoPorSetor)
                .WithOne()
                .HasForeignKey("IndicadoresEmpresaId")
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<AdesaoSetor>()
                .ToTable("AdesaoSetor")
                .HasKey(a => a.Id);

            modelBuilder.Entity<AdesaoSetor>()
                .HasOne(a => a.Setor)
                .WithMany()
                .HasForeignKey(a => a.SetorId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}