USE [CSS490];

IF NOT EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA = 'UniversityChat' 
				AND TABLE_NAME = 'Sessions' AND TABLE_TYPE = 'BASE TABLE')
BEGIN
	CREATE TABLE [UniversityChat].[Sessions]
	(
		[SessionId] [uniqueidentifier] NOT NULL,
		[RoomId] [uniqueidentifier] NOT NULL,
		[SessionStartDateTime] [datetime] NOT NULL,
		[SessionExpirationDateTime] [datetime] NOT NULL,
		[IsActive] [int] NOT NULL,
		CONSTRAINT [PK_Sessions] PRIMARY KEY CLUSTERED 
		(
			[SessionId] ASC
		)
		WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF,
		 ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
	) ON [PRIMARY]
END;


