using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WorkWell.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EMPRESAS",
                columns: table => new
                {
                    Id = table.Column<long>(type: "NUMBER(19)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    Nome = table.Column<string>(type: "NVARCHAR2(100)", maxLength: 100, nullable: false),
                    EmailAdmin = table.Column<string>(type: "NVARCHAR2(200)", maxLength: 200, nullable: false),
                    SenhaAdmin = table.Column<string>(type: "NVARCHAR2(256)", maxLength: 256, nullable: false),
                    TokenAcesso = table.Column<string>(type: "NVARCHAR2(100)", maxLength: 100, nullable: false),
                    LogoUrl = table.Column<string>(type: "NVARCHAR2(300)", maxLength: 300, nullable: false),
                    CorPrimaria = table.Column<string>(type: "NVARCHAR2(10)", maxLength: 10, nullable: false),
                    CorSecundaria = table.Column<string>(type: "NVARCHAR2(10)", maxLength: 10, nullable: false),
                    Missao = table.Column<string>(type: "NVARCHAR2(1000)", maxLength: 1000, nullable: false),
                    PoliticaBemEstar = table.Column<string>(type: "NVARCHAR2(1000)", maxLength: 1000, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EMPRESAS", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ENQUETES",
                columns: table => new
                {
                    Id = table.Column<long>(type: "NUMBER(19)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    EmpresaId = table.Column<long>(type: "NUMBER(19)", nullable: false),
                    Pergunta = table.Column<string>(type: "NVARCHAR2(300)", maxLength: 300, nullable: false),
                    DataCriacao = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: false),
                    Ativa = table.Column<int>(type: "NUMBER(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ENQUETES", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ENQUETES_EMPRESAS_EmpresaId",
                        column: x => x.EmpresaId,
                        principalTable: "EMPRESAS",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "INDICADORES_EMPRESA",
                columns: table => new
                {
                    Id = table.Column<long>(type: "NUMBER(19)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    EmpresaId = table.Column<long>(type: "NUMBER(19)", nullable: false),
                    HumorMedio = table.Column<double>(type: "BINARY_DOUBLE", nullable: false),
                    AdesaoAtividadesGeral = table.Column<double>(type: "BINARY_DOUBLE", nullable: false),
                    FrequenciaConsultas = table.Column<double>(type: "BINARY_DOUBLE", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_INDICADORES_EMPRESA", x => x.Id);
                    table.ForeignKey(
                        name: "FK_INDICADORES_EMPRESA_EMPRESAS_EmpresaId",
                        column: x => x.EmpresaId,
                        principalTable: "EMPRESAS",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SETORES",
                columns: table => new
                {
                    Id = table.Column<long>(type: "NUMBER(19)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    Nome = table.Column<string>(type: "NVARCHAR2(100)", maxLength: 100, nullable: false),
                    EmpresaId = table.Column<long>(type: "NUMBER(19)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SETORES", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SETORES_EMPRESAS_EmpresaId",
                        column: x => x.EmpresaId,
                        principalTable: "EMPRESAS",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ADESAO_SETOR",
                columns: table => new
                {
                    Id = table.Column<long>(type: "NUMBER(19)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    SetorId = table.Column<long>(type: "NUMBER(19)", nullable: false),
                    Adesao = table.Column<double>(type: "BINARY_DOUBLE", nullable: false),
                    IndicadoresEmpresaId = table.Column<long>(type: "NUMBER(19)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ADESAO_SETOR", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ADESAO_SETOR_INDICADORES_EMPRESA_IndicadoresEmpresaId",
                        column: x => x.IndicadoresEmpresaId,
                        principalTable: "INDICADORES_EMPRESA",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ADESAO_SETOR_SETORES_SetorId",
                        column: x => x.SetorId,
                        principalTable: "SETORES",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ATIVIDADES_BEM_ESTAR",
                columns: table => new
                {
                    Id = table.Column<long>(type: "NUMBER(19)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    EmpresaId = table.Column<long>(type: "NUMBER(19)", nullable: false),
                    Tipo = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    Titulo = table.Column<string>(type: "NVARCHAR2(100)", maxLength: 100, nullable: false),
                    Descricao = table.Column<string>(type: "NVARCHAR2(1000)", maxLength: 1000, nullable: false),
                    DataInicio = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: false),
                    DataFim = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: false),
                    SetorAlvoId = table.Column<long>(type: "NUMBER(19)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ATIVIDADES_BEM_ESTAR", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ATIVIDADES_BEM_ESTAR_EMPRESAS_EmpresaId",
                        column: x => x.EmpresaId,
                        principalTable: "EMPRESAS",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ATIVIDADES_BEM_ESTAR_SETORES_SetorAlvoId",
                        column: x => x.SetorAlvoId,
                        principalTable: "SETORES",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "FUNCIONARIOS",
                columns: table => new
                {
                    Id = table.Column<long>(type: "NUMBER(19)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    Nome = table.Column<string>(type: "NVARCHAR2(100)", maxLength: 100, nullable: false),
                    Email = table.Column<string>(type: "NVARCHAR2(200)", maxLength: 200, nullable: false),
                    Senha = table.Column<string>(type: "NVARCHAR2(256)", maxLength: 256, nullable: false),
                    TokenEmpresa = table.Column<string>(type: "NVARCHAR2(100)", maxLength: 100, nullable: false),
                    Cargo = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    Ativo = table.Column<int>(type: "NUMBER(1)", nullable: false),
                    SetorId = table.Column<long>(type: "NUMBER(19)", nullable: false),
                    Discriminator = table.Column<string>(type: "NVARCHAR2(13)", maxLength: 13, nullable: false),
                    EmpresaId = table.Column<long>(type: "NUMBER(19)", nullable: true),
                    Crp = table.Column<string>(type: "NVARCHAR2(30)", maxLength: 30, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FUNCIONARIOS", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FUNCIONARIOS_EMPRESAS_EmpresaId",
                        column: x => x.EmpresaId,
                        principalTable: "EMPRESAS",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_FUNCIONARIOS_SETORES_SetorId",
                        column: x => x.SetorId,
                        principalTable: "SETORES",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AGENDAS_FUNCIONARIOS",
                columns: table => new
                {
                    Id = table.Column<long>(type: "NUMBER(19)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    FuncionarioId = table.Column<long>(type: "NUMBER(19)", nullable: false),
                    Data = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AGENDAS_FUNCIONARIOS", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AGENDAS_FUNCIONARIOS_FUNCIONARIOS_FuncionarioId",
                        column: x => x.FuncionarioId,
                        principalTable: "FUNCIONARIOS",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AVALIACOES_PROFUNDAS",
                columns: table => new
                {
                    Id = table.Column<long>(type: "NUMBER(19)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    FuncionarioId = table.Column<long>(type: "NUMBER(19)", nullable: false),
                    Gad7Score = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    Phq9Score = table.Column<int>(type: "NUMBER(10)", nullable: true),
                    Interpretacao = table.Column<string>(type: "NVARCHAR2(1000)", maxLength: 1000, nullable: false),
                    DataRegistro = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AVALIACOES_PROFUNDAS", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AVALIACOES_PROFUNDAS_FUNCIONARIOS_FuncionarioId",
                        column: x => x.FuncionarioId,
                        principalTable: "FUNCIONARIOS",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CHATS_ANONIMOS",
                columns: table => new
                {
                    Id = table.Column<long>(type: "NUMBER(19)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    RemetenteId = table.Column<long>(type: "NUMBER(19)", nullable: true),
                    PsicologoId = table.Column<long>(type: "NUMBER(19)", nullable: false),
                    Mensagem = table.Column<string>(type: "NVARCHAR2(2000)", maxLength: 2000, nullable: false),
                    DataEnvio = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: false),
                    Anonimo = table.Column<int>(type: "NUMBER(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CHATS_ANONIMOS", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CHATS_ANONIMOS_FUNCIONARIOS_PsicologoId",
                        column: x => x.PsicologoId,
                        principalTable: "FUNCIONARIOS",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CHATS_ANONIMOS_FUNCIONARIOS_RemetenteId",
                        column: x => x.RemetenteId,
                        principalTable: "FUNCIONARIOS",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "CONSULTAS_PSICOLOGICAS",
                columns: table => new
                {
                    Id = table.Column<long>(type: "NUMBER(19)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    FuncionarioId = table.Column<long>(type: "NUMBER(19)", nullable: false),
                    PsicologoId = table.Column<long>(type: "NUMBER(19)", nullable: false),
                    DataConsulta = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: false),
                    Tipo = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    Status = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    AnotacoesSigilosas = table.Column<string>(type: "NVARCHAR2(2000)", maxLength: 2000, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CONSULTAS_PSICOLOGICAS", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CONSULTAS_PSICOLOGICAS_FUNCIONARIOS_FuncionarioId",
                        column: x => x.FuncionarioId,
                        principalTable: "FUNCIONARIOS",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CONSULTAS_PSICOLOGICAS_FUNCIONARIOS_PsicologoId",
                        column: x => x.PsicologoId,
                        principalTable: "FUNCIONARIOS",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DENUNCIAS",
                columns: table => new
                {
                    Id = table.Column<long>(type: "NUMBER(19)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    FuncionarioDenuncianteId = table.Column<long>(type: "NUMBER(19)", nullable: true),
                    DenuncianteId = table.Column<long>(type: "NUMBER(19)", nullable: true),
                    EmpresaId = table.Column<long>(type: "NUMBER(19)", nullable: false),
                    Tipo = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    Descricao = table.Column<string>(type: "NVARCHAR2(2000)", maxLength: 2000, nullable: false),
                    DataCriacao = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: false),
                    Status = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    CodigoRastreamento = table.Column<string>(type: "NVARCHAR2(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DENUNCIAS", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DENUNCIAS_EMPRESAS_EmpresaId",
                        column: x => x.EmpresaId,
                        principalTable: "EMPRESAS",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DENUNCIAS_FUNCIONARIOS_DenuncianteId",
                        column: x => x.DenuncianteId,
                        principalTable: "FUNCIONARIOS",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "MOOD_CHECKS",
                columns: table => new
                {
                    Id = table.Column<long>(type: "NUMBER(19)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    FuncionarioId = table.Column<long>(type: "NUMBER(19)", nullable: false),
                    Humor = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    Produtivo = table.Column<int>(type: "NUMBER(1)", nullable: false),
                    Estressado = table.Column<int>(type: "NUMBER(1)", nullable: false),
                    DormiuBem = table.Column<int>(type: "NUMBER(1)", nullable: false),
                    DataRegistro = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MOOD_CHECKS", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MOOD_CHECKS_FUNCIONARIOS_FuncionarioId",
                        column: x => x.FuncionarioId,
                        principalTable: "FUNCIONARIOS",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "NOTIFICACOES",
                columns: table => new
                {
                    Id = table.Column<long>(type: "NUMBER(19)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    FuncionarioId = table.Column<long>(type: "NUMBER(19)", nullable: false),
                    Mensagem = table.Column<string>(type: "NVARCHAR2(1000)", maxLength: 1000, nullable: false),
                    Tipo = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    Lida = table.Column<int>(type: "NUMBER(1)", nullable: false),
                    DataEnvio = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NOTIFICACOES", x => x.Id);
                    table.ForeignKey(
                        name: "FK_NOTIFICACOES_FUNCIONARIOS_FuncionarioId",
                        column: x => x.FuncionarioId,
                        principalTable: "FUNCIONARIOS",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PARTICIPACOES_ATIVIDADE",
                columns: table => new
                {
                    Id = table.Column<long>(type: "NUMBER(19)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    FuncionarioId = table.Column<long>(type: "NUMBER(19)", nullable: false),
                    AtividadeId = table.Column<long>(type: "NUMBER(19)", nullable: false),
                    Participou = table.Column<int>(type: "NUMBER(1)", nullable: false),
                    DataParticipacao = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PARTICIPACOES_ATIVIDADE", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PARTICIPACOES_ATIVIDADE_ATIVIDADES_BEM_ESTAR_AtividadeId",
                        column: x => x.AtividadeId,
                        principalTable: "ATIVIDADES_BEM_ESTAR",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PARTICIPACOES_ATIVIDADE_FUNCIONARIOS_FuncionarioId",
                        column: x => x.FuncionarioId,
                        principalTable: "FUNCIONARIOS",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PERFIS_EMOCIONAIS",
                columns: table => new
                {
                    Id = table.Column<long>(type: "NUMBER(19)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    FuncionarioId = table.Column<long>(type: "NUMBER(19)", nullable: false),
                    HumorInicial = table.Column<string>(type: "NVARCHAR2(50)", maxLength: 50, nullable: false),
                    Rotina = table.Column<string>(type: "NVARCHAR2(500)", maxLength: 500, nullable: false),
                    PrincipaisEstressores = table.Column<string>(type: "NVARCHAR2(500)", maxLength: 500, nullable: false),
                    DataCriacao = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PERFIS_EMOCIONAIS", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PERFIS_EMOCIONAIS_FUNCIONARIOS_FuncionarioId",
                        column: x => x.FuncionarioId,
                        principalTable: "FUNCIONARIOS",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RESPOSTAS_ENQUETE",
                columns: table => new
                {
                    Id = table.Column<long>(type: "NUMBER(19)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    EnqueteId = table.Column<long>(type: "NUMBER(19)", nullable: false),
                    FuncionarioId = table.Column<long>(type: "NUMBER(19)", nullable: false),
                    Resposta = table.Column<string>(type: "NVARCHAR2(500)", maxLength: 500, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RESPOSTAS_ENQUETE", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RESPOSTAS_ENQUETE_ENQUETES_EnqueteId",
                        column: x => x.EnqueteId,
                        principalTable: "ENQUETES",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RESPOSTAS_ENQUETE_FUNCIONARIOS_FuncionarioId",
                        column: x => x.FuncionarioId,
                        principalTable: "FUNCIONARIOS",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RISCOS_PSICOSSOCIAIS",
                columns: table => new
                {
                    Id = table.Column<long>(type: "NUMBER(19)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    FuncionarioId = table.Column<long>(type: "NUMBER(19)", nullable: false),
                    Categoria = table.Column<string>(type: "NVARCHAR2(100)", maxLength: 100, nullable: false),
                    NivelRisco = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    DataRegistro = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RISCOS_PSICOSSOCIAIS", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RISCOS_PSICOSSOCIAIS_FUNCIONARIOS_FuncionarioId",
                        column: x => x.FuncionarioId,
                        principalTable: "FUNCIONARIOS",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SOS_EMERGENCIAS",
                columns: table => new
                {
                    Id = table.Column<long>(type: "NUMBER(19)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    FuncionarioId = table.Column<long>(type: "NUMBER(19)", nullable: false),
                    DataAcionamento = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: false),
                    Tipo = table.Column<string>(type: "NVARCHAR2(100)", maxLength: 100, nullable: false),
                    PsicologoNotificado = table.Column<int>(type: "NUMBER(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SOS_EMERGENCIAS", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SOS_EMERGENCIAS_FUNCIONARIOS_FuncionarioId",
                        column: x => x.FuncionarioId,
                        principalTable: "FUNCIONARIOS",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ITENS_AGENDA",
                columns: table => new
                {
                    Id = table.Column<long>(type: "NUMBER(19)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    AgendaFuncionarioId = table.Column<long>(type: "NUMBER(19)", nullable: false),
                    Tipo = table.Column<string>(type: "NVARCHAR2(30)", maxLength: 30, nullable: false),
                    Titulo = table.Column<string>(type: "NVARCHAR2(100)", maxLength: 100, nullable: false),
                    Horario = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ITENS_AGENDA", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ITENS_AGENDA_AGENDAS_FUNCIONARIOS_AgendaFuncionarioId",
                        column: x => x.AgendaFuncionarioId,
                        principalTable: "AGENDAS_FUNCIONARIOS",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "INVESTIGACOES_DENUNCIA",
                columns: table => new
                {
                    Id = table.Column<long>(type: "NUMBER(19)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    DenunciaId = table.Column<long>(type: "NUMBER(19)", nullable: false),
                    EquipeResponsavel = table.Column<string>(type: "NVARCHAR2(100)", maxLength: 100, nullable: false),
                    DataInicio = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: false),
                    DataFim = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: true),
                    MedidasAdotadas = table.Column<string>(type: "NVARCHAR2(1000)", maxLength: 1000, nullable: false),
                    Concluida = table.Column<int>(type: "NUMBER(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_INVESTIGACOES_DENUNCIA", x => x.Id);
                    table.ForeignKey(
                        name: "FK_INVESTIGACOES_DENUNCIA_DENUNCIAS_DenunciaId",
                        column: x => x.DenunciaId,
                        principalTable: "DENUNCIAS",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ADESAO_SETOR_IndicadoresEmpresaId",
                table: "ADESAO_SETOR",
                column: "IndicadoresEmpresaId");

            migrationBuilder.CreateIndex(
                name: "IX_ADESAO_SETOR_SetorId",
                table: "ADESAO_SETOR",
                column: "SetorId");

            migrationBuilder.CreateIndex(
                name: "IX_AGENDAS_FUNCIONARIOS_FuncionarioId",
                table: "AGENDAS_FUNCIONARIOS",
                column: "FuncionarioId");

            migrationBuilder.CreateIndex(
                name: "IX_ATIVIDADES_BEM_ESTAR_EmpresaId",
                table: "ATIVIDADES_BEM_ESTAR",
                column: "EmpresaId");

            migrationBuilder.CreateIndex(
                name: "IX_ATIVIDADES_BEM_ESTAR_SetorAlvoId",
                table: "ATIVIDADES_BEM_ESTAR",
                column: "SetorAlvoId");

            migrationBuilder.CreateIndex(
                name: "IX_AVALIACOES_PROFUNDAS_FuncionarioId",
                table: "AVALIACOES_PROFUNDAS",
                column: "FuncionarioId");

            migrationBuilder.CreateIndex(
                name: "IX_CHATS_ANONIMOS_PsicologoId",
                table: "CHATS_ANONIMOS",
                column: "PsicologoId");

            migrationBuilder.CreateIndex(
                name: "IX_CHATS_ANONIMOS_RemetenteId",
                table: "CHATS_ANONIMOS",
                column: "RemetenteId");

            migrationBuilder.CreateIndex(
                name: "IX_CONSULTAS_PSICOLOGICAS_FuncionarioId",
                table: "CONSULTAS_PSICOLOGICAS",
                column: "FuncionarioId");

            migrationBuilder.CreateIndex(
                name: "IX_CONSULTAS_PSICOLOGICAS_PsicologoId",
                table: "CONSULTAS_PSICOLOGICAS",
                column: "PsicologoId");

            migrationBuilder.CreateIndex(
                name: "IX_DENUNCIAS_DenuncianteId",
                table: "DENUNCIAS",
                column: "DenuncianteId");

            migrationBuilder.CreateIndex(
                name: "IX_DENUNCIAS_EmpresaId",
                table: "DENUNCIAS",
                column: "EmpresaId");

            migrationBuilder.CreateIndex(
                name: "IX_ENQUETES_EmpresaId",
                table: "ENQUETES",
                column: "EmpresaId");

            migrationBuilder.CreateIndex(
                name: "IX_FUNCIONARIOS_EmpresaId",
                table: "FUNCIONARIOS",
                column: "EmpresaId");

            migrationBuilder.CreateIndex(
                name: "IX_FUNCIONARIOS_SetorId",
                table: "FUNCIONARIOS",
                column: "SetorId");

            migrationBuilder.CreateIndex(
                name: "IX_INDICADORES_EMPRESA_EmpresaId",
                table: "INDICADORES_EMPRESA",
                column: "EmpresaId");

            migrationBuilder.CreateIndex(
                name: "IX_INVESTIGACOES_DENUNCIA_DenunciaId",
                table: "INVESTIGACOES_DENUNCIA",
                column: "DenunciaId");

            migrationBuilder.CreateIndex(
                name: "IX_ITENS_AGENDA_AgendaFuncionarioId",
                table: "ITENS_AGENDA",
                column: "AgendaFuncionarioId");

            migrationBuilder.CreateIndex(
                name: "IX_MOOD_CHECKS_FuncionarioId",
                table: "MOOD_CHECKS",
                column: "FuncionarioId");

            migrationBuilder.CreateIndex(
                name: "IX_NOTIFICACOES_FuncionarioId",
                table: "NOTIFICACOES",
                column: "FuncionarioId");

            migrationBuilder.CreateIndex(
                name: "IX_PARTICIPACOES_ATIVIDADE_AtividadeId",
                table: "PARTICIPACOES_ATIVIDADE",
                column: "AtividadeId");

            migrationBuilder.CreateIndex(
                name: "IX_PARTICIPACOES_ATIVIDADE_FuncionarioId",
                table: "PARTICIPACOES_ATIVIDADE",
                column: "FuncionarioId");

            migrationBuilder.CreateIndex(
                name: "IX_PERFIS_EMOCIONAIS_FuncionarioId",
                table: "PERFIS_EMOCIONAIS",
                column: "FuncionarioId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_RESPOSTAS_ENQUETE_EnqueteId",
                table: "RESPOSTAS_ENQUETE",
                column: "EnqueteId");

            migrationBuilder.CreateIndex(
                name: "IX_RESPOSTAS_ENQUETE_FuncionarioId",
                table: "RESPOSTAS_ENQUETE",
                column: "FuncionarioId");

            migrationBuilder.CreateIndex(
                name: "IX_RISCOS_PSICOSSOCIAIS_FuncionarioId",
                table: "RISCOS_PSICOSSOCIAIS",
                column: "FuncionarioId");

            migrationBuilder.CreateIndex(
                name: "IX_SETORES_EmpresaId",
                table: "SETORES",
                column: "EmpresaId");

            migrationBuilder.CreateIndex(
                name: "IX_SOS_EMERGENCIAS_FuncionarioId",
                table: "SOS_EMERGENCIAS",
                column: "FuncionarioId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ADESAO_SETOR");

            migrationBuilder.DropTable(
                name: "AVALIACOES_PROFUNDAS");

            migrationBuilder.DropTable(
                name: "CHATS_ANONIMOS");

            migrationBuilder.DropTable(
                name: "CONSULTAS_PSICOLOGICAS");

            migrationBuilder.DropTable(
                name: "INVESTIGACOES_DENUNCIA");

            migrationBuilder.DropTable(
                name: "ITENS_AGENDA");

            migrationBuilder.DropTable(
                name: "MOOD_CHECKS");

            migrationBuilder.DropTable(
                name: "NOTIFICACOES");

            migrationBuilder.DropTable(
                name: "PARTICIPACOES_ATIVIDADE");

            migrationBuilder.DropTable(
                name: "PERFIS_EMOCIONAIS");

            migrationBuilder.DropTable(
                name: "RESPOSTAS_ENQUETE");

            migrationBuilder.DropTable(
                name: "RISCOS_PSICOSSOCIAIS");

            migrationBuilder.DropTable(
                name: "SOS_EMERGENCIAS");

            migrationBuilder.DropTable(
                name: "INDICADORES_EMPRESA");

            migrationBuilder.DropTable(
                name: "DENUNCIAS");

            migrationBuilder.DropTable(
                name: "AGENDAS_FUNCIONARIOS");

            migrationBuilder.DropTable(
                name: "ATIVIDADES_BEM_ESTAR");

            migrationBuilder.DropTable(
                name: "ENQUETES");

            migrationBuilder.DropTable(
                name: "FUNCIONARIOS");

            migrationBuilder.DropTable(
                name: "SETORES");

            migrationBuilder.DropTable(
                name: "EMPRESAS");
        }
    }
}
