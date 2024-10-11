CREATE PROCEDURE [dbo].[GetTransactionDetailsByAccountId]
	@accountId NVARCHAR(200) = ''
	WITH ENCRYPTION
AS
BEGIN

	SELECT T.*,
		JSON_QUERY(ISNULL((SELECT A.*,
			JSON_QUERY(ISNULL((SELECT UI.* FROM [UserInformation] UI WHERE UI.UserId = A.UserId FOR JSON PATH, WITHOUT_ARRAY_WRAPPER), '{}')) AS 'UserInformation'
		FROM [Account] A WHERE A.AccountId = T.AccountId FOR JSON PATH, WITHOUT_ARRAY_WRAPPER), '{}')) AS 'Account'
	FROM [Transaction] T
	INNER JOIN [Account] A ON A.AccountId = T.AccountId
	WHERE A.AccountId = @accountId
	FOR JSON PATH

END