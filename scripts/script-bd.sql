-- ======================================================================
-- WorkWell - Script Completo para Azure SQL
-- Baseado nas entidades (DOMAIN e DTOs) do projeto
-- ======================================================================

-- EMPRESA
CREATE TABLE Empresa (
    Id BIGINT IDENTITY(1,1) PRIMARY KEY,
    Nome NVARCHAR(100) NOT NULL,
    EmailAdmin NVARCHAR(200) NOT NULL,
    SenhaAdmin NVARCHAR(256) NOT NULL,
    TokenAcesso NVARCHAR(100) NOT NULL,
    LogoUrl NVARCHAR(300) NOT NULL,
    CorPrimaria NVARCHAR(10) NOT NULL,
    CorSecundaria NVARCHAR(10) NOT NULL,
    Missao NVARCHAR(1000) NOT NULL,
    PoliticaBemEstar NVARCHAR(1000) NOT NULL
);

-- SETOR
CREATE TABLE Setor (
    Id BIGINT IDENTITY(1,1) PRIMARY KEY,
    Nome NVARCHAR(100) NOT NULL,
    EmpresaId BIGINT NOT NULL,
    FOREIGN KEY (EmpresaId) REFERENCES Empresa(Id)
);

-- FUNCIONARIO
CREATE TABLE Funcionario (
    Id BIGINT IDENTITY(1,1) PRIMARY KEY,
    Nome NVARCHAR(100) NOT NULL,
    Email NVARCHAR(200) NOT NULL,
    Senha NVARCHAR(256) NOT NULL,
    TokenEmpresa NVARCHAR(100) NOT NULL,
    Cargo INT NOT NULL, -- ENUM: Admin=0, RH=1, Funcionario=2, Psicologo=3
    Ativo BIT NOT NULL DEFAULT 1,
    SetorId BIGINT NOT NULL,
    PerfilEmocionalId BIGINT NULL,
    FOREIGN KEY (SetorId) REFERENCES Setor(Id)
    -- PerfilEmocional FK criada abaixo
);

-- PERFIL EMOCIONAL
CREATE TABLE PerfilEmocional (
    Id BIGINT IDENTITY(1,1) PRIMARY KEY,
    FuncionarioId BIGINT NOT NULL,
    HumorInicial NVARCHAR(50) NOT NULL,
    Rotina NVARCHAR(500) NOT NULL,
    PrincipaisEstressores NVARCHAR(500) NOT NULL,
    DataCriacao DATETIME2 NOT NULL,
    FOREIGN KEY (FuncionarioId) REFERENCES Funcionario(Id)
);

-- Atualiza FK do Funcionario para perfil emocional (um-para-um)
ALTER TABLE Funcionario
ADD CONSTRAINT FK_Funcionario_PerfilEmocional
FOREIGN KEY (PerfilEmocionalId) REFERENCES PerfilEmocional(Id);

-- ATIVIDADE BEM ESTAR
CREATE TABLE AtividadeBemEstar (
    Id BIGINT IDENTITY(1,1) PRIMARY KEY,
    EmpresaId BIGINT NOT NULL,
    Tipo INT NOT NULL, -- ENUM
    Titulo NVARCHAR(100) NOT NULL,
    Descricao NVARCHAR(1000) NOT NULL,
    DataInicio DATETIME2 NOT NULL,
    DataFim DATETIME2 NOT NULL,
    SetorAlvoId BIGINT NULL,
    FOREIGN KEY (EmpresaId) REFERENCES Empresa(Id),
    FOREIGN KEY (SetorAlvoId) REFERENCES Setor(Id)
);

-- PARTICIPACAO ATIVIDADE
CREATE TABLE ParticipacaoAtividade (
    Id BIGINT IDENTITY(1,1) PRIMARY KEY,
    FuncionarioId BIGINT NOT NULL,
    AtividadeId BIGINT NOT NULL,
    Participou BIT NOT NULL,
    DataParticipacao DATETIME2 NOT NULL,
    FOREIGN KEY (FuncionarioId) REFERENCES Funcionario(Id),
    FOREIGN KEY (AtividadeId) REFERENCES AtividadeBemEstar(Id)
);

-- AGENDA FUNCIONARIO
CREATE TABLE AgendaFuncionario (
    Id BIGINT IDENTITY(1,1) PRIMARY KEY,
    FuncionarioId BIGINT NOT NULL,
    Data DATETIME2 NOT NULL,
    FOREIGN KEY (FuncionarioId) REFERENCES Funcionario(Id)
);

-- ITENS AGENDA
CREATE TABLE ItemAgenda (
    Id BIGINT IDENTITY(1,1) PRIMARY KEY,
    AgendaFuncionarioId BIGINT NOT NULL,
    Tipo NVARCHAR(30) NOT NULL,
    Titulo NVARCHAR(100) NOT NULL,
    Horario DATETIME2 NOT NULL,
    FOREIGN KEY (AgendaFuncionarioId) REFERENCES AgendaFuncionario(Id)
);

-- ENQUETE
CREATE TABLE Enquete (
    Id BIGINT IDENTITY(1,1) PRIMARY KEY,
    EmpresaId BIGINT NOT NULL,
    Pergunta NVARCHAR(300) NOT NULL,
    DataCriacao DATETIME2 NOT NULL,
    Ativa BIT NOT NULL,
    FOREIGN KEY (EmpresaId) REFERENCES Empresa(Id)
);

-- RESPOSTA ENQUETE
CREATE TABLE RespostaEnquete (
    Id BIGINT IDENTITY(1,1) PRIMARY KEY,
    EnqueteId BIGINT NOT NULL,
    FuncionarioId BIGINT NOT NULL,
    Resposta NVARCHAR(500) NOT NULL,
    FOREIGN KEY (EnqueteId) REFERENCES Enquete(Id),
    FOREIGN KEY (FuncionarioId) REFERENCES Funcionario(Id)
);

-- INDICADORES EMPRESA
CREATE TABLE IndicadoresEmpresa (
    Id BIGINT IDENTITY(1,1) PRIMARY KEY,
    EmpresaId BIGINT NOT NULL,
    HumorMedio FLOAT NOT NULL,
    AdesaoAtividadesGeral FLOAT NOT NULL,
    FrequenciaConsultas FLOAT NOT NULL,
    FOREIGN KEY (EmpresaId) REFERENCES Empresa(Id)
);

-- ADESAO POR SETOR
CREATE TABLE AdesaoSetor (
    Id BIGINT IDENTITY(1,1) PRIMARY KEY,
    IndicadoresEmpresaId BIGINT NOT NULL,
    SetorId BIGINT NOT NULL,
    Adesao FLOAT NOT NULL,
    FOREIGN KEY (IndicadoresEmpresaId) REFERENCES IndicadoresEmpresa(Id),
    FOREIGN KEY (SetorId) REFERENCES Setor(Id)
);

-- NOTIFICACAO
CREATE TABLE Notificacao (
    Id BIGINT IDENTITY(1,1) PRIMARY KEY,
    FuncionarioId BIGINT NOT NULL,
    Mensagem NVARCHAR(1000) NOT NULL,
    Tipo INT NOT NULL, -- ENUM
    Lida BIT NOT NULL,
    DataEnvio DATETIME2 NOT NULL,
    FOREIGN KEY (FuncionarioId) REFERENCES Funcionario(Id)
);

-- CHAT ANONIMO
CREATE TABLE ChatAnonimo (
    Id BIGINT IDENTITY(1,1) PRIMARY KEY,
    RemetenteId BIGINT NULL,
    PsicologoId BIGINT NOT NULL,
    Mensagem NVARCHAR(2000) NOT NULL,
    DataEnvio DATETIME2 NOT NULL,
    Anonimo BIT NOT NULL,
    FOREIGN KEY (RemetenteId) REFERENCES Funcionario(Id),
    FOREIGN KEY (PsicologoId) REFERENCES Funcionario(Id)
);

-- PSICOLOGO (herda Funcionario)
-- Os campos adicionais do psicólogo:
CREATE TABLE Psicologo (
    Id BIGINT PRIMARY KEY, -- ID igual ao Funcionario
    Crp NVARCHAR(30) NOT NULL,
    FOREIGN KEY (Id) REFERENCES Funcionario(Id)
);

-- CONSULTA PSICOLOGICA
CREATE TABLE ConsultaPsicologica (
    Id BIGINT IDENTITY(1,1) PRIMARY KEY,
    FuncionarioId BIGINT NOT NULL,
    PsicologoId BIGINT NOT NULL,
    DataConsulta DATETIME2 NOT NULL,
    Tipo INT NOT NULL,
    Status INT NOT NULL,
    AnotacoesSigilosas NVARCHAR(2000) NOT NULL,
    FOREIGN KEY (FuncionarioId) REFERENCES Funcionario(Id),
    FOREIGN KEY (PsicologoId) REFERENCES Psicologo(Id)
);

-- SOS EMERGENCIA
CREATE TABLE SOSemergencia (
    Id BIGINT IDENTITY(1,1) PRIMARY KEY,
    FuncionarioId BIGINT NOT NULL,
    DataAcionamento DATETIME2 NOT NULL,
    Tipo NVARCHAR(100) NOT NULL,
    PsicologoNotificado BIT NOT NULL,
    FOREIGN KEY (FuncionarioId) REFERENCES Funcionario(Id)
);

-- DENUNCIA
CREATE TABLE Denuncia (
    Id BIGINT IDENTITY(1,1) PRIMARY KEY,
    FuncionarioDenuncianteId BIGINT NULL,
    EmpresaId BIGINT NOT NULL,
    Tipo INT NOT NULL,
    Descricao NVARCHAR(2000) NOT NULL,
    DataCriacao DATETIME2 NOT NULL,
    Status INT NOT NULL,
    CodigoRastreamento NVARCHAR(100) NOT NULL,
    FOREIGN KEY (FuncionarioDenuncianteId) REFERENCES Funcionario(Id),
    FOREIGN KEY (EmpresaId) REFERENCES Empresa(Id)
);

-- INVESTIGACAO DENUNCIA
CREATE TABLE InvestigacaoDenuncia (
    Id BIGINT IDENTITY(1,1) PRIMARY KEY,
    DenunciaId BIGINT NOT NULL,
    EquipeResponsavel NVARCHAR(100) NOT NULL,
    DataInicio DATETIME2 NOT NULL,
    DataFim DATETIME2 NULL,
    MedidasAdotadas NVARCHAR(1000) NOT NULL,
    Concluida BIT NOT NULL,
    FOREIGN KEY (DenunciaId) REFERENCES Denuncia(Id)
);

-- AVALIACAO PROFUNDA
CREATE TABLE AvaliacaoProfunda (
    Id BIGINT IDENTITY(1,1) PRIMARY KEY,
    FuncionarioId BIGINT NOT NULL,
    Gad7Score INT NOT NULL,
    Phq9Score INT NULL,
    Interpretacao NVARCHAR(1000) NOT NULL,
    DataRegistro DATETIME2 NOT NULL,
    FOREIGN KEY (FuncionarioId) REFERENCES Funcionario(Id)
);

-- MOOD CHECK
CREATE TABLE MoodCheck (
    Id BIGINT IDENTITY(1,1) PRIMARY KEY,
    FuncionarioId BIGINT NOT NULL,
    Humor INT NOT NULL,
    Produtivo BIT NOT NULL,
    Estressado BIT NOT NULL,
    DormiuBem BIT NOT NULL,
    DataRegistro DATETIME2 NOT NULL,
    FOREIGN KEY (FuncionarioId) REFERENCES Funcionario(Id)
);

-- RISCO PSICOSSOCIAL
CREATE TABLE RiscoPsicossocial (
    Id BIGINT IDENTITY(1,1) PRIMARY KEY,
    FuncionarioId BIGINT NOT NULL,
    Categoria NVARCHAR(100) NOT NULL,
    NivelRisco INT NOT NULL,
    DataRegistro DATETIME2 NOT NULL,
    FOREIGN KEY (FuncionarioId) REFERENCES Funcionario(Id)
);