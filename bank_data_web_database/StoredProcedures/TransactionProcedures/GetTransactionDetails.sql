CREATE PROCEDURE [dbo].[GetTransactionDetails]
	@transactionId INT = 0
	WITH ENCRYPTION
AS
BEGIN

	SELECT T.*,
		JSON_QUERY(ISNULL((SELECT A.*,
			JSON_QUERY(ISNULL((SELECT UI.* FROM [UserInformation] UI WHERE UI.UserId = A.UserId FOR JSON PATH, WITHOUT_ARRAY_WRAPPER), '{}')) AS 'UserInformation'
		FROM [Account] A WHERE A.AccountId = T.AccountId FOR JSON PATH, WITHOUT_ARRAY_WRAPPER), '{}')) AS 'Account'
	FROM [Transaction] T
	WHERE [TransactionId] = @transactionId
	FOR JSON PATH

END
