CREATE TABLE [dbo].[Transaction]
(
	[TransactionId] INT IDENTITY NOT NULL,
	[AccountId] INT NOT NULL,
	[Amount] DECIMAL(18,2) NOT NULL,
	[TransactionDate] DATETIME NOT NULL,
	[TransactionTypeEnum] INT NOT NULL,
	[Description] NVARCHAR(MAX) NULL,
	[InitiatedAccountId] INT NULL

	CONSTRAINT [Transaction_TransactionId_PK] PRIMARY KEY ([TransactionId]),
	CONSTRAINT [Transaction_AccountId_FK] FOREIGN KEY ([AccountId]) REFERENCES [Account]([AccountId]),
	CONSTRAINT [Transaction_TransactionTypeEnum_FK] FOREIGN KEY ([TransactionTypeEnum]) REFERENCES [TransactionTypeEnum]([TransactionTypeEnum]),
)
