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
using System.Text;

namespace WorkWell.Infrastructure.Persistence
{
    public class WorkWellDbContext : DbContext
    {
        public WorkWellDbContext(DbContextOptions<WorkWellDbContext> options) : base(options) { }

        // Empresa & organização
        public DbSet<Empresa> Empresas => Set<Empresa>();
        public DbSet<Funcionario> Funcionarios => Set<Funcionario>();
        public DbSet<Setor> Setores => Set<Setor>();

        // Apoio Psicológico
        public DbSet<Psicologo> Psicologos => Set<Psicologo>();
        public DbSet<ConsultaPsicologica> ConsultasPsicologicas => Set<ConsultaPsicologica>();
        public DbSet<ChatAnonimo> ChatsAnonimos => Set<ChatAnonimo>();
        public DbSet<SOSemergencia> SOSemergencias => Set<SOSemergencia>();

        // Atividades de Bem-Estar
        public DbSet<AtividadeBemEstar> AtividadesBemEstar => Set<AtividadeBemEstar>();
        public DbSet<ParticipacaoAtividade> ParticipacoesAtividade => Set<ParticipacaoAtividade>();

        // Enquetes
        public DbSet<Enquete> Enquetes => Set<Enquete>();
        public DbSet<RespostaEnquete> RespostasEnquete => Set<RespostaEnquete>();

        // Indicadores
        public DbSet<IndicadoresEmpresa> IndicadoresEmpresa => Set<IndicadoresEmpresa>();

        // Avaliações emocionais
        public DbSet<AvaliacaoProfunda> AvaliacoesProfundas => Set<AvaliacaoProfunda>();
        public DbSet<MoodCheck> MoodChecks => Set<MoodCheck>();
        public DbSet<RiscoPsicossocial> RiscosPsicossociais => Set<RiscoPsicossocial>();
        public DbSet<PerfilEmocional> PerfisEmocionais => Set<PerfilEmocional>();

        // Notificações
        public DbSet<Notificacao> Notificacoes => Set<Notificacao>();

        // OmbudMind
        public DbSet<Denuncia> Denuncias => Set<Denuncia>();
        public DbSet<InvestigacaoDenuncia> InvestigacoesDenuncia => Set<InvestigacaoDenuncia>();

        // Agenda
        public DbSet<AgendaFuncionario> AgendasFuncionarios => Set<AgendaFuncionario>();
        public DbSet<ItemAgenda> ItensAgenda => Set<ItemAgenda>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            foreach (var entity in modelBuilder.Model.GetEntityTypes())
            {
                // Usa nome da tabela em CAPS_SNAKE_CASE
                modelBuilder.Entity(entity.ClrType).ToTable(ToOracleSnakeCase(entity.GetTableName()!));

                // Corrige mapeamento de bool para Oracle: NUMBER(1)
                var boolProperties = entity.ClrType.GetProperties()
                    .Where(p => p.PropertyType == typeof(bool) || p.PropertyType == typeof(bool?));
                foreach (var prop in boolProperties)
                {
                    modelBuilder.Entity(entity.ClrType)
                        .Property(prop.Name)
                        .HasConversion<int>()
                        .HasColumnType("NUMBER(1)");
                }
            }
        }

        private static string ToOracleSnakeCase(string name)
        {
            if (string.IsNullOrEmpty(name))
                return name ?? string.Empty;

            // CORREÇÃO para nomes iniciados por "SOS"
            if (name.StartsWith("SOS"))
            {
                // Exemplo: SOSemergencia, SOSemergencias, SOSemergenciaFoo -> SOS_EMERGENCIA(S)_FOO
                var rest = name.Substring(3); // parte depois de "SOS"
                var sb = new StringBuilder("SOS_");
                for (int i = 0; i < rest.Length; i++)
                {
                    var c = rest[i];
                    if (char.IsUpper(c) && i > 0)
                        sb.Append('_');
                    sb.Append(char.ToUpperInvariant(c));
                }
                return sb.ToString();
            }

            // Snake case PADRÃO para os demais
            var sb2 = new StringBuilder(name.Length * 2);
            for (int i = 0; i < name.Length; i++)
            {
                var c = name[i];
                if (char.IsUpper(c) && i > 0 &&
                    (char.IsLower(name[i - 1]) || (i + 1 < name.Length && char.IsLower(name[i + 1]))))
                {
                    sb2.Append('_');
                }
                sb2.Append(char.ToUpperInvariant(c));
            }
            return sb2.ToString();
        }
    }
}