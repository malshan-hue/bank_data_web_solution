CREATE PROCEDURE [dbo].[GetBankAccountByAccountHolderName]
	@holderName NVARCHAR(200) = ''
	WITH ENCRYPTION
AS
BEGIN

	SELECT A.* 
	FROM [Account] A
	WHERE 
		A.IsDeleted = 0
		AND A.AccountHolderName = @holderName
	FOR JSON PATH

END

