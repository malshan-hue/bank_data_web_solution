CREATE PROCEDURE [dbo].[GetAllUsers]
	WITH ENCRYPTION
AS
BEGIN

	SELECT U.*,
		JSON_QUERY(ISNULL((SELECT UserInformation.* FROM UserInformation WHERE UserInformation.UserId = U.UserId FOR JSON PATH, WITHOUT_ARRAY_WRAPPER), '{}')) AS 'UserInformation'
	FROM [User] U
	INNER JOIN UserInformation UI ON UI.UserId = U.UserId
	WHERE U.IsDeleted = 0
	FOR JSON PATH

END
