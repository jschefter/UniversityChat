USE [ucdatabase];

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLE_CONSTRAINTS
	WHERE CONSTRAINT_TYPE = 'PRIMARY KEY' AND TABLE_NAME = 'Users' AND 
	TABLE_SCHEMA ='UniversityChat' AND CONSTRAINT_NAME = 'PK_Users' )
	
	BEGIN
	
		ALTER TABLE [UniversityChat].[Users]
			ADD CONSTRAINT [PK_Users] PRIMARY KEY ([UserId])
	
	END;
	
IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLE_CONSTRAINTS
	WHERE CONSTRAINT_TYPE = 'PRIMARY KEY' AND TABLE_NAME = 'Sessions' AND 
	TABLE_SCHEMA ='UniversityChat' AND CONSTRAINT_NAME = 'PK_Sessions' )
	
	BEGIN
	
		ALTER TABLE [UniversityChat].[Sessions]
			ADD CONSTRAINT [PK_Sessions] PRIMARY KEY ([SessionId])
	
	END;
	
IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLE_CONSTRAINTS
	WHERE CONSTRAINT_TYPE = 'PRIMARY KEY' AND TABLE_NAME = 'RoomUsers' AND 
	TABLE_SCHEMA ='UniversityChat' AND CONSTRAINT_NAME = 'PK_Room_Users' )
	
	BEGIN
	
		ALTER TABLE [UniversityChat].[RoomUsers]
			ADD CONSTRAINT [PK_Room_Users] PRIMARY KEY ([RoomId], [UserId])
	
	END;
	
IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLE_CONSTRAINTS
	WHERE CONSTRAINT_TYPE = 'PRIMARY KEY' AND TABLE_NAME = 'Rooms' AND 
	TABLE_SCHEMA ='UniversityChat' AND CONSTRAINT_NAME = 'PK_Rooms' )
	
	BEGIN
	
		ALTER TABLE [UniversityChat].[Rooms]
			ADD CONSTRAINT [PK_Rooms] PRIMARY KEY ([RoomId])
	
	END;
	
	IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLE_CONSTRAINTS
	WHERE CONSTRAINT_TYPE = 'PRIMARY KEY' AND TABLE_NAME = 'History' AND 
	TABLE_SCHEMA ='UniversityChat' AND CONSTRAINT_NAME = 'PK_History' )
	
	BEGIN
	
		ALTER TABLE [UniversityChat].[History]
			ADD CONSTRAINT [PK_History] PRIMARY KEY ([Id])
	
	END;
	
IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLE_CONSTRAINTS
	WHERE CONSTRAINT_TYPE = 'PRIMARY KEY' AND TABLE_NAME = 'Rooms' AND 
	TABLE_SCHEMA ='UniversityChat' AND CONSTRAINT_NAME = 'PK_Rooms' )
	
	BEGIN
	
		ALTER TABLE [UniversityChat].[Rooms]
			ADD CONSTRAINT [PK_Rooms] PRIMARY KEY ([RoomId])
	
	END;
	
IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLE_CONSTRAINTS
	WHERE CONSTRAINT_TYPE = 'PRIMARY KEY' AND TABLE_NAME = 'Roles' AND 
	TABLE_SCHEMA ='UniversityChat' AND CONSTRAINT_NAME = 'PK_Roles' )
	
	BEGIN
	
		ALTER TABLE [UniversityChat].[Roles]
			ADD CONSTRAINT [PK_Roles] PRIMARY KEY ([RoleId])
	
	END;
	
IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLE_CONSTRAINTS
	WHERE CONSTRAINT_TYPE = 'PRIMARY KEY' AND TABLE_NAME = 'History' AND 
	TABLE_SCHEMA ='UniversityChat' AND CONSTRAINT_NAME = 'PK_History' )
	
	BEGIN
	
		ALTER TABLE [UniversityChat].[History]
			ADD CONSTRAINT [PK_History] PRIMARY KEY ([Id])
	
	END;
	
IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLE_CONSTRAINTS
	WHERE CONSTRAINT_TYPE = 'PRIMARY KEY' AND TABLE_NAME = 'Classes' AND 
	TABLE_SCHEMA ='UniversityChat' AND CONSTRAINT_NAME = 'PK_Classes' )
	
	BEGIN
	
		ALTER TABLE [UniversityChat].[Classes]
			ADD CONSTRAINT [PK_Classes] PRIMARY KEY ([ClassId])
	
	END;
	