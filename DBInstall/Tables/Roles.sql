USE [ucdatabase];

IF NOT EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA = 'UniversityChat' 
				AND TABLE_NAME = 'Roles' AND TABLE_TYPE = 'BASE TABLE')

BEGIN

	CREATE TABLE [UniversityChat].[Roles]
	(
		[RoleId] [int] NOT NULL,
		[RoleName] [varchar](50) NOT NULL,
		[RoleDesc] [varchar](250) NULL		 
	)

	ALTER TABLE [UniversityChat].[Roles]
		ADD CONSTRAINT UNQ_Roles_RoleName
		UNIQUE([RoleName]);
END;

