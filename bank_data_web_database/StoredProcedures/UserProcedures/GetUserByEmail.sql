CREATE PROCEDURE [dbo].[GetUserByEmail]
	@email NVARCHAR(100) = ''
	WITH ENCRYPTION
AS
BEGIN

	SELECT U.*,
		JSON_QUERY(ISNULL((SELECT UserInformation.* FROM UserInformation WHERE UserInformation.UserId = U.UserId FOR JSON PATH, WITHOUT_ARRAY_WRAPPER), '{}')) AS 'UserInformation'
	FROM [User] U
	INNER JOIN UserInformation UI ON UI.UserId = U.UserId
	WHERE UI.Email = @email AND U.IsDeleted = 0
	FOR JSON PATH

END