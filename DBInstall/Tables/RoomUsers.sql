USE [CSS490];

IF NOT EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA = 'UniversityChat' 
				AND TABLE_NAME = 'RoomUsers' AND TABLE_TYPE = 'BASE TABLE')
				
BEGIN

	CREATE TABLE [UniversityChat].[RoomUsers]
	(
		[RoomId] [uniqueidentifier] NOT NULL,
		[UserId] [uniqueidentifier] NOT NULL,
		
		CONSTRAINT [PK_Room_Users] PRIMARY KEY CLUSTERED 
		(
			[RoomId] ASC,
			[UserId] ASC			
		) WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF,
		 ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
	) ON [PRIMARY]
	
END;




