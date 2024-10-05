CREATE PROCEDURE [dbo].[UpdateBankAccount]
	@jsonString NVARCHAR(MAX),
	@executionStatus BIT OUT
	WITH ENCRYPTION
AS
BEGIN

	UPDATE A
	SET [UserId] = JsonData.[UserId],
		[AccountNumber] = JsonData.[AccountNumber],
		[AccountHolderName] = JsonData.[AccountHolderName],
		[Balance] = JsonData.[Balance]
	FROM [Account] AS A
	JOIN OPENJSON(@jsonString, '$')
	WITH(
		[AccountId] INT,
		[UserId] INT,
		[AccountNumber] NVARCHAR(200),
		[AccountHolderName] NVARCHAR(200),
		[Balance] DECIMAL(18,2)
	) JsonData ON JsonData.[AccountId] = A.[AccountId]

	SET @executionStatus = 1;

END