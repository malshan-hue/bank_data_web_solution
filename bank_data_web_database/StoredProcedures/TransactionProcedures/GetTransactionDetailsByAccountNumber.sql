CREATE PROCEDURE [dbo].[GetTransactionDetailsByAccountNumber]
	@accountNumber NVARCHAR(200) = ''
	WITH ENCRYPTION
AS
BEGIN

	SELECT T.*,
		JSON_QUERY(ISNULL((SELECT A.*,
			JSON_QUERY(ISNULL((SELECT UI.* FROM [UserInformation] UI WHERE UI.UserId = A.UserId FOR JSON PATH, WITHOUT_ARRAY_WRAPPER), '{}')) AS 'UserInformation'
		FROM [Account] A WHERE A.AccountId = T.AccountId FOR JSON PATH, WITHOUT_ARRAY_WRAPPER), '{}')) AS 'Account'
	FROM [Transaction] T
	INNER JOIN [Account] A ON A.AccountId = T.AccountId
	WHERE A.AccountNumber = @accountNumber OR T.InitiatedAccountId = (SELECT AccountId FROM Account WHERE AccountNumber = @accountNumber)
	ORDER BY T.TransactionId DESC
	FOR JSON PATH

END

