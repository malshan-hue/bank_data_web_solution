CREATE PROCEDURE [dbo].[CreateTransfer]
	@jsonString NVARCHAR(MAX),
	@executionStatus BIT OUT
	WITH ENCRYPTION
AS 
BEGIN

	DECLARE @fromAccountId INT,@toAccountNumber NVARCHAR(MAX), @amount DECIMAL(18,2), @description NVARCHAR(MAX);

	SELECT @fromAccountId = [FromAccountId], @toAccountNumber = [ToAccountNumber], @amount = [Amount], @description = [Description]
	FROM OPENJSON(@jsonString, '$')
	WITH(
		[FromAccountId] INT,
		[ToAccountNumber] NVARCHAR(MAX),
		[Amount] DECIMAL(18,2),
		[Description] NVARCHAR(MAX)

	);

	INSERT INTO [Transaction]([AccountId], [Amount], [TransactionDate], [TransactionTypeEnum], [Description],[InitiatedAccountId])
	SELECT (SELECT AccountId FROM Account WHERE AccountNumber = @toAccountNumber), @amount, GETUTCDATE(), 3, @description, @fromAccountId

	UPDATE Account SET Balance -= @amount WHERE AccountId = @fromAccountId
	UPDATE Account SET Balance += @amount WHERE AccountId = (SELECT AccountId FROM Account WHERE AccountNumber = @toAccountNumber)

	SET @executionStatus = 1;
END