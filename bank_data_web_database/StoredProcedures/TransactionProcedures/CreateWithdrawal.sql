CREATE PROCEDURE [dbo].[CreateWithdrawal]
	@jsonString NVARCHAR(MAX),
	@executionStatus BIT OUT
	WITH ENCRYPTION
AS
BEGIN

	INSERT INTO [Transaction]([AccountId], [Amount], [TransactionDate], [TransactionTypeEnum], [Description])
	SELECT [AccountId], [Amount], GETUTCDATE(), [TransactionTypeEnum], [Description]
	FROM OPENJSON(@jsonString, '$')
	WITH(
		[AccountId] INT,
		[Amount] DECIMAL(18,2),
		[TransactionTypeEnum] INT,
		[Description] NVARCHAR(MAX)
	);

	UPDATE A
		SET [Balance] -= JsonData.[Amount]
	FROM [Account] AS A
	JOIN OPENJSON(@jsonString, '$')
	WITH(
		[AccountId] INT,
		[Amount] DECIMAL(18,2)
	) JsonData ON JsonData.[AccountId] = A.AccountId

	SET @executionStatus = 1;

END
