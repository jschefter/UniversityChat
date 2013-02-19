USE [CSS490];

IF NOT EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA = 'UniversityChat' 
				AND TABLE_NAME = 'Rooms' AND TABLE_TYPE = 'BASE TABLE')

BEGIN

	CREATE TABLE [UniversityChat].[Rooms]
	(
		[RoomId] [uniqueidentifier] NOT NULL,
		[RoomName] [varchar](50) NOT NULL,
		[RoomDesc] [varchar](250) NULL,
		[ClassId] [uniqueidentifier] NOT NULL,
		[IsPrivate] [int] NULL,
		[IsActive] [int] NULL,
		[ExpirationDate] [date] NOT NULL,
		[StartDate] [date] NOT NULL,
		[LastUsedDate] [date] NOT NULL,
		[ModeratorId] [uniqueidentifier] NULL,
		
		CONSTRAINT [PK_Rooms] PRIMARY KEY CLUSTERED 
		(
			[RoomId] ASC
		)
		WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, 
			ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
			
	) ON [PRIMARY]

	ALTER TABLE [UniversityChat].[Rooms]
		ADD CONSTRAINT UNQ_Rooms_RoomName
		UNIQUE([RoomName]);

END;


