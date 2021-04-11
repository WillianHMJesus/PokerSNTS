BEGIN TRANSACTION;
GO

CREATE TABLE [Players] (
    [Id] uniqueidentifier NOT NULL,
    [Name] varchar(50) NOT NULL,
    [Created] datetime2 NOT NULL,
    [Actived] bit NOT NULL,
    CONSTRAINT [PK_Players] PRIMARY KEY ([Id])
);
GO


CREATE TABLE [Ranking] (
    [Id] uniqueidentifier NOT NULL,
    [Description] varchar(100) NOT NULL,
    [AwardValue] decimal(18,2) NULL,
    [Created] datetime2 NOT NULL,
    [Actived] bit NOT NULL,
    CONSTRAINT [PK_Ranking] PRIMARY KEY ([Id])
);
GO


CREATE TABLE [RankingPunctuations] (
    [Id] uniqueidentifier NOT NULL,
    [Position] smallint NOT NULL,
    [Punctuation] smallint NOT NULL,
    [Created] datetime2 NOT NULL,
    [Actived] bit NOT NULL,
    CONSTRAINT [PK_RankingPunctuations] PRIMARY KEY ([Id])
);
GO


CREATE TABLE [Regulations] (
    [Id] uniqueidentifier NOT NULL,
    [Description] varchar(8000) NOT NULL,
    [Created] datetime2 NOT NULL,
    [Actived] bit NOT NULL,
    CONSTRAINT [PK_Regulations] PRIMARY KEY ([Id])
);
GO


CREATE TABLE [Users] (
    [Id] uniqueidentifier NOT NULL,
    [UserName] varchar(50) NOT NULL,
    [Password] varchar(50) NOT NULL,
    [Created] datetime2 NOT NULL,
    [Actived] bit NOT NULL,
    CONSTRAINT [PK_Users] PRIMARY KEY ([Id])
);
GO


CREATE TABLE [Rounds] (
    [Id] uniqueidentifier NOT NULL,
    [Description] varchar(100) NOT NULL,
    [Date] date NOT NULL,
    [RankingId] uniqueidentifier NOT NULL,
    [Created] datetime2 NOT NULL,
    [Actived] bit NOT NULL,
    CONSTRAINT [PK_Rounds] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Rounds_Ranking_RankingId] FOREIGN KEY ([RankingId]) REFERENCES [Ranking] ([Id]) ON DELETE CASCADE
);
GO


CREATE TABLE [RoundsPunctuations] (
    [Id] uniqueidentifier NOT NULL,
    [Position] smallint NOT NULL,
    [Punctuation] smallint NOT NULL,
    [PlayerId] uniqueidentifier NOT NULL,
    [RoundId] uniqueidentifier NOT NULL,
    [Created] datetime2 NOT NULL,
    [Actived] bit NOT NULL,
    CONSTRAINT [PK_RoundsPunctuations] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_RoundsPunctuations_Players_PlayerId] FOREIGN KEY ([PlayerId]) REFERENCES [Players] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_RoundsPunctuations_Rounds_RoundId] FOREIGN KEY ([RoundId]) REFERENCES [Rounds] ([Id]) ON DELETE CASCADE
);
GO


CREATE INDEX [IX_Rounds_RankingId] ON [Rounds] ([RankingId]);
GO


CREATE INDEX [IX_RoundsPunctuations_PlayerId] ON [RoundsPunctuations] ([PlayerId]);
GO


CREATE INDEX [IX_RoundsPunctuations_RoundId] ON [RoundsPunctuations] ([RoundId]);
GO


COMMIT;
GO