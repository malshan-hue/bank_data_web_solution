CREATE TABLE [dbo].[UserInformation]
(
	[UserInformationId] INT IDENTITY NOT NULL,
	[UserId] INT NOT NULL,
    [Name] NVARCHAR(255) NOT NULL,
    [Email] NVARCHAR(255) NOT NULL,
    [Phone] NVARCHAR(50) NOT NULL,
    [PictureUrl] NVARCHAR(500),

    CONSTRAINT [UserInformation_UserInformationId_PK] PRIMARY KEY CLUSTERED ([UserInformationId]),
    CONSTRAINT [UserInformation_UserId_FK] FOREIGN KEY ([UserId]) REFERENCES [User]([UserId])
)
