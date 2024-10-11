CREATE PROCEDURE [dbo].[GetBankAccountByAccountId]
	@accountId NVARCHAR(200) = ''
	WITH ENCRYPTION
AS
BEGIN

	SELECT A.*
	FROM [Account] A
	WHERE 
		A.IsDeleted = 0
		AND A.AccountId = @accountId
	FOR JSON PATH

END
