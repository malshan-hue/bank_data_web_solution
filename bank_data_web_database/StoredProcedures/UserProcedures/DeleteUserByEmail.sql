CREATE PROCEDURE [dbo].[DeleteUserByEmail]
	@email NVARCHAR(100) = '',
	@executionStatus BIT OUT
	WITH ENCRYPTION
AS
BEGIN

	UPDATE U
	SET	IsDeleted = 1
	FROM [User] AS U
	INNER JOIN [UserInformation] UI ON UI.UserId = U.UserId
	WHERE UI.Email = @email

	SET @executionStatus = 1;

END