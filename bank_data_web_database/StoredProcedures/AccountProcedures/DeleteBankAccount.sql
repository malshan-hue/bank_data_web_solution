CREATE PROCEDURE [dbo].[DeleteBankAccount]
	@accountNumber NVARCHAR(200) = '',
	@executionStatus BIT OUT
	WITH ENCRYPTION
AS
BEGIN
	
	UPDATE [Account] SET IsDeleted = 1 WHERE AccountNumber = @accountNumber

	SET @executionStatus = 1;

END