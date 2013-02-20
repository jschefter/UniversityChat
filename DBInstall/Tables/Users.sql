USE [ucdatabase];

IF NOT EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA = 'UniversityChat' 
				AND TABLE_NAME = 'Users' AND TABLE_TYPE = 'BASE TABLE')

BEGIN

	CREATE TABLE [UniversityChat].[Users]
	(
		[UserId] [uniqueidentifier] NOT NULL,
		[FName] [varchar](50) NULL,
		[LName] [varchar](50) NULL,
		[NickName] [varchar](50) NOT NULL,
		[Email] [varchar](50) NOT NULL,
		[UserRoleId] [int] NOT NULL
	) 

	ALTER TABLE [UniversityChat].[Users]
		ADD CONSTRAINT UNQ_Users_NickName
		UNIQUE([NickName]);
		
	ALTER TABLE [UniversityChat].[Users]
	ADD CONSTRAINT UNQ_Users_Email
		UNIQUE([Email]);


END;


