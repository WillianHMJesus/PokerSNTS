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


CREATE TABLE [RankingPoints] (
    [Id] uniqueidentifier NOT NULL,
    [Position] smallint NOT NULL,
    [Point] smallint NOT NULL,
    [Created] datetime2 NOT NULL,
    [Actived] bit NOT NULL,
    CONSTRAINT [PK_RankingPoints] PRIMARY KEY ([Id])
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
    [Password] varchar(200) NOT NULL,
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


CREATE TABLE [RoundsPoints] (
    [Id] uniqueidentifier NOT NULL,
    [Position] smallint NOT NULL,
    [Point] smallint NOT NULL,
    [PlayerId] uniqueidentifier NOT NULL,
    [RoundId] uniqueidentifier NOT NULL,
    [Created] datetime2 NOT NULL,
    [Actived] bit NOT NULL,
    CONSTRAINT [PK_RoundsPoints] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_RoundsPoints_Players_PlayerId] FOREIGN KEY ([PlayerId]) REFERENCES [Players] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_RoundsPoints_Rounds_RoundId] FOREIGN KEY ([RoundId]) REFERENCES [Rounds] ([Id]) ON DELETE CASCADE
);
GO


CREATE INDEX [IX_Rounds_RankingId] ON [Rounds] ([RankingId]);
GO


CREATE INDEX [IX_RoundsPoints_PlayerId] ON [RoundsPoints] ([PlayerId]);
GO


CREATE INDEX [IX_RoundsPoints_RoundId] ON [RoundsPoints] ([RoundId]);
GO


COMMIT;
GO