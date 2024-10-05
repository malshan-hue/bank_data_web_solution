CREATE PROCEDURE [dbo].[CreateNewBankAccount]
	@jsonString NVARCHAR(MAX),
	@executionStatus BIT OUT
	WITH ENCRYPTION
AS
BEGIN

	INSERT INTO [Account]([UserId], [AccountNumber], [AccountHolderName], [Balance], [CreatedDateTime])
	SELECT [UserId], [AccountNumber], [AccountHolderName], [Balance], GETUTCDATE()
	FROM OPENJSON(@jsonString, '$')
	WITH(
		[UserId] INT,
		[AccountNumber] NVARCHAR(200),
		[AccountHolderName] NVARCHAR(200),
		[Balance] DECIMAL(18,2)
	);

END