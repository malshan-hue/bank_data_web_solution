CREATE PROCEDURE [dbo].[UpdateUser]
	@jsonString NVARCHAR(MAX),
	@executionStatus BIT OUT
	WITH ENCRYPTION
AS
BEGIN

	--update user
	UPDATE U
		SET [UserName] = JsonData.[UserName],
			[Password] = JsonData.[Password],
			[PasswordSalt] = JsonData.[PasswordSalt],
			[ActivationCode] = JsonData.[ActivationCode],
			[UserGlobalIdentity] = JsonData.[UserGlobalIdentity]
	FROM [User] AS U
	JOIN OPENJSON(@jsonString, '$')
	WITH(
		[UserId] INT,
		[UserName] NVARCHAR(255),
		[Password] NVARCHAR(255),
		[PasswordSalt] NVARCHAR(255),
		[ActivationCode] INT,
		[UserGlobalIdentity] UNIQUEIDENTIFIER
	) JsonData
	ON JsonData.[UserId] = U.UserId

	--update user information
	UPDATE UI
		SET [Name] = JsonData.[Name],
			[Email] = JsonData.[Email],
			[Phone] = JsonData.[Phone],
			[PictureUrl] = JsonData.[PictureUrl]
	FROM [UserInformation] AS UI
	JOIN OPENJSON(@jsonString, '$.UserInformation')
	WITH(
		[UserId] INT,
		[Name] NVARCHAR(255),
		[Email] NVARCHAR(255),
		[Phone] NVARCHAR(50),
		[PictureUrl] NVARCHAR(500)
	) JsonData
	ON JsonData.[UserId] = UI.UserId

	SET @executionStatus = 1;

END
