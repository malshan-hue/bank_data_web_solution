CREATE PROCEDURE [dbo].[CreateTransfer]
	@jsonString NVARCHAR(MAX),
	@executionStatus BIT OUT
	WITH ENCRYPTION
AS 
BEGIN

	DECLARE @fromAccountId INT,@toAccountNumber NVARCHAR(MAX), @amount DECIMAL(18,2);

	SELECT @fromAccountId = [FromAccountId], @toAccountNumber = [ToAccountNumber], @amount = [Amount]
	FROM OPENJSON(@jsonString, '$')
	WITH(
		[FromAccountId] INT,
		[ToAccountNumber] NVARCHAR(MAX),
		[Amount] DECIMAL(18,2)

	);

	INSERT INTO [Transaction]([AccountId], [Amount], [TransactionDate], [TransactionTypeEnum], [Description])
	SELECT (SELECT AccountId FROM Account WHERE AccountNumber = @toAccountNumber), @amount, GETUTCDATE(), 3, 'Transfered'

	UPDATE Account SET Balance -= @amount WHERE AccountId = @fromAccountId
	UPDATE Account SET Balance += @amount WHERE AccountId = (SELECT AccountId FROM Account WHERE AccountNumber = @toAccountNumber)

END