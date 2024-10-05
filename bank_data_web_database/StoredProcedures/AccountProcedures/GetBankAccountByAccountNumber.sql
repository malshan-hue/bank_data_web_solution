CREATE PROCEDURE [dbo].[GetBankAccountByAccountNumber]
	@accountNumber NVARCHAR(200) = ''
	WITH ENCRYPTION
AS
BEGIN

	SELECT A.* 
	FROM [Account] A
	WHERE 
		A.IsDeleted = 0
		AND A.AccountNumber = @accountNumber
	FOR JSON PATH

END
