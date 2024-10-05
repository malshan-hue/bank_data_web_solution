CREATE TABLE [dbo].[Account]
(
	[AccountId] INT IDENTITY NOT NULL,
	[UserId] INT NOT NULL,
	[AccountNumber] NVARCHAR(200) NOT NULL,
	[AccountHolderName] NVARCHAR(200) NOT NULL,
	[Balance] DECIMAL(18,2) NOT NULL,
	[CreatedDateTime] DATETIME NOT NULL,
	[IsDeleted] BIT NOT NULL

	CONSTRAINT [Account_AccountId_PK] PRIMARY KEY ([AccountId]),
	CONSTRAINT [Account_UserId_FK] FOREIGN KEY ([UserId]) REFERENCES [User]([UserId])

)
