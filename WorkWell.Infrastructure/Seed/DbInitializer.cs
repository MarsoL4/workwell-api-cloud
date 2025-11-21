using System;
using System.Collections.Generic;
using System.Linq;
using WorkWell.Domain.Entities.EmpresaOrganizacao;
using WorkWell.Domain.Entities.Agenda;
using WorkWell.Domain.Entities.ApoioPsicologico;
using WorkWell.Domain.Entities.AtividadesBemEstar;
using WorkWell.Domain.Entities.Enquetes;
using WorkWell.Domain.Entities.Indicadores;
using WorkWell.Domain.Entities.Notificacoes;
using WorkWell.Domain.Entities.OmbudMind;
using WorkWell.Domain.Entities.AvaliacoesEmocionais;
using WorkWell.Domain.Enums.ApoioPsicologico;
using WorkWell.Domain.Enums.AtividadesBemEstar;
using WorkWell.Domain.Enums.EmpresaOrganizacao;
using WorkWell.Domain.Enums.Notificacoes;
using WorkWell.Domain.Enums.OmbudMind;
using WorkWell.Infrastructure.Persistence;

namespace WorkWell.Infrastructure.Seed
{
    public static class DbInitializer
    {
        public static void Seed(WorkWellDbContext context)
        {
            context.Database.EnsureCreated();

            // Evita duplicidade se já existir seed
            if (context.Empresas.AsEnumerable().Any()) return;

            // --- Empresa e Setores
            var empresa = new Empresa
            {
                Nome = "Futuro do Trabalho Ltda",
                EmailAdmin = "admin@futurework.com",
                SenhaAdmin = "admin123", // Apenas para ambiente DEV/teste!
                TokenAcesso = "token-ftw-001",
                LogoUrl = "https://futurework.com/logo.png",
                CorPrimaria = "#1F77B4",
                CorSecundaria = "#FFB800",
                Missao = "Transformar o bem-estar no ambiente de trabalho.",
                PoliticaBemEstar = "Aqui o respeito e o cuidado são prioridades!",
            };
            context.Empresas.Add(empresa);
            context.SaveChanges();

            var setorRh = new Setor { Nome = "RH", Empresa = empresa };
            var setorTI = new Setor { Nome = "TI", Empresa = empresa };
            var setorFin = new Setor { Nome = "Financeiro", Empresa = empresa };
            context.Setores.AddRange(setorRh, setorTI, setorFin);
            context.SaveChanges();

            // --- Funcionários (Admin, RH, Psicólogo, Comum)
            var admin = new Funcionario
            {
                Nome = "Ana Admin",
                Email = "ana.admin@futurework.com",
                Senha = "admin123",
                TokenEmpresa = empresa.TokenAcesso,
                Cargo = Cargo.Admin,
                Ativo = true,
                Setor = setorRh
            };
            var rh = new Funcionario
            {
                Nome = "Roberta RH",
                Email = "roberta.rh@futurework.com",
                Senha = "rh123",
                TokenEmpresa = empresa.TokenAcesso,
                Cargo = Cargo.RH,
                Ativo = true,
                Setor = setorRh
            };
            var psic = new Psicologo
            {
                Nome = "Dra. Helena Alves",
                Email = "helena.alves@futurework.com",
                Senha = "psic123",
                TokenEmpresa = empresa.TokenAcesso,
                Cargo = Cargo.Psicologo,
                Ativo = true,
                Setor = setorRh,
                Crp = "06/123456"
            };
            var func = new Funcionario
            {
                Nome = "Carlos Silva",
                Email = "carlos@futurework.com",
                Senha = "func123",
                TokenEmpresa = empresa.TokenAcesso,
                Cargo = Cargo.Funcionario,
                Ativo = true,
                Setor = setorTI
            };
            context.Funcionarios.AddRange(admin, rh, psic, func);
            context.Psicologos.Add(psic);
            context.SaveChanges();

            // --- Perfil Emocional
            var perfil = new PerfilEmocional
            {
                Funcionario = func,
                HumorInicial = "Bem",
                Rotina = "Home office, faz exercícios matinais.",
                PrincipaisEstressores = "Excesso de reuniões.",
                DataCriacao = DateTime.Today
            };
            context.PerfisEmocionais.Add(perfil);
            context.SaveChanges();

            // --- Atividade Bem-Estar
            var atividade = new AtividadeBemEstar
            {
                Empresa = empresa,
                Tipo = TipoAtividade.PalestraBemEstar,
                Titulo = "Palestra - Equilíbrio Trabalho/Vida",
                Descricao = "Encontro sobre o futuro do trabalho saudável.",
                DataInicio = DateTime.Today.AddDays(1).AddHours(10),
                DataFim = DateTime.Today.AddDays(1).AddHours(11),
                SetorAlvo = setorTI
            };
            context.AtividadesBemEstar.Add(atividade);
            context.SaveChanges();

            var participacao = new ParticipacaoAtividade
            {
                Funcionario = func,
                Atividade = atividade,
                Participou = true,
                DataParticipacao = DateTime.Today.AddDays(1).AddHours(10)
            };
            context.ParticipacoesAtividade.Add(participacao);

            // --- Consulta Psicológica
            var consulta = new ConsultaPsicologica
            {
                Funcionario = func,
                Psicologo = psic,
                DataConsulta = DateTime.Today.AddDays(2).AddHours(15),
                Tipo = TipoConsulta.Online,
                Status = StatusConsulta.Agendada,
                AnotacoesSigilosas = "Primeira sessão, relata ansiedade."
            };
            context.ConsultasPsicologicas.Add(consulta);

            // --- Chat Anônimo
            var chat = new ChatAnonimo
            {
                Remetente = func,
                Psicologo = psic,
                Mensagem = "Sinto-me sobrecarregado nas tarefas.",
                DataEnvio = DateTime.Now,
                Anonimo = true // bool C# será convertido corretamente em NUMBER(1) pelo EF (mapeamento via OnModelCreating)
            };
            context.ChatsAnonimos.Add(chat);

            // --- SOS Emergência
            var sos = new SOSemergencia
            {
                Funcionario = func,
                DataAcionamento = DateTime.Now,
                Tipo = "Crise de ansiedade",
                PsicologoNotificado = true // bool C# para NUMBER(1)
            };
            context.SOSemergencias.Add(sos);

            // --- Enquete
            var enquete = new Enquete
            {
                Empresa = empresa,
                Pergunta = "Você está satisfeito com as condições de trabalho?",
                DataCriacao = DateTime.Today,
                Ativa = true // bool C# para NUMBER(1)
            };
            context.Enquetes.Add(enquete);
            context.SaveChanges();

            var resposta = new RespostaEnquete
            {
                Enquete = enquete,
                Funcionario = func,
                Resposta = "Sim"
            };
            context.RespostasEnquete.Add(resposta);

            // --- Indicadores
            var indicadores = new IndicadoresEmpresa
            {
                Empresa = empresa,
                HumorMedio = 4.1,
                AdesaoAtividadesGeral = 0.85,
                FrequenciaConsultas = 0.21,
                AdesaoPorSetor = new List<AdesaoSetor>
                {
                    new AdesaoSetor { Setor = setorTI, Adesao = 0.90 }
                }
            };
            context.IndicadoresEmpresa.Add(indicadores);

            // --- Notificação
            var notificacao = new Notificacao
            {
                Funcionario = func,
                Mensagem = "Você tem uma atividade planejada para amanhã!",
                Tipo = TipoNotificacao.AlertaAtividade,
                Lida = false, // bool C# para NUMBER(1)
                DataEnvio = DateTime.Now
            };
            context.Notificacoes.Add(notificacao);

            // --- MoodCheck
            var mood = new MoodCheck
            {
                Funcionario = func,
                Humor = 4,
                Produtivo = true,
                Estressado = false,
                DormiuBem = true,
                DataRegistro = DateTime.Today
            };
            context.MoodChecks.Add(mood);

            // --- Avaliação Profunda
            var avaliacao = new AvaliacaoProfunda
            {
                Funcionario = func,
                Gad7Score = 5,
                Phq9Score = 3,
                Interpretacao = "Ansiedade leve",
                DataRegistro = DateTime.Today
            };
            context.AvaliacoesProfundas.Add(avaliacao);

            // --- Risco Psicossocial
            var risco = new RiscoPsicossocial
            {
                Funcionario = func,
                Categoria = "Sobrecarga de trabalho",
                NivelRisco = 3,
                DataRegistro = DateTime.Today
            };
            context.RiscosPsicossociais.Add(risco);

            // --- Agenda do Funcionário (com um item)
            var agenda = new AgendaFuncionario
            {
                Funcionario = func,
                Data = DateTime.Today.AddDays(1)
            };
            context.AgendasFuncionarios.Add(agenda);
            context.SaveChanges();

            var itemAgenda = new ItemAgenda
            {
                AgendaFuncionario = agenda,
                Tipo = "atividade",
                Titulo = "Participação em palestra",
                Horario = agenda.Data.AddHours(10)
            };
            context.ItensAgenda.Add(itemAgenda);

            // --- Denúncia e Investigação
            var denuncia = new Denuncia
            {
                FuncionarioDenuncianteId = func.Id,
                Empresa = empresa,
                Tipo = TipoDenuncia.AssedioMoral,
                Descricao = "Relato de assédio moral pelo gestor.",
                DataCriacao = DateTime.Now,
                Status = StatusDenuncia.Aberta,
                CodigoRastreamento = Guid.NewGuid().ToString("N").ToUpper()[..12]
            };
            context.Denuncias.Add(denuncia);
            context.SaveChanges();

            var investigacao = new InvestigacaoDenuncia
            {
                Denuncia = denuncia,
                EquipeResponsavel = "RH",
                DataInicio = DateTime.Now,
                MedidasAdotadas = "Conversas e orientação com as partes.",
                Concluida = false // bool C# para NUMBER(1)
            };
            context.InvestigacoesDenuncia.Add(investigacao);

            context.SaveChanges();
        }
    }
}