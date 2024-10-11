CREATE PROCEDURE [dbo].[GetALLTransactionsForAdmin]
	@fromDate DATETIME = NULL,
	@toDate DATETIME = NULL
	WITH ENCRYPTION
AS
BEGIN
	
	SELECT T.*,
		JSON_QUERY(ISNULL((SELECT A.*,
			JSON_QUERY(ISNULL((SELECT UI.* FROM [UserInformation] UI WHERE UI.UserId = A.UserId FOR JSON PATH, WITHOUT_ARRAY_WRAPPER), '{}')) AS 'UserInformation'
		FROM [Account] A WHERE A.AccountId = T.AccountId FOR JSON PATH, WITHOUT_ARRAY_WRAPPER), '{}')) AS 'Account'
	FROM [Transaction] T
	WHERE CAST(T.TransactionDate AS DATE) >= CAST(@fromDate AS DATE) AND CAST(T.TransactionDate AS DATE) <= CAST(@toDate AS DATE)
	ORDER BY T.TransactionId DESC
	FOR JSON PATH

END
