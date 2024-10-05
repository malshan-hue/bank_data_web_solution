CREATE PROCEDURE [dbo].[CreateUser]
	@jsonString NVARCHAR(MAX),
	@executionStatus BIT OUT
	WITH ENCRYPTION
AS
BEGIN

	DECLARE @primaryKey INT;

	INSERT INTO [User]([UserName], [Password], [PasswordSalt], [ActivationCode], [CreatedDate], [LastLogginDate], [UserGlobalIdentity])
	SELECT [UserName], [Password], [PasswordSalt], [ActivationCode], GETUTCDATE(), GETUTCDATE(), [UserGlobalIdentity]
	FROM OPENJSON(@jsonString, '$')
	WITH(
		[UserName] NVARCHAR(255),
		[Password] NVARCHAR(255),
		[PasswordSalt] NVARCHAR(255),
		[ActivationCode] INT,
		[UserGlobalIdentity] UNIQUEIDENTIFIER
	);

	SET @primaryKey = SCOPE_IDENTITY();

	INSERT INTO [UserInformation]([UserId], [Name], [Email], [Phone], [PictureUrl])
	SELECT @primaryKey, [Name], [Email], [Phone], [PictureUrl]
	FROM OPENJSON(@jsonString, '$.UserInformation')
	WITH(
		[Name] NVARCHAR(255),
		[Email] NVARCHAR(255),
		[Phone] NVARCHAR(50),
		[PictureUrl] NVARCHAR(500)
	);

	SET @executionStatus = 1;

END
