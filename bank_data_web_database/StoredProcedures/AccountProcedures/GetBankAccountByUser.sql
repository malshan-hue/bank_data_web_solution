CREATE PROCEDURE [dbo].[GetBankAccountByUser]
	@userId NVARCHAR(200) = ''
	WITH ENCRYPTION
AS
BEGIN

	SELECT A.*
	FROM [Account] A
	WHERE 
		A.IsDeleted = 0
		AND A.UserId = @userId
	FOR JSON PATH

END

