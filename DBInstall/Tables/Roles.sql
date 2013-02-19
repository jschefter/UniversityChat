USE [CSS490];

IF NOT EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA = 'UniversityChat' 
				AND TABLE_NAME = 'Roles' AND TABLE_TYPE = 'BASE TABLE')

BEGIN

	CREATE TABLE [UniversityChat].[Roles]
	(
		[RoleId] [int] NOT NULL,
		[RoleName] [varchar](50) NOT NULL,
		[RoleDesc] [varchar](250) NULL,
		
		CONSTRAINT [PK_Roles] PRIMARY KEY CLUSTERED 
		(
			[RoleId] ASC
		)
		WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF,
		 ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
		 
	) ON [PRIMARY]	

	ALTER TABLE [UniversityChat].[Roles]
		ADD CONSTRAINT UNQ_Roles_RoleName
		UNIQUE([RoleName]);
END;

