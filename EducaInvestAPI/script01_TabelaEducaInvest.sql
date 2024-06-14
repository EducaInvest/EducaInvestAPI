IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;
GO

BEGIN TRANSACTION;
GO

CREATE TABLE [TB_USUARIOS] (
    [Id] int NOT NULL IDENTITY,
    [Perfil] int NOT NULL,
    [Email] nvarchar(max) NOT NULL,
    [Nome] nvarchar(30) NOT NULL,
    [Sobrenome] nvarchar(100) NOT NULL,
    [CPF] nvarchar(11) NOT NULL,
    [Telefone] nvarchar(11) NOT NULL,
    [LinkSocial] nvarchar(255) NULL,
    [Cidade] nvarchar(100) NULL,
    [UF] int NOT NULL,
    [DataAcesso] datetime2 NULL,
    [FileBytes] nvarchar(max) NULL,
    [PasswordHash] varbinary(max) NULL,
    [PasswordSalt] varbinary(max) NULL,
    CONSTRAINT [PK_TB_USUARIOS] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [TB_PROJETOS] (
    [Id] int NOT NULL IDENTITY,
    [StatusProjeto] int NOT NULL,
    [NomeProjeto] nvarchar(30) NOT NULL,
    [Subtitulo] nvarchar(65) NULL,
    [DescricaoProjeto] nvarchar(255) NULL,
    [CustoProjeto] decimal(18,2) NOT NULL,
    [Investido] bit NOT NULL,
    [DataPublicacao] datetime2 NOT NULL,
    [UsuarioId] int NOT NULL,
    [CronogramaId] int NOT NULL,
    [FileBytes] nvarchar(max) NULL,
    CONSTRAINT [PK_TB_PROJETOS] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_TB_PROJETOS_TB_USUARIOS_UsuarioId] FOREIGN KEY ([UsuarioId]) REFERENCES [TB_USUARIOS] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [TB_CRONOGRAMAS] (
    [Id] int NOT NULL IDENTITY,
    [ProjetoId] int NOT NULL,
    CONSTRAINT [PK_TB_CRONOGRAMAS] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_TB_CRONOGRAMAS_TB_PROJETOS_ProjetoId] FOREIGN KEY ([ProjetoId]) REFERENCES [TB_PROJETOS] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [TB_ATIVIDADES] (
    [Id] int NOT NULL IDENTITY,
    [DescricaoAtividade] nvarchar(200) NULL,
    [StatusAtividade] int NOT NULL,
    [DataInicioAtividade] datetime2 NOT NULL,
    [DataTerminoAtividade] datetime2 NOT NULL,
    [Percentual] decimal(18,2) NOT NULL,
    [CronogramaId] int NOT NULL,
    CONSTRAINT [PK_TB_ATIVIDADES] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_TB_ATIVIDADES_TB_CRONOGRAMAS_CronogramaId] FOREIGN KEY ([CronogramaId]) REFERENCES [TB_CRONOGRAMAS] ([Id]) ON DELETE CASCADE
);
GO

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'CPF', N'Cidade', N'DataAcesso', N'Email', N'FileBytes', N'LinkSocial', N'Nome', N'PasswordHash', N'PasswordSalt', N'Perfil', N'Sobrenome', N'Telefone', N'UF') AND [object_id] = OBJECT_ID(N'[TB_USUARIOS]'))
    SET IDENTITY_INSERT [TB_USUARIOS] ON;
INSERT INTO [TB_USUARIOS] ([Id], [CPF], [Cidade], [DataAcesso], [Email], [FileBytes], [LinkSocial], [Nome], [PasswordHash], [PasswordSalt], [Perfil], [Sobrenome], [Telefone], [UF])
VALUES (1, N'', N'', '2024-06-13T17:46:27.2605055-03:00', N'educainvest.co@gmail.com', NULL, N'', N'', 0x0F97B1A4C0E5FCE10E6D1CE8DEF2C13B2706260F8F394137BE8F8A8B958BD289AE1ED5E6EE2FF8B7FAA832F20781E533929045AE05EC8E113F21E972B3964C4E, 0x2A6F0578A504C2686D917BBA6CE3919967C950F1563391CD58C75CD1C6A8D10371772DEDA8BD8D0758763E75241805CD139847EDC2937D52BC32C63D8ADEE7BD76989BAFA5F37862203516F0AC48F63C308DA2CE9916D5594B671A05644094D38D643F25C8B125C4A7A977AABAD299A0DC610A6D2C7B0F7AB8BEA0FE959A8CE1, 1, N'', N'', 6);
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'CPF', N'Cidade', N'DataAcesso', N'Email', N'FileBytes', N'LinkSocial', N'Nome', N'PasswordHash', N'PasswordSalt', N'Perfil', N'Sobrenome', N'Telefone', N'UF') AND [object_id] = OBJECT_ID(N'[TB_USUARIOS]'))
    SET IDENTITY_INSERT [TB_USUARIOS] OFF;
GO

CREATE INDEX [IX_TB_ATIVIDADES_CronogramaId] ON [TB_ATIVIDADES] ([CronogramaId]);
GO

CREATE UNIQUE INDEX [IX_TB_CRONOGRAMAS_ProjetoId] ON [TB_CRONOGRAMAS] ([ProjetoId]);
GO

CREATE INDEX [IX_TB_PROJETOS_UsuarioId] ON [TB_PROJETOS] ([UsuarioId]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20240613204628_InitialCreate', N'8.0.6');
GO

COMMIT;
GO

