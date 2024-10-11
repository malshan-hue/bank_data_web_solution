CREATE TABLE [dbo].[User]
(
	[UserId] INT IDENTITY NOT NULL,
    [UserName] NVARCHAR(255) NOT NULL,
    [Password] NVARCHAR(255) NOT NULL,
    [PasswordSalt] NVARCHAR(255) NOT NULL,
    [ActivationCode] INT NOT NULL,
    [CreatedDate] DATETIME NOT NULL,
    [LastLogginDate] DATETIME,
    [IsDeleted] BIT NOT NULL DEFAULT 0,
    [UserGlobalIdentity] UNIQUEIDENTIFIER NULL,
    [IsAdmin] BIT DEFAULT 0

    CONSTRAINT [User_UserId_PK] PRIMARY KEY CLUSTERED ([UserId])
)
